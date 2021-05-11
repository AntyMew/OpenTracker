﻿using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This class contains the <see cref="IUndoable"/> action to change the <see cref="IMode.SmallKeyShuffle"/>
    /// property.
    /// </summary>
    public class ChangeSmallKeyShuffle : IChangeSmallKeyShuffle
    {
        private readonly IMode _mode;
        private readonly bool _newValue;
        private bool _previousValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        ///     The <see cref="IMode"/> data.
        /// </param>
        /// <param name="newValue">
        ///     A <see cref="bool"/> representing the new <see cref="IMode.SmallKeyShuffle"/> value.
        /// </param>
        public ChangeSmallKeyShuffle(IMode mode, bool newValue)
        {
            _mode = mode;
            _newValue = newValue;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void ExecuteDo()
        {
            _previousValue = _mode.SmallKeyShuffle;
            _mode.SmallKeyShuffle = _newValue;
        }

        public void ExecuteUndo()
        {
            _mode.SmallKeyShuffle = _previousValue;
        }
    }
}
