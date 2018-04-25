﻿// Copyright (c) on/off it-solutions gmbh. All rights reserved.
// Licensed under the MIT license. See license.txt file in the project root for license information.

namespace InfoCarrier.Core.FunctionalTests.InMemory.Query
{
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.EntityFrameworkCore.TestUtilities;
    using TestUtilities;

    public class InheritanceRelationshipsQueryInfoCarrierTest : InheritanceRelationshipsQueryTestBase<InheritanceRelationshipsQueryInfoCarrierTest.TestFixture>
    {
        public InheritanceRelationshipsQueryInfoCarrierTest(TestFixture fixture)
            : base(fixture)
        {
        }

        public class TestFixture : InheritanceRelationshipsQueryFixtureBase
        {
            private ITestStoreFactory testStoreFactory;

            protected override ITestStoreFactory TestStoreFactory =>
                InfoCarrierTestStoreFactory.EnsureInitialized(
                    ref this.testStoreFactory,
                    InfoCarrierTestStoreFactory.InMemory,
                    this.ContextType,
                    this.OnModelCreating);
        }
    }
}
