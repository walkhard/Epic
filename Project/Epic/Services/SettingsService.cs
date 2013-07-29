using System;
using System.Collections.Generic;
using System.Linq;
using Epic.Database;
using Epic.Models;

namespace Epic.Services
{
    public class SettingsService : ISettingsService
    {
        private IEpicDatabase db;

        public SettingsService (IEpicDatabase db)
        {
            this.db = db;
        }

        public void SaveSettings (EpicSettings epicSettings)
        {
            IDictionary<string, string> settings = epicSettings.AsDictionary ();
            IEnumerable<Setting> dbSettings = db.Settings;
            // todo fix it somehow
            // todo enum settings
            foreach (var s in settings)
            {
                var setting = dbSettings.SingleOrDefault (i => i.Key == s.Key);
                if (setting == null)
                {
                    db.Settings.Add (new Setting {
                        Key = s.Key,
                        Value = s.Value
                    });
                }
                else
                {
                    setting.Value = s.Value;
                }
            }

            db.SaveChanges ();
        }

        public EpicSettings Get()
        {
            return new EpicSettings(db.Settings.ToDictionary(row => row.Key, row => row.Value));
        }
    }
}

