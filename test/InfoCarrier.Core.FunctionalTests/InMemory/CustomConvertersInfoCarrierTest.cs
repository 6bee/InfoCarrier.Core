﻿// Copyright (c) on/off it-solutions gmbh. All rights reserved.
// Licensed under the MIT license. See license.txt file in the project root for license information.

namespace InfoCarrier.Core.FunctionalTests.InMemory
{
    using System;
    using InfoCarrier.Core.FunctionalTests.TestUtilities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.TestUtilities;

    public class CustomConvertersInfoCarrierTest : CustomConvertersTestBase<CustomConvertersInfoCarrierTest.TestFixture>
    {
        public CustomConvertersInfoCarrierTest(TestFixture fixture)
            : base(fixture)
        {
        }

        // Disabled: In-memory database is case-sensitive
        public override void Can_insert_and_read_back_with_case_insensitive_string_key()
        {
        }

        public class TestFixture : CustomConvertersFixtureBase
        {
            private ITestStoreFactory testStoreFactory;

            protected override ITestStoreFactory TestStoreFactory =>
                InfoCarrierTestStoreFactory.EnsureInitialized(
                    ref this.testStoreFactory,
                    InfoCarrierTestStoreFactory.InMemory,
                    this.ContextType,
                    this.OnModelCreating);

            public override bool StrictEquality => true;

            public override bool SupportsAnsi => false;

            public override bool SupportsUnicodeToAnsiConversion => true;

            public override bool SupportsLargeStringComparisons => true;

            public override bool SupportsBinaryKeys => false;

            public override DateTime DefaultDateTime => default;
        }
    }
}
