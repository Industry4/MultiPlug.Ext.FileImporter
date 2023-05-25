using MultiPlug.Base.Exchange;
using MultiPlug.Ext.FileImporter.Controllers.Shared;
using MultiPlug.Ext.FileImporter.Models.Load;
using MultiPlug.Ext.FileImporter.Properties;
using MultiPlug.Extension.Core;
using MultiPlug.Extension.Core.Http;
using System;

namespace MultiPlug.Ext.FileImporter
{
    public class FileImporter : MultiPlugExtension
    {
        internal event Action EventUpdated;
        public FileImporter()
        {
            Core.Instance.EventsUpdated += OnEventsUpdated;
        }

        public override Event[] Events
        {
            get
            {
                return Core.Instance.Events;
            }
        }

        private void OnEventsUpdated()
        {
            MultiPlugActions.Extension.Updates.Events();
        }

        public override RazorTemplate[] RazorTemplates
        {
            get
            {
                return new RazorTemplate[]
                {
                    new RazorTemplate(Templates.NotFound, Resources.NotFound),
                    new RazorTemplate(Templates.NavBar, Resources.SettingsNavBar),
                    new RazorTemplate(Templates.SettingsHome, Resources.SettingsHome),
                    new RazorTemplate(Templates.SettingsFile, Resources.SettingsFile),
                    new RazorTemplate(Templates.AppFileImporterHome, Resources.AppFileImporterHome),
                    new RazorTemplate(Templates.SettingsAbout, Resources.SettingsAbout),
                };
            }
        }

        public void Load(Root theConfig)
        {
            if(theConfig.Files != null)
            {
                Core.Instance.Load(theConfig.Files);
            }
        }

        public override object Save()
        {
            return Core.Instance;
        }
    }
}
