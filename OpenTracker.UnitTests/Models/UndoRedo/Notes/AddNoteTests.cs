using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.UndoRedo.Markings;
using OpenTracker.Models.UndoRedo.Notes;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Notes
{
    public class AddNoteTests
    {
        private readonly ILocationNoteCollection _notes = Substitute.For<ILocationNoteCollection>();
        private readonly AddNote _sut;

        public AddNoteTests()
        {
            var location = Substitute.For<ILocation>();

            static IChangeMarking ChangeMarkingFactory(IMarking marking, MarkType newMarking) =>
                Substitute.For<IChangeMarking>();
            
            location.Notes.Returns(_notes);

            _sut = new AddNote(() => new Marking(ChangeMarkingFactory), location);
        }

        [Theory]
        [InlineData(true, 0)]
        [InlineData(true, 1)]
        [InlineData(true, 2)]
        [InlineData(true, 3)]
        [InlineData(false, 4)]
        public void CanExecute_ReturnsTrue_WhenNotesCountIsLessThanFour(bool expected, int count)
        {
            _notes.Count.Returns(count);
            
            Assert.Equal(expected, _sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldCallAdd()
        {
            _sut.ExecuteDo();
            
            _notes.Received().Add(Arg.Any<IMarking>());
        }

        [Fact]
        public void ExecuteUndo_ShouldCallRemove()
        {
            _sut.ExecuteUndo();
            
            _notes.Received().Remove(Arg.Any<IMarking>());
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IAddNote.Factory>();
            var sut = factory(Substitute.For<ILocation>());
            
            Assert.NotNull(sut as AddNote);
        }
    }
}