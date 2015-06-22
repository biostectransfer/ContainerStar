using System.Diagnostics;
using MetadataLoader.Contracts.Utils;

namespace MetadataLoader.Contracts.Database
{
    public class BaseContent:IContent
    {
        #region	Private fields
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _codeName;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _codeNamePlural;
        #endregion
        #region	Public properties
        /// <summary>
        ///     CodeName of object in code.
        /// </summary>
        public string CodeName
        {
            get { return _codeName; }
            set
            {
                _codeName = value;
                CodeNameCamelCase = value.ToCamelCase();
            }
        }

        /// <summary>
        ///     <see cref="CodeName" /> start with lower char
        /// </summary>
        public string CodeNameCamelCase { get; set; }

        /// <summary>
        ///     Plural name of object
        /// </summary>
        public string CodeNamePlural
        {
            get { return _codeNamePlural; }
            set
            {
                _codeNamePlural = value;
                CodeNamePluralCamelCase = value.ToCamelCase();
            }
        }

        /// <summary>
        ///     <see cref="CodeNamePlural" /> start with lower char
        /// </summary>
        public string CodeNamePluralCamelCase { get; set; }
        #endregion
    }
}