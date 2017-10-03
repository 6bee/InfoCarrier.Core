﻿namespace WcfSample
{
    using System;
    using System.Data.SqlClient;
    using System.ServiceModel;
    using InfoCarrier.Core.Common;
    using InfoCarrier.Core.Server;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.Logging;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MyRemoteService : IMyRemoteService
    {
        private static readonly string MasterConnectionString =
            Environment.GetEnvironmentVariable(@"SqlServer__DefaultConnection")
            ?? @"Data Source=(localdb)\MSSQLLocalDB;Database=master;Integrated Security=True;Connect Timeout=30";

        private static readonly string SampleDbName = "InfoCarrierWcfSample";

        public static void RecreateDatabase()
        {
            // Drop database if exists
            using (var master = new SqlConnection(MasterConnectionString))
            {
                master.Open();
                using (var cmd = master.CreateCommand())
                {
                    cmd.CommandText = $@"
                        IF EXISTS (SELECT * FROM sys.databases WHERE name = N'{SampleDbName}')
                        BEGIN
                            ALTER DATABASE[{SampleDbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                            DROP DATABASE[{SampleDbName}];
                        END";
                    cmd.ExecuteNonQuery();
                }
            }

            SqlConnection.ClearAllPools();

            // Create database
            using (var ctx = CreateDbContext())
            {
                ctx.Database.EnsureCreated();
            }
        }

        public static DbContext CreateDbContext()
        {
            var connectionString =
                new SqlConnectionStringBuilder(MasterConnectionString) { InitialCatalog = SampleDbName }.ToString();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(connectionString);
            var context = new BloggingContext(optionsBuilder.Options);
            context.GetService<ILoggerFactory>().AddConsole((_, __) => true);
            return context;
        }

        public QueryDataResult ProcessQueryDataRequest(QueryDataRequest request)
        {
            using (var helper = new QueryDataHelper(CreateDbContext, request))
            {
                return helper.QueryData();
            }
        }

        public SaveChangesResult ProcessSaveChangesRequest(SaveChangesRequest request)
        {
            using (var helper = new SaveChangesHelper(CreateDbContext, request))
            {
                return helper.SaveChanges();
            }
        }
    }
}