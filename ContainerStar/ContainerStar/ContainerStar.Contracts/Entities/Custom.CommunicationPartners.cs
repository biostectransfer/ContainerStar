using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ContainerStar.Contracts.Entities
{
    /// <summary>
    ///  CommunicationPartners
    /// </summary>
    public partial class CommunicationPartners
    {
        /// <summary>
        /// Customer Name
        /// </summary>
        public string Title
        {
            get
            {
                return String.Format("{0} {1} ({2})", FirstName, Name, Phone);
            }
        }
    }
}
