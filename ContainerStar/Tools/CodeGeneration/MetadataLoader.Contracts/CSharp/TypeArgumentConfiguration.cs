using System.Linq;
using MetadataLoader.Contracts.Generation;

namespace MetadataLoader.Contracts.CSharp
{
    public sealed class TypeArgumentConfiguration
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public TypeArgumentConfiguration(TypeArgumentModifier modifier = TypeArgumentModifier.None, TypeArgumentConditionModifier conditionModifier = TypeArgumentConditionModifier.None,
            params TypeUsageInfo[] conditionTypes)
        {
            Modifier = modifier;
            ConditionModifier = conditionModifier;
            ConditionTypes = conditionTypes ?? new TypeUsageInfo[0];
        }
        #endregion
        #region	Public properties
        public TypeArgumentModifier Modifier { get; private set; }
        public TypeArgumentConditionModifier ConditionModifier { get; private set; }
        public TypeUsageInfo[] ConditionTypes { get; private set; }
        public string Description { get; set; }
        public bool IsEmpty
        {
            get { return ConditionTypes.Length == 0 || ConditionModifier == TypeArgumentConditionModifier.None; }
        }
        #endregion
        #region	Public methods
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Modifier: {0}, Condition: {1}{2}",
                Modifier, ConditionModifier, string.Join(", ", ConditionTypes.Cast<object>()).Prefix(", "));
        }
        #endregion
    }
}