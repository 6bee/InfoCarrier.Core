// Copyright (c) on/off it-solutions gmbh. All rights reserved.
// Licensed under the MIT license. See license.txt file in the project root for license information.

namespace InfoCarrier.Core.FunctionalTests.InMemory
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore.TestModels.UpdatesModel;

    public class UpdatesInfoCarrierFixture : UpdatesFixtureBase<TestStoreBase>
    {
        private readonly InfoCarrierTestHelper<UpdatesContext> helper;

        public UpdatesInfoCarrierFixture()
        {
            this.helper = InMemoryTestStore<UpdatesContext>.CreateHelper(
                null,
                opt => new UpdatesContext(opt),
                UpdatesModelInitializer.Seed,
                false,
                w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        }

        public override TestStoreBase CreateTestStore()
            => this.helper.CreateTestStore();

        public override UpdatesContext CreateContext(TestStoreBase testStore)
            => this.helper.CreateInfoCarrierContext(testStore);
    }
}