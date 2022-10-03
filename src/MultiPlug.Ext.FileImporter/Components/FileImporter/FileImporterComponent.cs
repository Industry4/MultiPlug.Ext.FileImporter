using System;
using System.Collections.Generic;
using MultiPlug.Ext.FileImporter.Models.Components.FileImporter;
using MultiPlug.Base.Exchange;

namespace MultiPlug.Ext.FileImporter.Components.FileImporter
{
    public class FileImporterComponent : FileImporterProperties
    {
        public FileImporterComponent(string theGuid)
        {
            Guid = theGuid;
            Type = "Unknown";

            RowEvent = new Event { Guid = System.Guid.NewGuid().ToString(), Id = "RowRead-" + theGuid, Description = "Row of a File", /*Subjects = new string[] { "json" }, Group = theGuid*/ };
        }
        internal void UpdateProperties(FileImporterProperties theNewProperties)
        {
            if(theNewProperties == null)
            {
                return;
            }

            if( theNewProperties.Guid != Guid)
            {
                return;
            }

            if(theNewProperties.Type != null && theNewProperties.Type != Type)
            {
                Type = theNewProperties.Type;
            }
        }

        internal void Import(string thePath)
        {
            char Delimiter;

            if (thePath.EndsWith(".tsv", StringComparison.OrdinalIgnoreCase))
            {
                Delimiter = '\t';
            }
            else if (thePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                Delimiter = ',';
            }
            else
            {
                return;
            }

            foreach (string FileLine in System.IO.File.ReadLines(thePath))
            {
                string[] Columns = FileLine.Split(Delimiter);

                List<PayloadSubject> PayloadSubjects = new List<PayloadSubject>();

                for (int i = 0; i < Columns.Length; i++)
                {
                    PayloadSubjects.Add(new PayloadSubject("Col" + (i + 1).ToString(), Columns[i]));
                }

                RowEvent.Invoke(new Payload(RowEvent.Id, PayloadSubjects.ToArray()));
            }
        }
    }
}
