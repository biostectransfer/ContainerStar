using System;

namespace ContainerStar.API.SystemLog
{
	public class EntityLoggerAttribute : Attribute
	{
		public EntityLoggerAttribute(/*SystemLogRecordTypes type, */EntityGender gender, string name)
		{
			//SystemLogRecordType = type;
			EntityGender = gender;
			EntityName = name;
		}

		//public SystemLogRecordTypes SystemLogRecordType
		//{
		//	get;
		//	set;
		//}

		public string EntityName
		{
			get;
			set;
		}

		public EntityGender EntityGender
		{
			get;
			set;
		}
	}
}
