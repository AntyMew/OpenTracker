using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo
{
    public class ChangeWorldStateTests
    {
        private readonly IMode _mode = new Mode();

        [Fact]
        public void CanExecute_ShouldAlwaysReturnTrue()
        {
            var sut = new ChangeWorldState(_mode, WorldState.StandardOpen);
            
            Assert.True(sut.CanExecute());
        }

        [Theory]
        [InlineData(WorldState.StandardOpen, WorldState.StandardOpen)]
        [InlineData(WorldState.Inverted, WorldState.Inverted)]
        public void ExecuteDo_ShouldSetWorldStateToNewValue(WorldState expected, WorldState newValue)
        {
            var sut = new ChangeWorldState(_mode, newValue);
            sut.ExecuteDo();
            
            Assert.Equal(expected, _mode.WorldState);
        }

        [Theory]
        [InlineData(WorldState.StandardOpen, WorldState.StandardOpen)]
        [InlineData(WorldState.Inverted, WorldState.Inverted)]
        public void ExecuteUndo_ShouldSetWorldStateToPreviousValue(
            WorldState expected, WorldState previousValue)
        {
            _mode.WorldState = previousValue;
            var sut = new ChangeWorldState(_mode, WorldState.StandardOpen);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _mode.WorldState);
        }
    }
}