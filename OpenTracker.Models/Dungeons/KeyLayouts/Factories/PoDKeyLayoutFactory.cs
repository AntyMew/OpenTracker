using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories
{
    /// <summary>
    ///     This class contains the creation logic for Palace of Darkness key layouts.
    /// </summary>
    public class PoDKeyLayoutFactory : IPoDKeyLayoutFactory
    {
        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IBigKeyShuffleRequirementDictionary _bigKeyShuffleRequirements;
        private readonly ISmallKeyShuffleRequirementDictionary _smallKeyShuffleRequirements;
        
        private readonly IBigKeyLayout.Factory _bigKeyFactory;
        private readonly IEndKeyLayout.Factory _endFactory;
        private readonly ISmallKeyLayout.Factory _smallKeyFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="aggregateRequirements">
        ///     The aggregate requirement dictionary.
        /// </param>
        /// <param name="bigKeyShuffleRequirements">
        ///     The big key shuffle requirement dictionary.
        /// </param>
        /// <param name="smallKeyShuffleRequirements">
        ///     The small key shuffle requirement dictionary.
        /// </param>
        /// <param name="bigKeyFactory">
        ///     An Autofac factory for creating big key layouts.
        /// </param>
        /// <param name="endFactory">
        ///     An Autofac factory for ending key layouts.
        /// </param>
        /// <param name="smallKeyFactory">
        ///     An Autofac factory for creating small key layouts.
        /// </param>
        public PoDKeyLayoutFactory(
            IAggregateRequirementDictionary aggregateRequirements,
            IBigKeyShuffleRequirementDictionary bigKeyShuffleRequirements,
            ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements, IBigKeyLayout.Factory bigKeyFactory,
            IEndKeyLayout.Factory endFactory, ISmallKeyLayout.Factory smallKeyFactory)
        {
            _aggregateRequirements = aggregateRequirements;
            _bigKeyShuffleRequirements = bigKeyShuffleRequirements;
            _smallKeyShuffleRequirements = smallKeyShuffleRequirements;

            _bigKeyFactory = bigKeyFactory;
            _endFactory = endFactory;
            _smallKeyFactory = smallKeyFactory;
        }

        public IList<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            return new List<IKeyLayout>
            {
                _endFactory(_aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[true],
                    _smallKeyShuffleRequirements[true]
                }]),
                _smallKeyFactory(
                    4, new List<DungeonItemID>
                    {
                        DungeonItemID.PoDShooterRoom,
                        DungeonItemID.PoDMapChest,
                        DungeonItemID.PoDArenaLedge,
                        DungeonItemID.PoDBigKeyChest,
                        DungeonItemID.PoDStalfosBasement,
                        DungeonItemID.PoDArenaBridge
                    },
                    false, new List<IKeyLayout>
                    {
                        _smallKeyFactory(
                            6, new List<DungeonItemID>
                            {
                                DungeonItemID.PoDShooterRoom,
                                DungeonItemID.PoDMapChest,
                                DungeonItemID.PoDArenaLedge,
                                DungeonItemID.PoDBigKeyChest,
                                DungeonItemID.PoDStalfosBasement,
                                DungeonItemID.PoDArenaBridge,
                                DungeonItemID.PoDCompassChest,
                                DungeonItemID.PoDDarkBasementLeft,
                                DungeonItemID.PoDDarkBasementRight,
                                DungeonItemID.PoDHarmlessHellway
                            },
                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                    },
                    dungeon, _bigKeyShuffleRequirements[true]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.PoDShooterRoom,
                        DungeonItemID.PoDMapChest,
                        DungeonItemID.PoDArenaLedge,
                        DungeonItemID.PoDBigKeyChest,
                        DungeonItemID.PoDStalfosBasement,
                        DungeonItemID.PoDArenaBridge
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.PoDShooterRoom,
                                DungeonItemID.PoDMapChest,
                                DungeonItemID.PoDArenaLedge,
                                DungeonItemID.PoDBigKeyChest,
                                DungeonItemID.PoDStalfosBasement,
                                DungeonItemID.PoDArenaBridge
                            },
                            true, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    6, new List<DungeonItemID>
                                    {
                                        DungeonItemID.PoDShooterRoom,
                                        DungeonItemID.PoDMapChest,
                                        DungeonItemID.PoDArenaLedge,
                                        DungeonItemID.PoDBigKeyChest,
                                        DungeonItemID.PoDStalfosBasement,
                                        DungeonItemID.PoDArenaBridge,
                                        DungeonItemID.PoDCompassChest,
                                        DungeonItemID.PoDDarkBasementLeft,
                                        DungeonItemID.PoDDarkBasementRight,
                                        DungeonItemID.PoDHarmlessHellway
                                    },
                                    true, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    _bigKeyShuffleRequirements[false]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.PoDCompassChest,
                        DungeonItemID.PoDDarkBasementLeft,
                        DungeonItemID.PoDDarkBasementRight,
                        DungeonItemID.PoDHarmlessHellway
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.PoDShooterRoom,
                                DungeonItemID.PoDMapChest,
                                DungeonItemID.PoDArenaLedge,
                                DungeonItemID.PoDBigKeyChest,
                                DungeonItemID.PoDStalfosBasement,
                                DungeonItemID.PoDArenaBridge
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    6, new List<DungeonItemID>
                                    {
                                        DungeonItemID.PoDShooterRoom,
                                        DungeonItemID.PoDMapChest,
                                        DungeonItemID.PoDArenaLedge,
                                        DungeonItemID.PoDBigKeyChest,
                                        DungeonItemID.PoDStalfosBasement,
                                        DungeonItemID.PoDArenaBridge,
                                        DungeonItemID.PoDCompassChest,
                                        DungeonItemID.PoDDarkBasementLeft,
                                        DungeonItemID.PoDDarkBasementRight,
                                        DungeonItemID.PoDHarmlessHellway
                                    },
                                    true, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    _bigKeyShuffleRequirements[false]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.PoDDarkMazeTop,
                        DungeonItemID.PoDDarkMazeBottom
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.PoDShooterRoom,
                                DungeonItemID.PoDMapChest,
                                DungeonItemID.PoDArenaLedge,
                                DungeonItemID.PoDBigKeyChest,
                                DungeonItemID.PoDStalfosBasement,
                                DungeonItemID.PoDArenaBridge
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    6, new List<DungeonItemID>
                                    {
                                        DungeonItemID.PoDShooterRoom,
                                        DungeonItemID.PoDMapChest,
                                        DungeonItemID.PoDArenaLedge,
                                        DungeonItemID.PoDBigKeyChest,
                                        DungeonItemID.PoDStalfosBasement,
                                        DungeonItemID.PoDArenaBridge,
                                        DungeonItemID.PoDCompassChest,
                                        DungeonItemID.PoDDarkBasementLeft,
                                        DungeonItemID.PoDDarkBasementRight,
                                        DungeonItemID.PoDHarmlessHellway
                                    },
                                    false, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    _bigKeyShuffleRequirements[false])
            };
        }
    }
}