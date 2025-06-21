using Panner;
using Panner.Builders;
using Panner.Filter.Generators;
using Panner.Filter.Particles;
using Xunit;
using Filter.Tests.Parsing.ByPropertyName;

namespace Filter.Tests.Parsing.Generators
{
    public class ComplexLogicGeneratorTests
    {
        private static IPContext GetContext()
        {
            var builder = new PContextBuilder();
            builder.Entity<FilterableOne>()
                .Property(p => p.Filterable)
                .IsFilterableByName();

            return builder.Build();
        }

        [Fact]
        public void DoublePipeParsesAsOr()
        {
            var context = GetContext();
            var generator = new ComplexLogicGenerator<FilterableOne>();

            var result = generator.TryGenerate(context, "Filterable=1||Filterable=2", out var particle);

            Assert.True(result);
            Assert.IsType<OrFilterParticle<FilterableOne>>(particle);
        }

        [Fact]
        public void SinglePipeDoesNotParse()
        {
            var context = GetContext();
            var generator = new ComplexLogicGenerator<FilterableOne>();

            var result = generator.TryGenerate(context, "Filterable=1|Filterable=2", out var particle);

            Assert.False(result);
            Assert.Null(particle);
        }
    }
}
