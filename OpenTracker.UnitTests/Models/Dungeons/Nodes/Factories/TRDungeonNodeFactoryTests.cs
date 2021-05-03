using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using NSubstitute;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Nodes.Factories;
using OpenTracker.Models.Items;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Nodes.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
{
    public class TRDungeonNodeFactoryTests
    {
        private readonly IBossRequirementDictionary _bossRequirements;
        private readonly IComplexRequirementDictionary _complexRequirements;
        private readonly IItemRequirementDictionary _itemRequirements;

        private readonly IOverworldNodeDictionary _overworldNodes;
        
        private readonly TRDungeonNodeFactory _sut;

        private readonly List<INode> _entryFactoryCalls = new();
        private readonly List<(INode fromNode, INode toNode, IRequirement? requirement)> _connectionFactoryCalls = new();

        private static readonly Dictionary<DungeonNodeID, List<OverworldNodeID>> ExpectedEntryValues = new();
        private static readonly Dictionary<DungeonNodeID, List<DungeonNodeID>> ExpectedNoRequirementValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, BossPlacementID bossID)>> ExpectedBossValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>> ExpectedComplexValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, ItemType type, int count)>> ExpectedItemValues = new();
        private static readonly Dictionary<DungeonNodeID,
            List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>> ExpectedKeyDoorValues = new();

        public TRDungeonNodeFactoryTests()
        {
            _bossRequirements = new BossRequirementDictionary(
                Substitute.For<IBossPlacementDictionary>(),
                _ => Substitute.For<IBossRequirement>());
            _complexRequirements = new ComplexRequirementDictionary(
                () => Substitute.For<IComplexRequirementFactory>());
            _itemRequirements = new ItemRequirementDictionary(
                Substitute.For<IItemDictionary>(), (_, _) => Substitute.For<IItemRequirement>());

            _overworldNodes = new OverworldNodeDictionary(() => Substitute.For<IOverworldNodeFactory>());

            IEntryNodeConnection EntryFactory(INode fromNode)
            {
                _entryFactoryCalls.Add(fromNode);
                return Substitute.For<IEntryNodeConnection>();
            }

            INodeConnection ConnectionFactory(INode fromNode, INode toNode, IRequirement? requirement)
            {
                _connectionFactoryCalls.Add((fromNode, toNode, requirement));
                return Substitute.For<INodeConnection>();
            }

            _sut = new TRDungeonNodeFactory(
                _bossRequirements, _complexRequirements, _itemRequirements, _overworldNodes, EntryFactory,
                ConnectionFactory);
        }

        private static void PopulateExpectedValues()
        {
            ExpectedEntryValues.Clear();
            ExpectedNoRequirementValues.Clear();
            ExpectedBossValues.Clear();
            ExpectedComplexValues.Clear();
            ExpectedItemValues.Clear();
            ExpectedKeyDoorValues.Clear();
            
            foreach (DungeonNodeID id in Enum.GetValues(typeof(DungeonNodeID)))
            {
                switch (id)
                {
                    case DungeonNodeID.TRFront:
                        ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.TRFrontEntry});
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRF1SomariaTrack
                        });
                        break;
                    case DungeonNodeID.TRF1SomariaTrack:
                        ExpectedItemValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                            {
                                (DungeonNodeID.TRFront, ItemType.CaneOfSomaria, 1),
                                (DungeonNodeID.TRF1CompassChestArea, ItemType.CaneOfSomaria, 1),
                                (DungeonNodeID.TRF1FourTorchRoom, ItemType.CaneOfSomaria, 1),
                                (DungeonNodeID.TRF1FirstKeyDoorArea, ItemType.CaneOfSomaria, 1)
                            });
                        break;
                    case DungeonNodeID.TRF1CompassChestArea:
                    case DungeonNodeID.TRF1FourTorchRoom:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRF1SomariaTrack
                        });
                        break;
                    case DungeonNodeID.TRF1RollerRoom:
                        ExpectedItemValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                            {
                                (DungeonNodeID.TRF1FourTorchRoom, ItemType.FireRod, 1)
                            });
                        break;
                    case DungeonNodeID.TRF1FirstKeyDoorArea:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRF1SomariaTrack
                        });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.TRF1PastFirstKeyDoor, KeyDoorID.TR1FFirstKeyDoor)
                            });
                        break;
                    case DungeonNodeID.TRF1FirstKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRF1FirstKeyDoorArea,
                            DungeonNodeID.TRF1PastFirstKeyDoor
                        });
                        break;
                    case DungeonNodeID.TRF1PastFirstKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.TRF1FirstKeyDoorArea, KeyDoorID.TR1FFirstKeyDoor),
                                (DungeonNodeID.TRF1PastSecondKeyDoor, KeyDoorID.TR1FSecondKeyDoor)
                            });
                        break;
                    case DungeonNodeID.TRF1SecondKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRF1PastFirstKeyDoor,
                            DungeonNodeID.TRF1PastSecondKeyDoor
                        });
                        break;
                    case DungeonNodeID.TRF1PastSecondKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB1
                        });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.TRF1PastFirstKeyDoor, KeyDoorID.TR1FSecondKeyDoor)
                            });
                        break;
                    case DungeonNodeID.TRB1:
                        ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.TRMiddleEntry});
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB1BigChestArea
                        });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.TRF1PastSecondKeyDoor, KeyDoorID.TR1FThirdKeyDoor),
                                (DungeonNodeID.TRB1RightSide, KeyDoorID.TRB1BigKeyDoor),
                                (DungeonNodeID.TRB1PastBigKeyChestKeyDoor, KeyDoorID.TRB1BigKeyChestKeyDoor)
                            });
                        break;
                    case DungeonNodeID.TRB1BigKeyChestKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB1,
                            DungeonNodeID.TRB1PastBigKeyChestKeyDoor
                        });
                        break;
                    case DungeonNodeID.TRB1PastBigKeyChestKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.TRB1, KeyDoorID.TRB1BigKeyChestKeyDoor)
                            });
                        break;
                    case DungeonNodeID.TRB1MiddleRightEntranceArea:
                        ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.TRMiddleEntry});
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB1
                        });
                        break;
                    case DungeonNodeID.TRB1BigChestArea:
                        ExpectedItemValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                            {
                                (DungeonNodeID.TRB1MiddleRightEntranceArea, ItemType.Hookshot, 1),
                                (DungeonNodeID.TRB1MiddleRightEntranceArea, ItemType.CaneOfSomaria, 1)
                            });
                        ExpectedComplexValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ComplexRequirementType type)>
                            {
                                (DungeonNodeID.TRB1MiddleRightEntranceArea, ComplexRequirementType.Hover)
                            });
                        break;
                    case DungeonNodeID.TRBigChest:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.TRB1BigChestArea, KeyDoorID.TRBigChest)
                            });
                        break;
                    case DungeonNodeID.TRB1BigKeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB1,
                            DungeonNodeID.TRB1RightSide
                        });
                        break;
                    case DungeonNodeID.TRB1RightSide:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRPastB1ToB2KeyDoor
                        });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.TRB1, KeyDoorID.TRB1BigKeyDoor)
                            });
                        break;
                    case DungeonNodeID.TRPastB1ToB2KeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB2DarkRoomTop
                        });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.TRB1RightSide, KeyDoorID.TRB1ToB2KeyDoor)
                            });
                        break;
                    case DungeonNodeID.TRB2DarkRoomTop:
                        ExpectedComplexValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                            {
                                (DungeonNodeID.TRPastB1ToB2KeyDoor, ComplexRequirementType.DarkRoomTR)
                            });
                        ExpectedItemValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                            {
                                (DungeonNodeID.TRB2DarkRoomBottom, ItemType.CaneOfSomaria, 1)
                            });
                        break;
                    case DungeonNodeID.TRB2DarkRoomBottom:
                        ExpectedItemValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                            {
                                (DungeonNodeID.TRB2DarkRoomTop, ItemType.CaneOfSomaria, 1)
                            });
                        ExpectedComplexValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                            {
                                (DungeonNodeID.TRB2PastDarkMaze, ComplexRequirementType.DarkRoomTR)
                            });
                        break;
                    case DungeonNodeID.TRB2PastDarkMaze:
                        ExpectedEntryValues.Add(id, new List<OverworldNodeID> {OverworldNodeID.TRBackEntry});
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB2DarkRoomBottom
                        });
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.TRB2PastKeyDoor, KeyDoorID.TRB2KeyDoor)
                            });
                        break;
                    case DungeonNodeID.TRLaserBridgeChests:
                        ExpectedComplexValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ComplexRequirementType requirementType)>
                            {
                                (DungeonNodeID.TRB2PastDarkMaze, ComplexRequirementType.LaserBridge)
                            });
                        break;
                    case DungeonNodeID.TRB2KeyDoor:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                        {
                            DungeonNodeID.TRB2PastDarkMaze,
                            DungeonNodeID.TRB2PastKeyDoor
                        });
                        break;
                    case DungeonNodeID.TRB2PastKeyDoor:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.TRB2PastDarkMaze, KeyDoorID.TRB2KeyDoor)
                            });
                        break;
                    case DungeonNodeID.TRB3:
                        ExpectedNoRequirementValues.Add(id, new List<DungeonNodeID>
                            {
                                DungeonNodeID.TRB2PastKeyDoor
                            });
                        break;
                    case DungeonNodeID.TRB3BossRoomEntry:
                        ExpectedItemValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, ItemType type, int count)>
                            {
                                (DungeonNodeID.TRB3, ItemType.CaneOfSomaria, 1)
                            });
                        break;
                    case DungeonNodeID.TRBossRoom:
                        ExpectedKeyDoorValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
                            {
                                (DungeonNodeID.TRB3BossRoomEntry, KeyDoorID.TRBossRoomBigKeyDoor)
                            });
                        break;
                    case DungeonNodeID.TRBoss:
                        ExpectedBossValues.Add(id,
                            new List<(DungeonNodeID fromNodeID, BossPlacementID requirementType)>
                            {
                                (DungeonNodeID.TRBossRoom, BossPlacementID.TRBoss)
                            });
                        break;
                }
            }
        }

        [Fact]
        public void PopulateNodeConnections_ShouldThrowException_WhenNodeIDIsUnexpected()
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            const DungeonNodeID id = (DungeonNodeID)int.MaxValue;

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _sut.PopulateNodeConnections(dungeonData, id, node, connections));
        }
        
        [Theory]
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedEntryConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedEntryConnections(
            DungeonNodeID id, OverworldNodeID fromNodeID)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.Contains(_entryFactoryCalls, x => x == _overworldNodes[fromNodeID]);
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedEntryConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedEntryValues.Keys
                from value in ExpectedEntryValues[id]
                select new object[] {id, value}).ToList();
        }
        
        [Theory]
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedNoRequirementConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedNoRequirementConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.Contains(_connectionFactoryCalls, x =>
                x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == null);
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedNoRequirementConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedNoRequirementValues.Keys
                from value in ExpectedNoRequirementValues[id]
                select new object[] {id, value}).ToList();
        }

        [Theory]
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedBossConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedBossConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID, BossPlacementID bossID)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.Contains(_connectionFactoryCalls, x =>
                x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _bossRequirements[bossID]);
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedBossConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedBossValues.Keys from value in ExpectedBossValues[id]
                select new object[] {id, value.fromNodeID, value.bossID}).ToList();
        }

        [Theory]
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedComplexConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedComplexConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID, ComplexRequirementType type)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.Contains(_connectionFactoryCalls, x =>
                x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _complexRequirements[type]);
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedComplexConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedComplexValues.Keys from value in ExpectedComplexValues[id]
                select new object[] {id, value.fromNodeID, value.type}).ToList();
        }

        [Theory]
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedItemConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedItemConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID, ItemType type, int count)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.Contains(_connectionFactoryCalls, x =>
                x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _itemRequirements[(type, count)]);
        }

        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedItemConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedItemValues.Keys from value in ExpectedItemValues[id]
                select new object[] {id, value.fromNodeID, value.type, value.count}).ToList();
        }

        [Theory]
        [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnectionsData))]
        public void PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnections(
            DungeonNodeID id, DungeonNodeID fromNodeID, KeyDoorID keyDoor)
        {
            var dungeonData = Substitute.For<IMutableDungeon>();
            var node = Substitute.For<IDungeonNode>();
            var connections = new List<INodeConnection>();
            _sut.PopulateNodeConnections(dungeonData, id, node, connections);

            Assert.Contains(_connectionFactoryCalls, x =>
                x.fromNode == dungeonData.Nodes[fromNodeID] &&
                x.requirement == dungeonData.KeyDoors[keyDoor].Requirement);
        }
        
        public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnectionsData()
        {
            PopulateExpectedValues();

            return (from id in ExpectedKeyDoorValues.Keys from value in ExpectedKeyDoorValues[id]
                select new object[] {id, value.fromNodeID, value.keyDoor}).ToList();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<ITRDungeonNodeFactory>();
            
            Assert.NotNull(sut as TRDungeonNodeFactory);
        }
    }
}