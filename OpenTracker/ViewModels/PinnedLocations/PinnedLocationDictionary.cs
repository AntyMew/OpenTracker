﻿using System.Collections.Generic;
using OpenTracker.Models.Locations;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.PinnedLocations
{
    public class PinnedLocationDictionary : LazyDictionary<LocationID, IPinnedLocationVM>,
        IPinnedLocationDictionary
    {
        private readonly ILocationDictionary _locations;
        private readonly IPinnedLocationVMFactory _factory;

        public PinnedLocationDictionary(
            ILocationDictionary locations, IPinnedLocationVMFactory factory)
            : base(new Dictionary<LocationID, IPinnedLocationVM>())
        {
            _locations = locations;
            _factory = factory;
        }

        protected override IPinnedLocationVM Create(LocationID key)
        {
            return _factory.GetLocationControlVM(_locations[key]);
        }
    }
}
