using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.FileImporter.Controllers.Shared;
using MultiPlug.Ext.FileImporter.Models.Components.FileImporter;

namespace MultiPlug.Ext.FileImporter.Controllers.Settings.Home
{
    [Route("")]
    public class HomeController : SettingsApp
    {
        public Response Get()
        {
            return new Response
            {
                Model = new Models.Settings.Home.Home
                {
                    Guid = Core.Instance.Files.Select( File => File.Guid ).ToArray(),
                    Type = Core.Instance.Files.Select( File => File.Type ).ToArray()
                },
                Template = Templates.SettingsHome
            };
        }

        public Response Post(Models.Settings.Home.Home theModel)
        {
            if(theModel.Type != null)
            {
                FileImporterProperties[] NewFiles = new FileImporterProperties[theModel.Type.Length];

                for (int i = 0; i < theModel.Type.Length; i++)
                {
                    NewFiles[i] = new FileImporterProperties { Type = theModel.Type[i] };
                }

                Core.Instance.Load(NewFiles);
            }

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };
        }
    }
}
