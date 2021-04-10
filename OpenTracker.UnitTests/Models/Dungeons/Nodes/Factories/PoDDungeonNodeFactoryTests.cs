// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Autofac;
// using NSubstitute;
// using OpenTracker.Models.Dungeons.KeyDoors;
// using OpenTracker.Models.Dungeons.Mutable;
// using OpenTracker.Models.Dungeons.Nodes;
// using OpenTracker.Models.Dungeons.Nodes.Factories;
// using OpenTracker.Models.NodeConnections;
// using OpenTracker.Models.Nodes;
// using OpenTracker.Models.Requirements;
// using Xunit;
//
// namespace OpenTracker.UnitTests.Models.Dungeons.Nodes.Factories
// {
//     public class PoDDungeonNodeFactoryTests
//     {
//         private readonly IRequirementFactory _requirementFactory = Substitute.For<IRequirementFactory>();
//         private readonly IOverworldNodeFactory _overworldNodeFactory = Substitute.For<IOverworldNodeFactory>();
//         
//         private readonly IRequirementDictionary _requirements;
//         private readonly IOverworldNodeDictionary _overworldNodes;
//
//         private readonly List<INode> _entryFactoryCalls = new();
//         private readonly List<(INode fromNode, INode toNode, IRequirement requirement)> _connectionFactoryCalls = new();
//
//         private readonly PoDDungeonNodeFactory _sut;
//
//         private static readonly Dictionary<DungeonNodeID, OverworldNodeID> ExpectedEntryValues = new();
//         private static readonly Dictionary<DungeonNodeID, List<
//             (DungeonNodeID fromNodeID, RequirementType requirementType)>> ExpectedConnectionValue = new();
//         private static readonly Dictionary<DungeonNodeID, List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>>
//             ExpectedKeyDoorValues = new();
//
//         public PoDDungeonNodeFactoryTests()
//         {
//             _requirements = new RequirementDictionary(() => _requirementFactory);
//             _overworldNodes = new OverworldNodeDictionary(() => _overworldNodeFactory);
//
//             IEntryNodeConnection EntryFactory(INode fromNode)
//             {
//                 _entryFactoryCalls.Add(fromNode);
//                 return Substitute.For<IEntryNodeConnection>();
//             }
//
//             INodeConnection ConnectionFactory(INode fromNode, INode toNode, IRequirement requirement)
//             {
//                 _connectionFactoryCalls.Add((fromNode, toNode, requirement));
//                 return Substitute.For<INodeConnection>();
//             }
//
//             _sut = new PoDDungeonNodeFactory(_overworldNodes, EntryFactory, ConnectionFactory);
//         }
//
//         private static void PopulateExpectedValues()
//         {
//             ExpectedEntryValues.Clear();
//             ExpectedConnectionValue.Clear();
//             ExpectedKeyDoorValues.Clear();
//             
//             foreach (DungeonNodeID id in Enum.GetValues(typeof(DungeonNodeID)))
//             {
//                 switch (id)
//                 {
//                     case DungeonNodeID.PoD:
//                         ExpectedEntryValues.Add(id, OverworldNodeID.PoDEntry);
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.PoDLobbyArena, KeyDoorID.PoDFrontKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.PoDPastFirstRedGoriyaRoom:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoD, RequirementType.RedEyegoreGoriya),
//                                 (DungeonNodeID.PoD, RequirementType.CameraUnlock),
//                                 (DungeonNodeID.PoD, RequirementType.MimicClip)
//                             });
//                         break;
//                     case DungeonNodeID.PoDFrontKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoD, RequirementType.NoRequirement),
//                                 (DungeonNodeID.PoDLobbyArena, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.PoDLobbyArena:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDPastFirstRedGoriyaRoom, RequirementType.Hammer)
//                             });
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.PoD, KeyDoorID.PoDFrontKeyDoor),
//                                 (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, KeyDoorID.PoDCollapsingWalkwayKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.PoDBigKeyChestArea:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.PoDLobbyArena, KeyDoorID.PoDBigKeyChestKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.PoDCollapsingWalkwayKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDLobbyArena, RequirementType.NoRequirement),
//                                 (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.PoDLobbyArena, KeyDoorID.PoDCollapsingWalkwayKeyDoor),
//                                 (DungeonNodeID.PoDPastDarkMazeKeyDoor, KeyDoorID.PoDDarkMazeKeyDoor),
//                                 (DungeonNodeID.PoDHarmlessHellwayRoom, KeyDoorID.PoDHarmlessHellwayKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.PoDDarkBasement:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, RequirementType.DarkRoomPoDDarkBasement)
//                             });
//                         break;
//                     case DungeonNodeID.PoDHarmlessHellwayKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, RequirementType.NoRequirement),
//                                 (DungeonNodeID.PoDHarmlessHellwayRoom, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.PoDHarmlessHellwayRoom:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, KeyDoorID.PoDHarmlessHellwayKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.PoDDarkMazeKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, RequirementType.NoRequirement),
//                                 (DungeonNodeID.PoDPastDarkMazeKeyDoor, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.PoDPastDarkMazeKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDDarkMaze, RequirementType.NoRequirement)
//                             });
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, KeyDoorID.PoDDarkMazeKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.PoDDarkMaze:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDPastDarkMazeKeyDoor, RequirementType.DarkRoomPoDDarkMaze),
//                                 (DungeonNodeID.PoDBigChestLedge, RequirementType.DarkRoomPoDDarkMaze)
//                             });
//                         break;
//                     case DungeonNodeID.PoDBigChestLedge:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDDarkMaze, RequirementType.NoRequirement),
//                                 (DungeonNodeID.PoDPastCollapsingWalkwayKeyDoor, RequirementType.BombJumpPoDHammerJump)
//                             });
//                         break;
//                     case DungeonNodeID.PoDBigChest:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.PoDBigChestLedge, KeyDoorID.PoDBigChest)
//                             });
//                         break;
//                     case DungeonNodeID.PoDPastSecondRedGoriyaRoom:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDLobbyArena, RequirementType.RedEyegoreGoriya),
//                                 (DungeonNodeID.PoDLobbyArena, RequirementType.MimicClip)
//                             });
//                         break;
//                     case DungeonNodeID.PoDPastBowStatue:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDPastSecondRedGoriyaRoom, RequirementType.Bow)
//                             });
//                         break;
//                     case DungeonNodeID.PoDBossAreaDarkRooms:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDPastBowStatue, RequirementType.DarkRoomPoDBossArea)
//                             });
//                         break;
//                     case DungeonNodeID.PoDPastHammerBlocks:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDBossAreaDarkRooms, RequirementType.Hammer)
//                             });
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.PoDPastBossAreaKeyDoor, KeyDoorID.PoDBossAreaKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.PoDBossAreaKeyDoor:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDPastHammerBlocks, RequirementType.NoRequirement),
//                                 (DungeonNodeID.PoDPastBossAreaKeyDoor, RequirementType.NoRequirement)
//                             });
//                         break;
//                     case DungeonNodeID.PoDPastBossAreaKeyDoor:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.PoDPastHammerBlocks, KeyDoorID.PoDBossAreaKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.PoDBossRoom:
//                         ExpectedKeyDoorValues.Add(id,
//                             new List<(DungeonNodeID fromNodeID, KeyDoorID keyDoor)>
//                             {
//                                 (DungeonNodeID.PoDPastBossAreaKeyDoor, KeyDoorID.PoDBigKeyDoor)
//                             });
//                         break;
//                     case DungeonNodeID.PoDBoss:
//                         ExpectedConnectionValue.Add(id,
//                             new List<(DungeonNodeID fromNodeID, RequirementType requirementType)>
//                             {
//                                 (DungeonNodeID.PoDBossRoom, RequirementType.PoDBoss)
//                             });
//                         break;
//                 }
//             }
//         }
//
//         [Fact]
//         public void PopulateNodeConnections_ShouldThrowException_WhenNodeIDIsUnexpected()
//         {
//             var dungeonData = Substitute.For<IMutableDungeon>();
//             var node = Substitute.For<IDungeonNode>();
//             var connections = new List<INodeConnection>();
//             const DungeonNodeID id = (DungeonNodeID)int.MaxValue;
//
//             Assert.Throws<ArgumentOutOfRangeException>(() =>
//                 _sut.PopulateNodeConnections(dungeonData, id, node, connections));
//         }
//         
//         [Theory]
//         [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedEntryConnectionsData))]
//         public void PopulateNodeConnections_ShouldCreateExpectedEntryConnections(
//             DungeonNodeID id, OverworldNodeID fromNodeID)
//         {
//             var dungeonData = Substitute.For<IMutableDungeon>();
//             var node = Substitute.For<IDungeonNode>();
//             var connections = new List<INodeConnection>();
//             _sut.PopulateNodeConnections(dungeonData, id, node, connections);
//
//             Assert.Contains(_entryFactoryCalls, x => x == _overworldNodes[fromNodeID]);
//         }
//
//         public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedEntryConnectionsData()
//         {
//             PopulateExpectedValues();
//
//             return ExpectedEntryValues.Keys.Select(id => new object[] {id, ExpectedEntryValues[id]}).ToList();
//         }
//
//         [Theory]
//         [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedConnectionsData))]
//         public void PopulateNodeConnections_ShouldCreateExpectedConnections(
//             DungeonNodeID id, DungeonNodeID fromNodeID, RequirementType requirementType)
//         {
//             var dungeonData = Substitute.For<IMutableDungeon>();
//             var node = Substitute.For<IDungeonNode>();
//             var connections = new List<INodeConnection>();
//             _sut.PopulateNodeConnections(dungeonData, id, node, connections);
//
//             Assert.Contains(_connectionFactoryCalls, x =>
//                 x.fromNode == dungeonData.Nodes[fromNodeID] && x.requirement == _requirements[requirementType]);
//         }
//
//         public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedConnectionsData()
//         {
//             PopulateExpectedValues();
//
//             return (from id in ExpectedConnectionValue.Keys from value in ExpectedConnectionValue[id]
//                 select new object[] {id, value.fromNodeID, value.requirementType}).ToList();
//         }
//
//         [Theory]
//         [MemberData(nameof(PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnectionsData))]
//         public void PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnections(
//             DungeonNodeID id, DungeonNodeID fromNodeID, KeyDoorID keyDoor)
//         {
//             var dungeonData = Substitute.For<IMutableDungeon>();
//             var node = Substitute.For<IDungeonNode>();
//             var connections = new List<INodeConnection>();
//             _sut.PopulateNodeConnections(dungeonData, id, node, connections);
//
//             Assert.Contains(_connectionFactoryCalls, x =>
//                 x.fromNode == dungeonData.Nodes[fromNodeID] &&
//                 x.requirement == dungeonData.KeyDoors[keyDoor].Requirement);
//         }
//         
//         public static IEnumerable<object[]> PopulateNodeConnections_ShouldCreateExpectedKeyDoorConnectionsData()
//         {
//             PopulateExpectedValues();
//
//             return (from id in ExpectedKeyDoorValues.Keys from value in ExpectedKeyDoorValues[id]
//                 select new object[] {id, value.fromNodeID, value.keyDoor}).ToList();
//         }
//
//         [Fact]
//         public void AutofacTest()
//         {
//             using var scope = ContainerConfig.Configure().BeginLifetimeScope();
//             var sut = scope.Resolve<IPoDDungeonNodeFactory>();
//             
//             Assert.NotNull(sut as PoDDungeonNodeFactory);
//         }
//     }
// }