using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.BossShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.BossShuffle
{
    public class BossShuffleRequirementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

        [Fact]
        public void ModeChanged_ShouldUpdateMetValue()
        {
            var sut = new BossShuffleRequirement(_mode, true);
            _mode.BossShuffle.Returns(true);

            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.BossShuffle)));
            
            Assert.True(sut.Met);
        }

        [Theory]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(true, true, true)]
        public void Met_ShouldReturnExpectedValue(bool expected, bool bossShuffle, bool requirement)
        {
            _mode.BossShuffle.Returns(bossShuffle);
            var sut = new BossShuffleRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IBossShuffleRequirement.Factory>();
            var sut = factory(false);
            
            Assert.NotNull(sut as BossShuffleRequirement);
        }
    }
}