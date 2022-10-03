using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.FileImporter.Controllers.Settings.File
{
    [Route("file/delete")]
    public class FileDeleteController : SettingsApp
    {
        public Response Post(string id)
        {
            if( Core.Instance.DeleteFile(id) )
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.Moved,
                    Location = Context.Referrer
                };
            }
            else
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
        }
    }
}
