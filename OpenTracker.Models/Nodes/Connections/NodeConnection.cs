﻿using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.Models.Nodes.Connections
{
    /// <summary>
    ///     This class contains node connection data.
    /// </summary>
    public class NodeConnection : ReactiveObject, INodeConnection
    {
        private readonly INode _fromNode;
        private readonly INode _toNode;

        public IRequirement? Requirement { get; }
        
        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set => this.RaiseAndSetIfChanged(ref _accessibility, value);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="fromNode">
        ///     The node from which the connection originates.
        /// </param>
        /// <param name="toNode">
        ///     The node to which the connection belongs.
        /// </param>
        /// <param name="requirement">
        ///     The requirement for the connection to be accessible.
        /// </param>
        public NodeConnection(INode fromNode, INode toNode, IRequirement? requirement = null)
        {
            _fromNode = fromNode;
            _toNode = toNode;

            Requirement = requirement;

            _fromNode.PropertyChanged += OnNodeChanged;

            if (Requirement is not null)
            {
                Requirement.PropertyChanged += OnRequirementChanged;
            }

            UpdateAccessibility();
        }

        public AccessibilityLevel GetConnectionAccessibility(IList<INode> excludedNodes)
        {
            var requirement = Requirement?.Accessibility ?? AccessibilityLevel.Normal;

            if (requirement == AccessibilityLevel.None || _fromNode.Accessibility == AccessibilityLevel.None ||
                excludedNodes.Contains(_fromNode))
            {
                return AccessibilityLevel.None;
            }

            var newExcludedNodes = new List<INode>(excludedNodes) {_toNode};

            return AccessibilityLevelMethods.Min(requirement, _fromNode.GetNodeAccessibility(newExcludedNodes));
        }
        
        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IRequirementNode interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IOverworldNode.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        ///     Updates the Accessibility property.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = GetConnectionAccessibility(new List<INode>());
        }
    }
}
