using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using MetadataLoader.Contracts.CSharp;

namespace MetadataLoader.Contracts.Generation
{
    public sealed class CodeGenerationTools
    {
        private const string UnknownNamespace = "UNKNOWN_NAMESPACE";
        #region Static
        private static string ResolveAccessibility(MemberInfo info)
        {
            var level = info.Accessibility != AccessibilityLevels.None ? info.Accessibility : info.DefaultAccessibility;
            level = level != AccessibilityLevels.None ? level : AccessibilityLevels.Public;

            return ResolveAccessibility(level);
        }
        private static string ResolveAccessibility(AccessibilityLevels level, AccessibilityLevels defaultLevel)
        {
            return level == defaultLevel ? null : ResolveAccessibility(level);
        }
        private static string ResolveAccessibility(AccessibilityLevels level)
        {
            switch (level)
            {
                case AccessibilityLevels.Public:
                    return "public";
                case AccessibilityLevels.Protected:
                    return "protected";
                case AccessibilityLevels.Internal:
                    return "internal";
                case AccessibilityLevels.ProtectedInternal:
                    return "protected internal";
                case AccessibilityLevels.Private:
                    return "private";
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }
        private static string ResolveAttribute(AttributeUsageInfo info, bool single = true)
        {
            var attribute = info.Name;
            if (!string.IsNullOrWhiteSpace(info.InitializationString))
            {
                attribute = string.Format("{0}({1})", attribute, info.InitializationString);
            }
            if (single)
            {
                attribute = string.Format("[{0}]", attribute);
            }
            return attribute;
        }
        private static string ResolveTypeArgument(TypeUsageInfo argument)
        {
            var modifier = string.Empty;
            if (argument.IsTypeArgument)
            {
                switch (argument.TypeArgumentConfiguration.Modifier)
                {
                    case TypeArgumentModifier.In:
                        modifier = "in";
                        break;
                    case TypeArgumentModifier.Out:
                        modifier = "out";
                        break;
                }
            }
            return modifier.Postfix() + argument.CodeName();
        }
        private static string ResolveClassMemberModifier(IClassMember info)
        {
            if (info.IsAbstract)
            {
                return "abstract";
            }
            if (info.IsVirtual)
            {
                return "virtual";
            }
            if (info.IsOverride)
            {
                return "override";
            }
            return null;
        }
        #endregion
        #region	Private fields
        private readonly string _defaultNamespace;
        private readonly string _indent;
        private readonly ITextTransform _transform;
        #endregion
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public CodeGenerationTools(ITextTransform transform, string defaultNamespace = null, string indent = "    ")
        {
            Contract.Requires(_transform != null);

            _transform = transform;
            _indent = indent;
            _defaultNamespace = defaultNamespace ?? UnknownNamespace;
        }
        #endregion
        #region	Public methods
        public void WriteUsing(params Info[] infos)
        {
            WriteUsing(null, infos);
        }
        public void WriteUsing(string[] usings, params Info[] infos)
        {
            usings = usings ?? new string[0];
            infos = infos ?? new Info[0];

            var allUsings = usings.Union(infos.SelectMany(info => info.GetUsedNamespaces())).Where(s => !string.IsNullOrEmpty(s)).Distinct().OrderBy(s => s);
            foreach (var @using in allUsings)
            {
                WriteLine("using {0};", @using);
            }
        }

        /// <summary>
        ///     Places namespace, opening curved brace and PushIndent
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startArea"> print opening curved brace and PushIndent or not </param>
        public void WriteNamespace(string name = null, bool startArea = true)
        {
            WriteLine("namespace {0}", !string.IsNullOrWhiteSpace(name) ? name : _defaultNamespace);
            StartArea(startArea);
        }
        public void WriteAttributes(IReadOnlyCollection<AttributeUsageInfo> attributes)
        {
            foreach (var attribute in attributes)
            {
                WriteLine(ResolveAttribute(attribute));
            }
        }
        /// <summary>
        ///     Write class declaration header
        /// </summary>
        /// <param name="info"></param>
        public void WriteDescription(MemberInfo info)
        {
            WriteDescription(info.Description);
        }
        public void WriteDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                return;
            }
            WriteLine("/// <summary>");
            WriteLine("///     {0}", description);
            WriteLine("/// </summary>");
        }

        /// <summary>
        ///     Write class declaration header
        /// </summary>
        /// <param name="info"></param>
        /// <param name="startArea"></param>
        public void WriteClassDeclaration(ClassInfo info, bool startArea = true)
        {
            WriteAdvTypeDeclaration(info, "class", startArea, WriteClassModifiers);
        }
        public void WriteInterfaceDeclaration(InterfaceInfo info, bool startArea = true)
        {
            WriteAdvTypeDeclaration(info, "interface", startArea);
        }

        public void WriteDefaultConstructor(ClassInfo info, AccessibilityLevels accessibility = AccessibilityLevels.Public, bool startArea = true)
        {
            WriteDefaultConstructor(info.Name, accessibility, startArea);
        }
        public void WriteDefaultConstructor(string className, AccessibilityLevels accessibility = AccessibilityLevels.Public, bool startArea = true)
        {
            WriteDescription(string.Format(@"Initializes a new instance of the <see cref=""{0}"" /> class.", className));
            Write(ResolveAccessibility(accessibility));
            WriteLine(" {0}()", className);
            StartArea(startArea);
        }

        public void WriteProperty(PropertyInfo info, AdvTypeInfo parent = null)
        {
            var parentIsInterface = false;
            if (parent != null)
            {
                parentIsInterface = parent.GetTypeUsage().IsInterface;
            }

            WriteDescription(info.Description);
            WriteAttributes(info.Attributes);

            if (parentIsInterface)
            {
                Write(info.Type.CodeName().Postfix());
                Write(info.Name);
                WriteLine("{{{0}{1}}}", info.HasGetter ? "get;" : string.Empty, info.HasSetter ? "set;" : string.Empty);
            }
            else
            {
                if (!info.IsExplicit)
                {
                    Write(ResolveAccessibility(info).Postfix());
                    Write(ResolveClassMemberModifier(info).Postfix());
                }
                Write(info.Type.CodeName().Postfix());
                if (info.IsExplicit)
                {
                    Write(info.ExplicitInterface.CodeName());
                    Write(".");
                }
                Write(info.Name);

                if (info.IsAutoProperty)
                {
                    WriteLine("{{ {0}get; {1}set; }}",
                        ResolveAccessibility(info.Getter.Accessibility, AccessibilityLevels.Public).Postfix(),
                        ResolveAccessibility(info.Setter.Accessibility, AccessibilityLevels.Public).Postfix());
                }
                else
                {
                    WriteLine();
                    StartArea();
                    WritePropertyInvoker(info.Getter, "get", invoker => string.Format("return {0};", info.BackingField.Name));
                    WritePropertyInvoker(info.Setter, "set", invoker => string.Format("{0} = value;", info.BackingField.Name));
                    EndArea();
                }
            }
        }

        private void WritePropertyInvoker(PropertyInvokerInfo invoker, string name, Func<PropertyInvokerInfo, string> defaultBody)
        {
            if (invoker == null)
            {
                return;
            }
            WriteAttributes(invoker.Attributes);
            Write("{0}{1}", ResolveAccessibility(invoker.Accessibility, AccessibilityLevels.Public).Postfix(), name);
            #region Body
            var body = invoker.HasBody ? invoker.Body.Text : defaultBody(invoker);
            var bodyLines = Regex.Split(body, "\r\n|\r|\n");
            if (bodyLines.Length == 1)
            {
                WriteLine(" {{ {0} }}", body);
            }
            else
            {
                StartArea();
                foreach (var line in bodyLines)
                {
                    WriteLine(line);
                }
                EndArea();
            }
            #endregion
        }

        /// <summary>
        ///     Places opening curved brace and PushIndent
        /// </summary>
        /// <param name="startArea"> Print opening curved brace and PushIndent or not </param>
        public void StartArea(bool startArea = true)
        {
            if (!startArea)
            {
                return;
            }
            WriteLine("{");
            PushIndent();
        }
        /// <summary>
        ///     Places closing curved brace and pop indent
        /// </summary>
        public void EndArea()
        {
            PopIndent();
            WriteLine("}");
        }
        public void PushIndent()
        {
            _transform.PushIndent(_indent);
        }
        public void PopIndent()
        {
            _transform.PopIndent();
        }
        #endregion
        #region Private methods
        public void Write(string value, params object[] args)
        {
            _transform.Write(value, args);
        }
        public void WriteLine(string value = null, params object[] args)
        {
            _transform.WriteLine(value ?? string.Empty, args);
        }
        private void WriteClassModifiers(ClassInfo info)
        {
            if (info.IsStatic)
            {
                Write(" static");
            }
            else if (info.IsSealed)
            {
                Write(" sealed");
            }
            else if (info.IsAbstract)
            {
                Write(" abstract");
            }
        }
        private void WriteAdvTypeDeclaration<T>(T info, string keyword, bool startArea = true, Action<T> writeModifiers = null)
            where T : AdvTypeInfo
        {
            WriteDescription(info);
            WriteAttributes(info.Attributes);
            Write(ResolveAccessibility(info));
            if (writeModifiers != null)
            {
                writeModifiers(info);
            }
            if (info.IsPartial)
            {
                Write(" partial");
            }
            Write(" {0} {1}", keyword, info.Name);
            if (info.IsGeneric)
            {
                Write("<{0}>", string.Join(", ", info.TypeArguments.Select(ResolveTypeArgument)));
            }
            #region Inherits
            if (info.Inherits.Count != 0)
            {
                Write(": ");
                var inherits = info.Inherits.OrderByDescending(t => t.IsClass).ThenBy(t => t.Name).Select(t => t.CodeName()).ToArray();
                WriteLine(inherits[0]);
                PushIndent();
                for (var i = 1; i < inherits.Length; i++)
                {
                    WriteLine(inherits[i].Prefix(","));
                }
                PopIndent();
            }
            else
            {
                WriteLine();
            }
            #endregion
            #region Generic conditions
            if (info.IsGeneric)
            {
                PushIndent();
                foreach (var argument in info.TypeArguments.Where(t => t.IsTypeArgument && !t.TypeArgumentConfiguration.IsEmpty))
                {
                    Write("where {0}:", argument.CodeName());
                    if (argument.TypeArgumentConfiguration.ConditionModifier.HasFlag(TypeArgumentConditionModifier.Class))
                    {
                        Write(" class");
                    }
                    else if (argument.TypeArgumentConfiguration.ConditionModifier.HasFlag(TypeArgumentConditionModifier.Struct))
                    {
                        Write(" struct");
                    }

                    Write(string.Join(", ", argument.TypeArgumentConfiguration.ConditionTypes.Select(t => t.CodeName())));

                    if (argument.TypeArgumentConfiguration.ConditionModifier.HasFlag(TypeArgumentConditionModifier.DefaultConstructor))
                    {
                        Write(", new()");
                    }
                }
                PopIndent();
            }
            #endregion
            StartArea(startArea);
        }
        #endregion
    }
}