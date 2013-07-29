using System.Collections.Generic;
using Epic.Services;

namespace Epic.Models
{
    /// <summary>
    /// Wrapper for settings.
    /// </summary>
    public class EpicSettings
    {
        private IDictionary<string, string> settings;

        public EpicSettings()
        {
            settings = new Dictionary<string, string> ();
        }

        public EpicSettings (IDictionary<string, string> settings)
        {
            this.settings = settings;
        }

        public EpicSettings (ISettingsService settingsService)
        {
            this.settings = settingsService.Get ().AsDictionary();
        }

        public string MetaTitle
        {
            get 
            { 
                string val;
                settings.TryGetValue ("MetaTitle", out val);
                return val;
            }

            set 
            { 
                settings ["MetaTitle"] = value; 
            }
        }

        public string Title
        {
            get 
            { 
                string val;
                settings.TryGetValue ("Title", out val);
                return val;
            }

            set 
            { 
                settings ["Title"] = value; 
            }
        }

        public IDictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>(settings);
        }
    }
}