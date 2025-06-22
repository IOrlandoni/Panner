using System;
using Panner.Builders;
using Xunit;

namespace Core.Tests
{
    public class PEntityBuilderTests
    {
        private class WithField
        {
            public int Field = 0;
        }

        private class Sample
        {
            public int Value { get; set; }
        }

        [Fact]
        public void PropertyExpressionWithMethodThrows()
        {
            var builder = new PContextBuilder().Entity<Sample>();
            Assert.Throws<ArgumentException>(() => builder.Property(x => x.ToString()));
        }

        [Fact]
        public void PropertyExpressionWithFieldThrows()
        {
            var builder = new PContextBuilder().Entity<WithField>();
            Assert.Throws<ArgumentException>(() => builder.Property(x => x.Field));
        }

        [Fact]
        public void PropertyExpressionFromDifferentTypeThrows()
        {
            var builder = new PContextBuilder().Entity<WithField>();
            Assert.Throws<ArgumentException>(() => builder.Property(x => ((Sample)(object)x).Value));
        }
    }
}
