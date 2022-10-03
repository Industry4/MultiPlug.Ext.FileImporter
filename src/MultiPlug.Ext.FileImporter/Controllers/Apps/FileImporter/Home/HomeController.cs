using System.Linq;
using MultiPlug.Base.Http;
using MultiPlug.Ext.FileImporter.Controllers.Shared;

namespace MultiPlug.Ext.FileImporter.Controllers.Apps.FileImporter.Home
{
    public class HomeController : FileImporterApp
    {
        public HomeController()
        {
        }

        public Response Get()
        {
            return new Response
            {
                Model = new Models.Apps.FileImporter.Home.Home
                {
                    Guid = Core.Instance.Files.Select(File => File.Guid).ToArray(),
                    Type = Core.Instance.Files.Select(File => File.Type).ToArray()
                },
                Template = Templates.AppFileImporterHome
            };
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
                        Search.Import(theFiles.Files[0].Path);
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
