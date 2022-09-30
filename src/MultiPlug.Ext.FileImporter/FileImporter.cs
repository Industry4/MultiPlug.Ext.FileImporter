using MultiPlug.Base.Exchange;
using MultiPlug.Ext.FileImporter.Controllers.Shared;
using MultiPlug.Ext.FileImporter.Properties;
using MultiPlug.Extension.Core;
using MultiPlug.Extension.Core.Http;

namespace MultiPlug.Ext.FileImporter
{
    public class FileImporter : MultiPlugExtension
    {
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
                    new RazorTemplate(Templates.SettingsHome, Resources.SettingsHome),
                };
            }
        }
    }
}
