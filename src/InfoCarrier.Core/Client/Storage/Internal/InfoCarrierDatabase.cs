// Copyright (c) on/off it-solutions gmbh. All rights reserved.
// Licensed under the MIT license. See license.txt file in the project root for license information.

namespace InfoCarrier.Core.Client.Storage.Internal
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using InfoCarrier.Core.Client.Infrastructure.Internal;
    using InfoCarrier.Core.Common;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.EntityFrameworkCore.Update;

    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InfoCarrierDatabase : Database, IInfoCarrierDatabase
    {
        private readonly IInfoCarrierClient infoCarrierClient;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Entity Framework Core internal.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1642:ConstructorSummaryDocumentationMustBeginWithStandardText", Justification = "Entity Framework Core internal.")]
        public InfoCarrierDatabase(
            DatabaseDependencies dependencies,
            IDbContextOptions options)
            : base(dependencies)
        {
            this.infoCarrierClient = options.Extensions.OfType<InfoCarrierOptionsExtension>().First().InfoCarrierClient;
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Entity Framework Core internal.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:ElementReturnValueMustBeDocumented", Justification = "Entity Framework Core internal.")]
        public override int SaveChanges(IReadOnlyList<IUpdateEntry> entries)
        {
            SaveChangesResult result = this.infoCarrierClient.SaveChanges(new SaveChangesRequest(entries));
            return result.ApplyTo(entries);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Entity Framework Core internal.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:ElementReturnValueMustBeDocumented", Justification = "Entity Framework Core internal.")]
        public override async Task<int> SaveChangesAsync(
            IReadOnlyList<IUpdateEntry> entries,
            CancellationToken cancellationToken = default)
        {
            SaveChangesResult result = await this.infoCarrierClient.SaveChangesAsync(new SaveChangesRequest(entries), cancellationToken);
            return result.ApplyTo(entries);
        }
    }
}
