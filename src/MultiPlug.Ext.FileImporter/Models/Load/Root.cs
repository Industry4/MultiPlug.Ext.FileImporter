using System.Runtime.Serialization;
using MultiPlug.Ext.FileImporter.Models.Components.FileImporter;

namespace MultiPlug.Ext.FileImporter.Models.Load
{
    public class Root
    {
        [DataMember]
        public FileImporterProperties[] Files { get; set; }
    }
}
