namespace InfoCarrier.Core.Client.Storage.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Infrastructure.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.EntityFrameworkCore.Update;
    using Microsoft.Extensions.Logging;

    public class InfoCarrierDatabase : Database, IInfoCarrierDatabase
    {
        private readonly IInfoCarrierBackend infoCarrierBackend;
        private readonly ILogger<InfoCarrierDatabase> logger;

        public InfoCarrierDatabase(
            IQueryCompilationContextFactory queryCompilationContextFactory,
            IDbContextOptions options,
            ILogger<InfoCarrierDatabase> logger)
            : base(queryCompilationContextFactory)
        {
            this.logger = logger;
            this.infoCarrierBackend = options.Extensions.OfType<InfoCarrierOptionsExtension>().First().InfoCarrierBackend;
        }

        public override int SaveChanges(IReadOnlyList<IUpdateEntry> entries)
        {
            var saveChanges = new SaveChangesRequest();
            saveChanges.DataTransferObjects.AddRange(entries.Select(e => new DataTransferObject(e)));
            SaveChangesResult result = this.infoCarrierBackend.SaveChanges(saveChanges, i => entries[i]);
            MergeResults(entries, result);
            return result.CountPersisted;
        }

        public override async Task<int> SaveChangesAsync(
            IReadOnlyList<IUpdateEntry> entries,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var saveChanges = new SaveChangesRequest();
            saveChanges.DataTransferObjects.AddRange(entries.Select(e => new DataTransferObject(e)));
            SaveChangesResult result = await this.infoCarrierBackend.SaveChangesAsync(saveChanges, i => entries[i]);
            MergeResults(entries, result);
            return result.CountPersisted;
        }

        private static void MergeResults(IReadOnlyList<IUpdateEntry> entries, SaveChangesResult result)
        {
            // Merge the results / update properties modified during SaveChanges on the server-side
            foreach (var merge in entries.Zip(result.DataTransferObjects, (e, d) => new { Entry = e, Dto = d }))
            {
                foreach (DataTransferObject.Property prop in
                    merge.Entry.EntityType.GetProperties().SelectMany(p => merge.Dto.YieldPropery(p)))
                {
                    // Can not (and need not) merge non-temporary PK values
                    if (prop.EfProperty.IsKey() && !merge.Entry.HasTemporaryValue(prop.EfProperty))
                    {
                        continue;
                    }

                    merge.Entry.SetCurrentValue(prop.EfProperty, prop.CurrentValue);
                }
            }
        }
    }
}