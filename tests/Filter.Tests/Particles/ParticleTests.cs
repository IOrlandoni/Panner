using System;
using System.Linq;
using System.Linq.Expressions;
using Panner.Filter.Particles;
using Panner.Filter;
using Xunit;

namespace Filter.Tests.Particles
{
    public class ParticleTests
    {
        private class Fake
        {
            public int Value { get; set; }
        }

        [Fact]
        public void ByPropertyParticleEqual()
        {
            var prop = typeof(Fake).GetProperty(nameof(Fake.Value));
            var particle = new ByPropertyParticle<Fake, int>(prop, Operator.Equal, 5);
            var param = Expression.Parameter(typeof(Fake), "e");
            var expr = particle.GetExpression(param);
            var func = Expression.Lambda<Func<Fake, bool>>(expr, param).Compile();

            Assert.True(func(new Fake { Value = 5 }));
            Assert.False(func(new Fake { Value = 3 }));
        }

        [Fact]
        public void AndFilterParticleCombinesExpressions()
        {
            var prop = typeof(Fake).GetProperty(nameof(Fake.Value));
            var gt = new ByPropertyParticle<Fake, int>(prop, Operator.GreaterThan, 2);
            var lt = new ByPropertyParticle<Fake, int>(prop, Operator.LessThan, 5);
            var and = new AndFilterParticle<Fake>(gt, lt);
            var param = Expression.Parameter(typeof(Fake), "e");
            var expr = and.GetExpression(param);
            var func = Expression.Lambda<Func<Fake, bool>>(expr, param).Compile();

            Assert.True(func(new Fake { Value = 3 }));
            Assert.False(func(new Fake { Value = 6 }));
        }

        [Fact]
        public void OrFilterParticleCombinesExpressions()
        {
            var prop = typeof(Fake).GetProperty(nameof(Fake.Value));
            var eq5 = new ByPropertyParticle<Fake, int>(prop, Operator.Equal, 5);
            var eq7 = new ByPropertyParticle<Fake, int>(prop, Operator.Equal, 7);
            var or = new OrFilterParticle<Fake>(eq5, eq7);
            var param = Expression.Parameter(typeof(Fake), "e");
            var expr = or.GetExpression(param);
            var func = Expression.Lambda<Func<Fake, bool>>(expr, param).Compile();

            Assert.True(func(new Fake { Value = 5 }));
            Assert.True(func(new Fake { Value = 7 }));
            Assert.False(func(new Fake { Value = 3 }));
        }
    }
}
