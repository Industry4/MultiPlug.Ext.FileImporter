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
                        Guid = Search.Guid
                    },
                    Template = Templates.SettingsFile
                };
            }
        }
    }
}
