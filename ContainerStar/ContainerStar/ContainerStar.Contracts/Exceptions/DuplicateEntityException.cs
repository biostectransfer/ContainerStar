using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerStar.Contracts.Exceptions
{
    /// <summary>
    /// Duplicate Entity Exception
    /// </summary>
    public class DuplicateEntityException : DbEntityValidationException
    {
        public DuplicateEntityException(string[] businessKeys, string message) :
            base(message)
        {
            BusinessKeys = businessKeys;
        }

        /// <summary>
        /// Keys for checking entity on duplicates
        /// </summary>
        public string[] BusinessKeys { get; private set; }
    }
}
