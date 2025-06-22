using System;
using Panner;
using Panner.Builders;
using Xunit;

namespace Core.Tests
{
    public class PContextTests
    {
        private class Sample { }

        [Fact]
        public void GetGeneratorsThrowsWhenEntityMissing()
        {
            var context = new PContextBuilder().Build();
            Assert.Throws<Exception>(() => context.GetGenerators<Sample, ISortParticle<Sample>>());
        }
    }
}
