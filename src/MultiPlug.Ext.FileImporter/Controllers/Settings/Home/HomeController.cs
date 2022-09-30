using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.FileImporter.Controllers.Shared;

namespace MultiPlug.Ext.FileImporter.Controllers.Settings.Home
{
    [Route("")]
    public class HomeController : SettingsApp
    {
        public Response Get()
        {
            return new Response
            {
                Model = new Models.Settings.Home.Home(),
                Template = Templates.SettingsHome
            };
        }
    }
}
