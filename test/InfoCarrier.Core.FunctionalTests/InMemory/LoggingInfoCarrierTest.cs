﻿namespace InfoCarrier.Core.FunctionalTests.InMemory
{
    using Client;
    using Microsoft.EntityFrameworkCore;

    public class LoggingInfoCarrierTest : LoggingTestBase
    {
        private readonly IInfoCarrierBackend backend = new SimpleInMemoryTestStore(nameof(LoggingInfoCarrierTest));

        protected override string ProviderName => @"InfoCarrier.Core";

        protected override string DefaultOptions => @"InfoCarrierServerUrl=LoggingInfoCarrierTest ";

        protected override DbContextOptionsBuilder CreateOptionsBuilder()
            => new DbContextOptionsBuilder().UseInfoCarrierBackend(this.backend);
    }
}