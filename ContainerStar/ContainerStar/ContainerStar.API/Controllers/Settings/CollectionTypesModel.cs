using System;

namespace ContainerStar.API.Controllers
{
    /// <summary>
    ///     CollectionTypesModel class
    /// </summary>
    public partial class CollectionTypesModel
    {
        public bool Permission { get; set;}
        public bool Role { get; set;}
        public bool Equipments { get; set;}
        public bool TransportProducts { get; set;}
        public bool ContainerTypes { get; set;}
    }
}
