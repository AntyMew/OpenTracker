﻿using OpenTracker.Models.Locations.Map.Connections;

namespace OpenTracker.Models.UndoRedo.Connections
{
    /// <summary>
    /// This class contains undoable action to remove a connection between two entrances.
    /// </summary>
    public class RemoveConnection : IRemoveConnection
    {
        private readonly IConnectionCollection _connections;
        private readonly IConnection _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connections">
        /// The connection collection.
        /// </param>
        /// <param name="connection">
        /// The connection to be removed.
        /// </param>
        public RemoveConnection(IConnectionCollection connections, IConnection connection)
        {
            _connections = connections;
            _connection = connection;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return true;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _connections.Remove(_connection);
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _connections.Add(_connection);
        }
    }
}
