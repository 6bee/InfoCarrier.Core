// Copyright (c) on/off it-solutions gmbh. All rights reserved.
// Licensed under the MIT license. See license.txt file in the project root for license information.

namespace InfoCarrier.Core.Server
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using InfoCarrier.Core.Common;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// A service which provides QueryData and SaveChanges functionality on the server-side.
    /// </summary>
    public interface IInfoCarrierServer
    {
        /// <summary>
        ///     Executes the requested query against the actual database.
        /// </summary>
        /// <param name="dbContextFactory">
        ///     Factory for <see cref="DbContext" /> against which the requested query will be executed.
        /// </param>
        /// <param name="request">
        ///     The <see cref="QueryDataRequest" /> object from the client containing the query.
        /// </param>
        /// <returns>
        ///     The result of the query execution.
        /// </returns>
        QueryDataResult QueryData(
            Func<DbContext> dbContextFactory,
            QueryDataRequest request);

        /// <summary>
        ///     Asynchronously executes the requested query against the actual database.
        /// </summary>
        /// <param name="dbContextFactory">
        ///     Factory for <see cref="DbContext" /> against which the requested query will be executed.
        /// </param>
        /// <param name="request">
        ///     The <see cref="QueryDataRequest" /> object from the client containing the query.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the result of the query execution.
        /// </returns>
        Task<QueryDataResult> QueryDataAsync(
            Func<DbContext> dbContextFactory,
            QueryDataRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Saves the updated entities into the actual database.
        /// </summary>
        /// <param name="dbContextFactory">
        ///     Factory for <see cref="DbContext" /> to save the updated entities into.
        /// </param>
        /// <param name="request">
        ///     The <see cref="SaveChangesRequest" /> object from the client containing the updated entities.
        /// </param>
        /// <returns>
        ///     The save operation result which can either be
        ///     a SaveChangesResult.Success or SaveChangesResult.Error.
        /// </returns>
        SaveChangesResult SaveChanges(
            Func<DbContext> dbContextFactory,
            SaveChangesRequest request);

        /// <summary>
        ///     Asynchronously saves the updated entities into the actual database.
        /// </summary>
        /// <param name="dbContextFactory">
        ///     Factory for <see cref="DbContext" /> to save the updated entities into.
        /// </param>
        /// <param name="request">
        ///     The <see cref="SaveChangesRequest" /> object from the client containing the updated entities.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the save operation result which can either be
        ///     a SaveChangesResult.Success or SaveChangesResult.Error.
        /// </returns>
        Task<SaveChangesResult> SaveChangesAsync(
            Func<DbContext> dbContextFactory,
            SaveChangesRequest request,
            CancellationToken cancellationToken = default);
    }
}
