using System.Runtime.Serialization;
using ContainerStar.Contracts;
using System;

namespace ContainerStar.API.Models
{
    [DataContract]
    public class BaseModel: IHasId<int>, ISystemModelFields
    {
        [DataMember]		
        public int Id { get; set; }

        /// <summary>
        ///     Create date entity
        /// </summary>
        [DataMember]
        public DateTime createDate { get; set; }

        /// <summary>
        ///     Change date entity
        /// </summary>
        [DataMember]
        public DateTime changeDate { get; set; }
    }
}
