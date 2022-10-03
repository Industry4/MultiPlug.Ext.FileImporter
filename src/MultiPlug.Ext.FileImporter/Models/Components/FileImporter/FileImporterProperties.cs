using System.Runtime.Serialization;
using MultiPlug.Base.Exchange;

namespace MultiPlug.Ext.FileImporter.Models.Components.FileImporter
{
    public class FileImporterProperties
    {
        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public Event RowEvent { get; set; }

    }
}
