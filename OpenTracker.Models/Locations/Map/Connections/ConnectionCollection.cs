﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Connections;

namespace OpenTracker.Models.Locations.Map.Connections
{
    /// <summary>
    ///     This class contains the collection container for map connections.
    /// </summary>
    public class ConnectionCollection : ObservableCollection<IConnection>, IConnectionCollection
    {
        private readonly ILocationDictionary _locations;

        private readonly IConnection.Factory _connectionFactory;
        private readonly IAddConnection.Factory _addConnectionFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="locations">
        ///     The location dictionary.
        /// </param>
        /// <param name="connectionFactory">
        ///     An Autofac factory for creating new connections.
        /// </param>
        /// <param name="addConnectionFactory">
        ///     An Autofac factory for creating new add connection undoable actions.
        /// </param>
        public ConnectionCollection(
            ILocationDictionary locations, IConnection.Factory connectionFactory,
            IAddConnection.Factory addConnectionFactory)
        {
            _locations = locations;
            _connectionFactory = connectionFactory;
            _addConnectionFactory = addConnectionFactory;
        }

        public IUndoable AddConnection(IMapLocation location1, IMapLocation location2)
        {
            return _addConnectionFactory(_connectionFactory(location1, location2));
        }

        /// <summary>
        ///     Returns a list of connection save data.
        /// </summary>
        /// <returns>
        ///     A list of connection save data.
        /// </returns>
        public IList<ConnectionSaveData> Save()
        {
            return this.Select(connection => connection.Save()).ToList();
        }

        /// <summary>
        ///     Loads a list of connection save data.
        /// </summary>
        /// <param name="saveData">
        ///     A list of connection save data.
        /// </param>
        public void Load(IList<ConnectionSaveData>? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            Clear();

            foreach (var connection in saveData)
            {
                Add(_connectionFactory(
                    _locations[connection.Location1].MapLocations[connection.Index1],
                    _locations[connection.Location2].MapLocations[connection.Index2]));
            }
        }
    }
}
