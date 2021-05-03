﻿using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo.Sections
{
    /// <summary>
    /// This class contains undoable action to uncollect a section.
    /// </summary>
    public class UncollectSection : IUncollectSection
    {
        private readonly ISection _section;

        private bool _previousUserManipulated;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The section data to be manipulated.
        /// </param>
        public UncollectSection(ISection section)
        {
            _section = section;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return _section.CanBeUncleared();
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _previousUserManipulated = _section.UserManipulated;
            _section.UserManipulated = true;
            _section.Available++;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _section.UserManipulated = _previousUserManipulated;
            _section.Available--;
        }
    }
}
