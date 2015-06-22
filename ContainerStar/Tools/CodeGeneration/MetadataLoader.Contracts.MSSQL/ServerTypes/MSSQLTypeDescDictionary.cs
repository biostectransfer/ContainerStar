using System;
using System.Collections.Generic;

namespace MetadataLoader.MSSQL.Contracts.ServerTypes
{
    public class MSSQLTypeDescDictionary : ITypeDictionary
    {
        #region Private fields
        private readonly Dictionary<short, MSSQLTypeDesc> _idDictionary;
        private readonly Dictionary<string, MSSQLTypeDesc> _nameDictionary;
        private readonly Dictionary<NativeTypes, MSSQLTypeDesc> _nativeTypeDictionary;
        #endregion
        #region Constructor
        /// <summary>
        ///     Instance constructor
        /// </summary>
        /// <exception cref="ArgumentNullException">Argument is null.</exception>
        public MSSQLTypeDescDictionary()
        {
            _idDictionary = new Dictionary<short, MSSQLTypeDesc>();
            _nativeTypeDictionary = new Dictionary<NativeTypes, MSSQLTypeDesc>();
            _nameDictionary = new Dictionary<string, MSSQLTypeDesc>();
        }
        #endregion
        #region Public methods
        public MSSQLTypeDesc GetDesc(NativeTypes type)
        {
            return _nativeTypeDictionary[type];
        }
        public MSSQLTypeDesc GetDesc(short type)
        {
            return _idDictionary[type];
        }
        public MSSQLTypeDesc GetDesc(string typeName)
        {
            return _nameDictionary[typeName];
        }

        public bool TryGetDescriptor(string typeName, out MSSQLTypeDesc descriptor)
        {
            return _nameDictionary.TryGetValue(typeName, out descriptor);
        }
        public bool TryGetDescriptor(int value, out MSSQLTypeDesc descriptor)
        {
            if (value <= short.MaxValue)
            {
                return _idDictionary.TryGetValue((short) value, out descriptor);
            }
            descriptor = null;
            return false;
        }
        public bool TryGetDescriptor(short value, out MSSQLTypeDesc descriptor)
        {
            return _idDictionary.TryGetValue(value, out descriptor);
        }
        #endregion
        #region ITypeDictionary members
        /// <summary>
        ///     Add desc
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="names"></param>
        /// <exception cref="ArgumentNullException">Argument is null.</exception>
        void ITypeDictionary.AddDesc(MSSQLTypeDesc desc, List<string> names)
        {
            _idDictionary.Add(desc.Id, desc);
            if (!desc.IsDerived)
            {
                _nativeTypeDictionary.Add(desc.BaseType, desc);
            }
            if (ReferenceEquals(names, null))
            {
                return;
            }
            foreach (var name in names)
            {
                _nameDictionary.Add(name, desc);
            }
        }
        #endregion
    }
}