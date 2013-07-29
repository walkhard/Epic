using Epic.Models;

namespace Epic.Services
{
    public interface ISettingsService
    {
        /// <summary>
        /// Save the settings.
        /// </summary>
        /// <param name="settings">Settings.</param>
        void SaveSettings (EpicSettings settings);

        /// <summary>
        /// Get all settings.
        /// </summary>
        EpicSettings Get ();
    }
}