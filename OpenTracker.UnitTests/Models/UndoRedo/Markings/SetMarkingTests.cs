using Autofac;
using NSubstitute;
using OpenTracker.Models.Markings;
using OpenTracker.Models.UndoRedo.Markings;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Markings
{
    public class ChangeMarkingTests
    {
        private readonly IMarking _marking = Substitute.For<IMarking>();
        private const MarkType NewMarking = MarkType.HCLeft;
        private readonly ChangeMarking _sut;

        public ChangeMarkingTests()
        {
            _sut = new ChangeMarking(_marking, NewMarking);
        }

        [Fact]
        public void CanExecute_ShouldReturnTrue()
        {
            Assert.True(_sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldSetMarkingToNewMarking()
        {
            _sut.ExecuteDo();
            
            Assert.Equal(NewMarking, _marking.Mark);
        }

        [Fact]
        public void ExecuteUndo_ShouldSetMarkingToPreviousMarking()
        {
            const MarkType previousMarking = MarkType.Unknown;
            _marking.Mark.Returns(previousMarking);
            _sut.ExecuteDo();
            _sut.ExecuteUndo();
            
            Assert.Equal(previousMarking, _marking.Mark);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IChangeMarking.Factory>();
            var sut = factory(_marking, NewMarking);
            
            Assert.NotNull(sut as ChangeMarking);
        }
    }
}