using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.FileImporter.Controllers.Shared;

namespace MultiPlug.Ext.FileImporter.Controllers.Settings.File
{
    [Route("file")]
    public class FileController : SettingsApp
    {
        public Response Get(string id)
        {
            var Search = Core.Instance.Files.FirstOrDefault(File => File.Guid == id);


            if (Search == null)
            {
                return new Response
                {
                    Template = Templates.NotFound
                };
            }
            else
            {
                return new Response
                {
                    Model = new Models.Settings.File.File
                    {
                        Guid = Search.Guid,
                        Description = Search.Type
                    },
                    Template = Templates.SettingsFile
                };
            }
        }

        public Response Post(Models.Settings.File.File thePost)
        {
            var Search = Core.Instance.Files.FirstOrDefault(File => File.Guid == thePost.Guid);


            if (Search == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.Moved,
                    Location = Context.Referrer
                };
            }
            else
            {
                Search.UpdateProperties(new Models.Components.FileImporter.FileImporterProperties { Type = thePost.Description , Guid = thePost.Guid});
                return new Response
                {
                    Model = new Models.Settings.File.File
                    {
                        Guid = Search.Guid,
                        Description = Search.Type
                    },
                    Template = Templates.SettingsFile
                };
            }
        }


    }
}
