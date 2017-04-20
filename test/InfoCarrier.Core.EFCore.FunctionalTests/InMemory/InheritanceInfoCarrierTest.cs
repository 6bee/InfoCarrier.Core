﻿namespace InfoCarrier.Core.EFCore.FunctionalTests.InMemory
{
    using System;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.EntityFrameworkCore.Specification.Tests;
    using Xunit;

    public class InheritanceInfoCarrierTest : InheritanceTestBase<InheritanceInfoCarrierFixture>
    {
        public InheritanceInfoCarrierTest(InheritanceInfoCarrierFixture fixture)
            : base(fixture)
        {
        }

        public override void Discriminator_used_when_projection_over_derived_type2()
        {
            Assert.Equal(
                CoreStrings.PropertyNotFound("Discriminator", "Bird"),
                Assert.Throws<InvalidOperationException>(() =>
                        base.Discriminator_used_when_projection_over_derived_type2()).Message);
        }

        public override void Discriminator_with_cast_in_shadow_property()
        {
            Assert.Equal(
                CoreStrings.PropertyNotFound("Discriminator", "Animal"),
                Assert.Throws<InvalidOperationException>(() =>
                        base.Discriminator_with_cast_in_shadow_property()).Message);
        }
    }
}