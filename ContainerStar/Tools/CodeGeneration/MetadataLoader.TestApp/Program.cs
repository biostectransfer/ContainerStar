using System.Diagnostics;
using MasterDataModule.Generation;
using MetadataLoader.Excel;
using MetadataLoader.TestApp.PrepareData;

namespace MetadataLoader.TestApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string conn = @"Data Source=.;Initial Catalog=CommonMasterData;Integrated Security=True;MultipleActiveResultSets=True";
            const string contentFile = @"P:\SVN\TUEVSUED\src\Tools\CodeGeneration\Data\FE Stammdaten.xlsx";


            //var migration = new DataMigration(@"P:\SVN\TUEVSUED\src\MasterDataModule.Generation\MasterDataModule\Generation\MasterDataModule.Generation\Declarations\Original");
            //migration.Migrate();

            var timer = Stopwatch.StartNew();
            #region Legacy
            //var stamdattenTableNames = DataSheetManager.Read(contentFile, TableDescriptor.GetRead("Tables{T}", 2)).Tables[0].Rows.Cast<DataRow>().Select(row => row[1].ToString()).ToArray();

            //var loader = MSSQLLoadManager<TableContent, ColumnContent>.Instance.GetDatabaseLoader(Connection);
            //var tables = loader.Load(tableFilter: table => !table.IsView && table.Schema == "DATA" && stamdattenTableNames.Contains(table.Name));

            //var contentLoader = MSSQLLoadManager<TableContent, ColumnContent>.Instance.GetContentLoader(tables);
            //contentLoader.Load(contentFile);

            //var entityLoader = MSSQLManager<TableContent, ColumnContent>.Instance.GetEntityLoader(
            //    new EntityExtractor());

            //var entities = entityLoader.Load(tables);

            //NOTE: Create template
            //contentLoader.CreateDefaultTemplate(@"p:\cool.xlsx", ContentLoadType.Column);
            #endregion
            var tablesManager = TablesManager.LoadFromDatabase(conn);
            var tables = tablesManager.Load(contentFile);

            var entityManager = EntitiesManager.Create();
            var entities = entityManager.Load(tables);


            

            timer.Stop();
        }
    }
}