using System.Collections.Generic;
using System.Linq;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Aggregate
{
    /// <summary>
    ///     This class contains the dictionary container for requirements aggregating a set of requirements.
    /// </summary>
    public class AggregateRequirementDictionary : LazyDictionary<HashSet<IRequirement>, IRequirement>, IAggregateRequirementDictionary
    {
        private readonly IAggregateRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new requirements aggregating a set of requirements.
        /// </param>
        public AggregateRequirementDictionary(IAggregateRequirement.Factory factory)
            : base(new Dictionary<HashSet<IRequirement>, IRequirement>(HashSet<IRequirement>.CreateSetComparer()))
        {
            _factory = factory;
        }

        protected override IRequirement Create(HashSet<IRequirement> key)
        {
            return _factory(key.ToList());
        }
    }
}