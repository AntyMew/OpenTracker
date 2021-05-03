using System.ComponentModel;
using OpenTracker.ViewModels.Capture;

namespace OpenTracker.Models.Requirements.CaptureWindowOpen
{
    /// <summary>
    ///     This class contains capture window open requirement data.
    /// </summary>
    public class CaptureWindowOpenRequirement : BooleanRequirement, ICaptureWindowOpenRequirement
    {
        private readonly ICaptureWindowVM _captureWindow;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="captureWindow">
        ///     The capture window data.
        /// </param>
        public CaptureWindowOpenRequirement(ICaptureWindowVM captureWindow)
        {
            _captureWindow = captureWindow;

            _captureWindow.PropertyChanged += OnCaptureWindowChanged;
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the ICaptureWindow interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnCaptureWindowChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ICaptureWindowVM.IsOpen))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _captureWindow.IsOpen;
        }
    }
}