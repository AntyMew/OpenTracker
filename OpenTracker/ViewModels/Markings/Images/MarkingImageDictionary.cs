﻿using System.Collections.Generic;
using OpenTracker.Models.Markings;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Markings.Images
{
    /// <summary>
    /// This is the dictionary container for marking image control ViewModel instances.
    /// </summary>
    public class MarkingImageDictionary : LazyDictionary<MarkType, IMarkingImageVMBase>,
        IMarkingImageDictionary
    {
        private readonly IMarkingImageFactory _factory;

        public MarkingImageDictionary(IMarkingImageFactory factory)
            : base(new Dictionary<MarkType, IMarkingImageVMBase>())
        {
            _factory = factory;
        }

        protected override IMarkingImageVMBase Create(MarkType key)
        {
            return _factory.GetMarkingImageVM(key);
        }
    }
}
