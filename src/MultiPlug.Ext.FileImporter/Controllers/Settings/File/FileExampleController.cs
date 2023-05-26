using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using System.Linq;

namespace MultiPlug.Ext.FileImporter.Controllers.Settings.File
{
    [Route("file/example")]
    public class FileExampleController : SettingsApp
    {
        public FileExampleController()
        {
        }

        public Response Post(string id, UploadFilePaths theFiles)
        {
            var Search = Core.Instance.Files.FirstOrDefault(File => File.Guid == id);

            if (Search != null)
            {
                if (theFiles.Files.Length > 0)
                {
                    try
                    {
                        Search.ReadHeaders(theFiles.Files[0].Path);
                    }
                    catch
                    {
                    }
                }
            }

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };
        }
    }
}
