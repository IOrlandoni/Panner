using System.Collections.Generic;
using System.Linq;
using Panner.Sort.Particles;
using Xunit;

namespace Sort.Tests.Particles
{
    public class ByPropertyParticleTests
    {
        private class Fake
        {
            public int Value { get; set; }
        }

        [Fact]
        public void SortsAscending()
        {
            var prop = typeof(Fake).GetProperty(nameof(Fake.Value));
            var particle = new ByPropertyParticle<Fake>(prop);
            var data = new List<Fake>
            {
                new Fake{Value = 3},
                new Fake{Value = 1},
                new Fake{Value = 2},
            }.AsQueryable().OrderBy(x => 0);

            var result = particle.ApplyTo(data).Select(x => x.Value).ToList();
            Assert.Equal(new List<int>{1,2,3}, result);
        }

        [Fact]
        public void SortsDescending()
        {
            var prop = typeof(Fake).GetProperty(nameof(Fake.Value));
            var particle = new ByPropertyParticle<Fake>(prop, true);
            var data = new List<Fake>
            {
                new Fake{Value = 1},
                new Fake{Value = 2},
                new Fake{Value = 3},
            }.AsQueryable().OrderBy(x => 0);

            var result = particle.ApplyTo(data).Select(x => x.Value).ToList();
            Assert.Equal(new List<int>{3,2,1}, result);
        }
    }
}
