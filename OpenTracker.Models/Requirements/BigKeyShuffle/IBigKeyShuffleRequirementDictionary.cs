using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.BigKeyShuffle
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
    /// <see cref="IBigKeyShuffleRequirement"/> objects indexed by <see cref="bool"/>.
    /// </summary>
    public interface IBigKeyShuffleRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}