using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Exchange;
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
                        Description = Search.Type,
                        Headings = Search.RowEvent.Subjects
                    },
                    Template = Templates.SettingsFile
                };
            }
        }

        public Response Post(Models.Settings.File.File thePost)
        {
            var Search = Core.Instance.Files.FirstOrDefault(File => File.Guid == thePost.Guid);

            if (Search!= null)
            {
                Search.UpdateProperties(new Models.Components.FileImporter.FileImporterProperties
                {
                    Type = thePost.Description,
                    Guid = thePost.Guid,
                    RowEvent = new Event(Search.RowEvent.Guid, Search.RowEvent.Id, Search.RowEvent.Description, thePost.Headings)
                });
            }
            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };
        }
    }
}
