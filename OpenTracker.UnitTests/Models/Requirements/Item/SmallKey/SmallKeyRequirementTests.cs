using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Item.SmallKey;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Item.SmallKey
{
    public class SmallKeyRequirementTests
    {
        private readonly ISmallKeyItem _item = Substitute.For<ISmallKeyItem>();
        
        [Fact]
        public void ItemChanged_ShouldUpdateValue()
        {
            var sut = new SmallKeyRequirement(_item);
            _item.EffectiveCurrent.Returns(1);

            _item.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _item, new PropertyChangedEventArgs(nameof(ISmallKeyItem.EffectiveCurrent)));
            
            Assert.Equal(AccessibilityLevel.Normal, sut.Accessibility);
        }

        [Fact]
        public void Met_ShouldRaisePropertyChanged()
        {
            var sut = new SmallKeyRequirement(_item);
            _item.EffectiveCurrent.Returns(1);

            Assert.PropertyChanged(sut, nameof(IRequirement.Met),
                () => _item.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _item, new PropertyChangedEventArgs(nameof(ISmallKeyItem.EffectiveCurrent))));
        }

        [Fact]
        public void Met_ShouldRaiseChangePropagated()
        {
            var sut = new SmallKeyRequirement(_item);
            _item.EffectiveCurrent.Returns(1);

            var eventRaised = false;

            void Handler(object? sender, EventArgs e)
            {
                eventRaised = true;
            }
            
            sut.ChangePropagated += Handler;
            _item.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _item, new PropertyChangedEventArgs(nameof(ISmallKeyItem.EffectiveCurrent)));
            sut.ChangePropagated -= Handler;
            
            Assert.True(eventRaised);
        }

        [Theory]
        [InlineData(false, 0, 1)]
        [InlineData(true, 1, 1)]
        [InlineData(true, 2, 1)]
        [InlineData(true, 3, 1)]
        [InlineData(false, 0, 2)]
        [InlineData(false, 1, 2)]
        [InlineData(true, 2, 2)]
        [InlineData(true, 3, 2)]
        [InlineData(false, 0, 3)]
        [InlineData(false, 1, 3)]
        [InlineData(false, 2, 3)]
        [InlineData(true, 3, 3)]
        public void Met_ShouldMatchExpected(bool expected, int current, int count)
        {
            _item.EffectiveCurrent.Returns(current);
            var sut = new SmallKeyRequirement(_item, count);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void Accessibility_ShouldRaisePropertyChanged()
        {
            var sut = new SmallKeyRequirement(_item);
            _item.EffectiveCurrent.Returns(1);

            Assert.PropertyChanged(sut, nameof(IRequirement.Accessibility),
                () => _item.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _item, new PropertyChangedEventArgs(nameof(ISmallKeyItem.EffectiveCurrent))));
        }

        [Theory]
        [InlineData(AccessibilityLevel.None, 0, 1)]
        [InlineData(AccessibilityLevel.Normal, 1, 1)]
        [InlineData(AccessibilityLevel.Normal, 2, 1)]
        [InlineData(AccessibilityLevel.Normal, 3, 1)]
        [InlineData(AccessibilityLevel.None, 0, 2)]
        [InlineData(AccessibilityLevel.None, 1, 2)]
        [InlineData(AccessibilityLevel.Normal, 2, 2)]
        [InlineData(AccessibilityLevel.Normal, 3, 2)]
        [InlineData(AccessibilityLevel.None, 0, 3)]
        [InlineData(AccessibilityLevel.None, 1, 3)]
        [InlineData(AccessibilityLevel.None, 2, 3)]
        [InlineData(AccessibilityLevel.Normal, 3, 3)]
        public void Accessibility_ShouldMatchExpected(AccessibilityLevel expected, int current, int count)
        {
            _item.EffectiveCurrent.Returns(current);
            var sut = new SmallKeyRequirement(_item, count);
            
            Assert.Equal(expected, sut.Accessibility);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<ISmallKeyRequirement.Factory>();
            var sut = factory(_item);
            
            Assert.NotNull(sut as SmallKeyRequirement);
        }
    }
}