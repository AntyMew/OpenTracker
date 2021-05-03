﻿using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo.Sections
{
    /// <summary>
    /// This class contains undoable action data to collect an item/entrance from a location section.
    /// </summary>
    public class CollectSection : ICollectSection
    {
        private readonly ISection _section;
        private readonly bool _force;
        private MarkType? _previousMarking;
        private bool _previousUserManipulated;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The section to be collected.
        /// </param>
        /// <param name="force">
        /// A boolean representing whether to obey the section logic.
        /// </param>
        public CollectSection(ISection section, bool force)
        {
            _section = section;
            _force = force;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return _section.CanBeCleared(_force);
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _previousMarking = _section.Marking?.Mark;

            _previousUserManipulated = _section.UserManipulated;
            _section.UserManipulated = true;
            _section.Available--;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _section.Available++;

            if (_previousMarking is not null && _section.Marking is not null)
            {
                _section.Marking.Mark = _previousMarking.Value;
            }

            _section.UserManipulated = _previousUserManipulated;
        }
    }
}
