namespace SmartStore.Data.Migrations
{
    using SmartStore.Core.Data;
    using System;
    using System.Data.Entity.Migrations;
    using System.Web.Hosting;

    public partial class update_sp_with_new_tablename : DbMigration
    {
        public override void Up()
        {
            if (HostingEnvironment.IsHosted && DataSettings.Current.IsSqlServer)
            {
              //  Sql(GetAlterTagCountProcedureSql(true));
            }
        }
        
        public override void Down()
        {
            if (HostingEnvironment.IsHosted && DataSettings.Current.IsSqlServer)
            {
              //  Sql(GetAlterTagCountProcedureSql(false));
            }
        }

        private string GetAlterTagCountProcedureSql(bool newVersion)
        {
            string result = null;

            if (newVersion)
            {
                result =
                    "ALTER PROCEDURE [dbo].[ProductTagCountLoadAll]\r\n" +
                    "(\r\n" +
                    "	@StoreId int,\r\n" +
                    "   @IncludeHidden bit = 0\r\n" +
                    ")\r\n" +
                    "AS\r\n" +
                    "BEGIN\r\n" +
                    "    SET NOCOUNT ON\r\n" +
                    "    SELECT pt.Id as [ProductTagId], COUNT(p.Id) as [ProductCount]\r\n" +
                    "    FROM SSProductTag pt with (NOLOCK)\r\n" +
                    "	LEFT JOIN SSProduct_ProductTag_Mapping pptm with (NOLOCK) ON pt.[Id] = pptm.[ProductTag_Id]\r\n" +
                    "	LEFT JOIN SSProduct p with (NOLOCK) ON pptm.[Product_Id] = p.[Id]\r\n" +
                    "	WHERE\r\n" +
                    "		p.[Deleted] = 0\r\n" +
                    "		AND p.Published = 1\r\n" +
                    "		AND p.VisibleIndividually = 1\r\n" +
                    "		AND (@IncludeHidden = 1 Or pt.Published = 1)\r\n" +
                    "		AND (@StoreId = 0 or (p.LimitedToStores = 0 OR EXISTS (\r\n" +
                    "			SELECT 1 FROM [SSStoreMapping] sm\r\n" +
                    "			WHERE [sm].EntityId = p.Id AND [sm].EntityName = 'Product' and [sm].StoreId=@StoreId\r\n" +
                    "			)))\r\n" +
                    "	GROUP BY pt.Id\r\n" +
                    "	ORDER BY pt.Id\r\n" +
                    "END\r\n";
            }
            else
            {
                result =
                    "ALTER PROCEDURE [dbo].[ProductTagCountLoadAll]\r\n" +
                    "(\r\n" +
                    "	@StoreId int\r\n" +
                    ")\r\n" +
                    "AS\r\n" +
                    "BEGIN\r\n" +
                    "    SET NOCOUNT ON\r\n" +
                    "    SELECT pt.Id as [ProductTagId], COUNT(p.Id) as [ProductCount]\r\n" +
                    "    FROM SSProductTag pt with (NOLOCK)\r\n" +
                    "	LEFT JOIN SSProduct_ProductTag_Mapping pptm with (NOLOCK) ON pt.[Id] = pptm.[ProductTag_Id]\r\n" +
                    "	LEFT JOIN SSProduct p with (NOLOCK) ON pptm.[Product_Id] = p.[Id]\r\n" +
                    "	WHERE\r\n" +
                    "		p.[Deleted] = 0\r\n" +
                    "		AND p.Published = 1\r\n" +
                    "		AND (@StoreId = 0 or (p.LimitedToStores = 0 OR EXISTS (\r\n" +
                    "			SELECT 1 FROM [SSStoreMapping] sm\r\n" +
                    "			WHERE [sm].EntityId = p.Id AND [sm].EntityName = 'Product' and [sm].StoreId=@StoreId\r\n" +
                    "			)))\r\n" +
                    "	GROUP BY pt.Id\r\n" +
                    "	ORDER BY pt.Id\r\n" +
                    "END\r\n";
            }

            return result;
        }
    }
}
