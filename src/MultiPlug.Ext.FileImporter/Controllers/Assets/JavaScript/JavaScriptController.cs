using System.Text;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.FileImporter.Properties;

namespace MultiPlug.Ext.FileImporter.Controllers.Assets.JavaScript
{
    [Route("js/*")]
    public class JavaScriptController : AssetsController
    {
        public Response Get(string theName)
        {
            string Result = string.Empty;

            switch (theName)
            {
                case "files.js":
                    Result = Resources.FilesJs;
                    break;
                case "file.js":
                    Result = Resources.FileJs;
                    break;
                default:
                    break;
            }

            if (string.IsNullOrEmpty(Result))
            {
                return new Response { StatusCode = System.Net.HttpStatusCode.NotFound };
            }
            else
            {
                return new Response { MediaType = "text/javascript", RawBytes = Encoding.ASCII.GetBytes(Result) };
            }
        }
    }
}
