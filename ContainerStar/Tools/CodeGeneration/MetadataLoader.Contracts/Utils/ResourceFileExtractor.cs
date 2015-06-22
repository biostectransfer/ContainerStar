using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MetadataLoader.Contracts.Utils
{
    /// <summary>
    ///     Summary description for ResourceFileExtractor.
    /// </summary>
    public sealed class ResourceFileExtractor
    {
        #region Private fields
        private readonly Assembly _assembly;
        private readonly ResourceFileExtractor _baseExtractor;
        private readonly string _assemblyName;
        #endregion
        #region Constructors
        /// <summary>
        ///     Instance constructor
        /// </summary>
        /// <param name="resourcePath"> </param>
        /// <param name="assembly"> </param>
        /// <param name="baseExtractor"> </param>
        public ResourceFileExtractor(string resourcePath = ".Resources.",
            Assembly assembly = null,
            ResourceFileExtractor baseExtractor = null)
        {
            _assembly = assembly ?? Assembly.GetCallingAssembly();
            _baseExtractor = baseExtractor;
            _assemblyName = Assembly.GetName().Name;
            ResourceFilePath = resourcePath;
        }
        #endregion
        #region Public properties
        /// <summary>
        ///     Work assembly
        /// </summary>
        public Assembly Assembly
        {
            [DebuggerStepThrough] get { return _assembly; }
        }
        /// <summary>
        ///     Work assembly name
        /// </summary>
        public string AssemblyName
        {
            [DebuggerStepThrough] get { return _assemblyName; }
        }
        /// <summary>
        ///     Path to read resource files. Example: .Resources.Upgrades.
        /// </summary>
        public string ResourceFilePath { get; private set; }
        #endregion
        #region Public methods
        public IEnumerable<string> GetFileNames()
        {
            return GetFileNames(true);
        }

        public IEnumerable<string> GetFileNames(bool includeBase)
        {
            var path = AssemblyName + ResourceFilePath;
            var result = (Assembly.GetManifestResourceNames().Where(resourceName => resourceName.StartsWith(path))
                .Select(resourceName => resourceName.Replace(path, string.Empty))).ToList();

            if (ReferenceEquals(_baseExtractor, null) || !includeBase)
            {
                return result;
            }

            foreach (var name in _baseExtractor.GetFileNames(true).Where(name => !result.Contains(name)))
            {
                result.Add(name);
            }
            return result;
        }

        public string ReadFileFromRes(string fileName)
        {
            var stream = ReadFileFromResToStream(fileName);
            string result;
            var sr = new StreamReader(stream);
            try
            {
                result = sr.ReadToEnd();
            }
            finally
            {
                sr.Close();
            }
            return result;
        }

        public bool TryReadFileFromRes(string fileName, out string text)
        {
            Stream stream;
            if (TryReadFileFromResToStream(fileName, out stream))
            {
                using (stream)
                {
                    using (var sr = new StreamReader(stream))
                    {
                        text = sr.ReadToEnd();
                        return true;
                    }
                }
            }
            text = null;
            return false;
        }

        public string ReadFileFromResFormat(string fileName, params object[] formatArgs)
        {
            return string.Format(ReadFileFromRes(fileName), formatArgs);
        }

        /// <summary>
        ///     Read file in current assembly by specific path
        /// </summary>
        /// <param name="specificPath"> Specific path </param>
        /// <param name="fileName"> Read file name </param>
        /// <returns> </returns>
        public string ReadSpecificFileFromRes(string specificPath, string fileName)
        {
            var ext = new ResourceFileExtractor(specificPath, Assembly);
            return ext.ReadFileFromRes(fileName);
        }

        /// <summary>
        ///     Read file in current assembly by specific path
        /// </summary>
        /// <param name="specificPath"> Specific path </param>
        /// <param name="fileName"> Read file name </param>
        /// <returns> </returns>
        public Stream ReadSpecificFileFromResToStream(string specificPath, string fileName)
        {
            var ext = new ResourceFileExtractor(specificPath, Assembly);
            return ext.ReadFileFromResToStream(fileName);
        }
        /// <summary>
        ///     Read file in current assembly by specific file name
        /// </summary>
        /// <param name="fileName"> </param>
        /// <returns> </returns>
        /// <exception cref="ApplicationException">
        ///     <c>ApplicationException</c>
        ///     .
        /// </exception>
        public Stream ReadFileFromResToStream(string fileName)
        {
            Stream stream;
            if (TryReadFileFromResToStream(fileName, out stream))
            {
                return stream;
            }
            var nameResFile = AssemblyName + ResourceFilePath + fileName;
            throw new ApplicationException("Can't find resource file " + nameResFile);
        }
        /// <summary>
        ///     Read file in current assembly by specific file name
        /// </summary>
        /// <param name="fileName"> </param>
        /// <param name="stream"> </param>
        /// <returns> </returns>
        /// <exception cref="ApplicationException">
        ///     <c>ApplicationException</c>
        ///     .
        /// </exception>
        public bool TryReadFileFromResToStream(string fileName, out Stream stream)
        {
            var nameResFile = AssemblyName + ResourceFilePath + fileName;
            stream = Assembly.GetManifestResourceStream(nameResFile);
            if (ReferenceEquals(stream, null))
            {
                #region Get from base extractor
                if (!ReferenceEquals(_baseExtractor, null))
                {
                    stream = _baseExtractor.ReadFileFromResToStream(fileName);
                }
                #endregion
            }
            return !ReferenceEquals(stream, null);
        }

        public void CopyToFile(string resName, FileInfo file)
        {
            using (var srcStream = ReadFileFromResToStream(resName))
            using (var destStream = file.Create())
            {
                srcStream.CopyTo(destStream);
            }
        }
        #endregion
    }
}