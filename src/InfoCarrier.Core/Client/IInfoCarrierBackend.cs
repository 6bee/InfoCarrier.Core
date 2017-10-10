﻿// Copyright (c) on/off it-solutions gmbh. All rights reserved.
// Licensed under the MIT license. See license.txt file in the project root for license information.

namespace InfoCarrier.Core.Client
{
    using System.Threading.Tasks;
    using Common;
    using Microsoft.EntityFrameworkCore;

    public interface IInfoCarrierBackend
    {
        string ServerUrl { get; }

        QueryDataResult QueryData(QueryDataRequest request, DbContext dbContext);

        Task<QueryDataResult> QueryDataAsync(QueryDataRequest request, DbContext dbContext);

        SaveChangesResult SaveChanges(SaveChangesRequest request);

        Task<SaveChangesResult> SaveChangesAsync(SaveChangesRequest request);

        void BeginTransaction();

        Task BeginTransactionAsync();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
