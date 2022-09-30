using MultiPlug.Base.Http;
using MultiPlug.Extension.Core.Attribute;

namespace MultiPlug.Ext.FileImporter.Controllers.Settings
{
    [Name("File Importer")]
    [HttpEndpointType(HttpEndpointType.Settings)]
    [ViewAs(ViewAs.Partial)]
    public class SettingsApp : Controller
    {
    }
}
