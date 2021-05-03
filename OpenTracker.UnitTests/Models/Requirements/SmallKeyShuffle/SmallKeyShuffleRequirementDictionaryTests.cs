using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.SmallKeyShuffle
{
    public class SmallKeyShuffleRequirementDictionaryTests
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly SmallKeyShuffleRequirementDictionary _sut;

        public SmallKeyShuffleRequirementDictionaryTests()
        {
            static ISmallKeyShuffleRequirement Factory(bool expectedValue)
            {
                return Substitute.For<ISmallKeyShuffleRequirement>();
            }

            _sut = new SmallKeyShuffleRequirementDictionary(Factory);
        }

        [Fact]
        public void Indexer_ShouldReturnTheSameInstance()
        {
            var requirement1 = _sut[false];
            var requirement2 = _sut[false];
            
            Assert.Equal(requirement1, requirement2);
        }

        [Fact]
        public void Indexer_ShouldReturnTheDifferentInstances()
        {
            var requirement1 = _sut[false];
            var requirement2 = _sut[true];
            
            Assert.NotEqual(requirement1, requirement2);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<ISmallKeyShuffleRequirementDictionary>();
            
            Assert.NotNull(sut as SmallKeyShuffleRequirementDictionary);
        }
    }
}