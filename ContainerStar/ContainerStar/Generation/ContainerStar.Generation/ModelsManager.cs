using System.Collections.Generic;
using System.Linq;
using MetadataLoader.Contracts.CSharp;
using MetadataLoader.Contracts.Database;
using MetadataLoader.EntityFramework;

namespace ContainerStar.Generation
{
    public sealed class ModelsManager
    {
        public List<ModelInfo> Generate(IEnumerable<ITable<TableContent, ColumnContent>> tables, IEnumerable<EntityInfo> entities)
        {
            var result = new List<ModelInfo>();
            var tableArray = tables.ToArray();

            var baseModel = TypeUsageInfo.CreateClass("BaseModel", string.Empty);
            var iintervalfields = TypeUsageInfo.CreateInterface("IIntervalModelFields", string.Empty);

            foreach (var entity in entities)
            {
                var table = tableArray.First(t => t.Name == entity.TableName && t.Schema == entity.TableSchemaName);
                var modelColumns = table.Columns.Where(c => c.Content.InModel).ToArray();
                if (modelColumns.Length == 0)
                {
                    continue;
                }

                var properties = modelColumns.Select(c => new PropertyModelInfo(entity.SimpleProperties.First(p => p.ColumnName == c.Name), c)).ToList();
                var model = new ModelInfo(entity, table, properties);

                model.Attributes.Add(AttributeDictionary.DataContract);
                model.InheritsFrom(baseModel);
                
                #region IntervalFields
                {
                    var fromDate = properties.FirstOrDefault(p => p.Name == "fromDate");
                    var toDate = properties.FirstOrDefault(p => p.Name == "toDate");
                    if (fromDate != null && toDate != null)
                    {
                        entity.InheritsFrom(iintervalfields);

                        EntitiesManager.AddExplicitProperty(fromDate, iintervalfields, entity);
                        EntitiesManager.AddExplicitProperty(toDate, iintervalfields, entity);
                    }
                }
                #endregion

                foreach (var property in model.ModelProperties)
                {
                    if (property.Content.IsModelRequired)
                    {
                        property.Attributes.Add(AttributeDictionary.Required);
                    }
                    property.Attributes.Add(AttributeDictionary.DataMember);
                    property.Description = string.Format("Model property for <see cref=\"{0}.{1}\"/> entity", entity.Name, property.Property.Name);
                }

                result.Add(model);
            }

            return result;
        }
    }
}