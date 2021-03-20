using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo
{
    public class ChangeGenericKeysTests
    {
        private readonly IMode _mode = new Mode();

        [Fact]
        public void CanExecute_ShouldReturnTrueAlways()
        {
            var sut = new ChangeGenericKeys(_mode, false);
            
            Assert.True(sut.CanExecute());
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteDo_ShouldSetBigKeyShuffleToNewValue(bool expected, bool newValue)
        {
            var sut = new ChangeGenericKeys(_mode, newValue);
            sut.ExecuteDo();
            
            Assert.Equal(expected, _mode.GenericKeys);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteUndo_ShouldSetBigKeyShuffleToPreviousValue(bool expected, bool previousValue)
        {
            _mode.GenericKeys = previousValue;
            var sut = new ChangeGenericKeys(_mode, !previousValue);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _mode.GenericKeys);
        }
    }
}