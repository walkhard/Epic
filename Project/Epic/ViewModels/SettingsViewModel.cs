using Epic.Models;

namespace Epic.ViewModels
{
    public class SettingsViewModel
    {
        public string Title { get; set; }

        public string MetaTitle { get; set; }

        public SettingsViewModel()
        {
        }

        public SettingsViewModel(EpicSettings settings)
        {
            Title = settings.Title;
            MetaTitle = settings.MetaTitle;
        }
    }
}