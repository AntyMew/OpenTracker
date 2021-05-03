﻿using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This class contains undoable action data for changing the key drop shuffle mode setting.
    /// </summary>
    public class ChangeKeyDropShuffle : IChangeKeyDropShuffle
    {
        private readonly IMode _mode;
        private readonly bool _newValue;
        private bool _previousValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="newValue">
        /// The new key drp shuffle setting.
        /// </param>
        public ChangeKeyDropShuffle(IMode mode, bool newValue)
        {
            _mode = mode;
            _newValue = newValue;
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
            _previousValue = _mode.KeyDropShuffle;
            _mode.KeyDropShuffle = _newValue;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.KeyDropShuffle = _previousValue;
        }
    }
}
