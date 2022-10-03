using MultiPlug.Base.Http;
using MultiPlug.Extension.Core.Attribute;

namespace MultiPlug.Ext.FileImporter.Controllers.Apps.FileImporter
{
    [HttpEndpointType(HttpEndpointType.App)]
    [ViewAs(ViewAs.Partial)]
    [Name("File Importer")]
    public class FileImporterApp : Controller
    {
    }
}
