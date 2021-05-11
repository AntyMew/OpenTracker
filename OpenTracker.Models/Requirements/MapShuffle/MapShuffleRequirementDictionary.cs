using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.MapShuffle
{
    /// <summary>
    /// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
    /// <see cref="IMapShuffleRequirement"/> objects indexed by <see cref="bool"/>.
    /// </summary>
    public class MapShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>, IMapShuffleRequirementDictionary
    {
        private readonly IMapShuffleRequirement.Factory _factory;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="IMapShuffleRequirement"/> objects.
        /// </param>
        public MapShuffleRequirementDictionary(IMapShuffleRequirement.Factory factory)
            : base(new Dictionary<bool, IRequirement>())
        {
            _factory = factory;
        }

        protected override IRequirement Create(bool key)
        {
            return _factory(key);
        }
    }
}