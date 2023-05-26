using System;
using System.Collections.Generic;
using MultiPlug.Ext.FileImporter.Models.Components.FileImporter;
using MultiPlug.Base.Exchange;
using System.Linq;

namespace MultiPlug.Ext.FileImporter.Components.FileImporter
{
    public class FileImporterComponent : FileImporterProperties
    {
        internal event Action EventUpdated;
        public FileImporterComponent(string theGuid)
        {
            Guid = theGuid;
            Type = "Unknown";
            Skip = 0;
            RowEvent = new Event { Guid = System.Guid.NewGuid().ToString(), Id = "RowRead-" + theGuid, Description = "Row of a File", Subjects = new string[0] };
        }
        internal void UpdateProperties(FileImporterProperties theNewProperties)
        {
            if (theNewProperties == null)
            {
                return;
            }
            if (theNewProperties.Guid != Guid)
            {
                return;
            }
            if (theNewProperties.Type != null && theNewProperties.Type != Type)
            {
                Type = theNewProperties.Type;
            }
            if (theNewProperties.RowEvent != null && Event.Merge(RowEvent, theNewProperties.RowEvent, true))
            {
                EventUpdated?.Invoke();
            }
            if (theNewProperties.Skip != null && theNewProperties.Skip != Skip)
            {
                Skip = theNewProperties.Skip;
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

            int skipNumber = Skip.Value;

            foreach (string FileLine in System.IO.File.ReadLines(thePath))
            {                
                if (skipNumber != 0)
                {
                    skipNumber--;
                    continue;
                }

                string[] Columns = FileLine.Split(Delimiter);

                List<PayloadSubject> PayloadSubjects = new List<PayloadSubject>();
                
                for (int i = 0; i < Columns.Length; i++)
                {
                    PayloadSubjects.Add(new PayloadSubject("Col" + (i + 1).ToString(), Columns[i]));
                }
                RowEvent.Invoke(new Payload(RowEvent.Id, PayloadSubjects.ToArray()));
            }
        }

        internal void ReadHeaders(string thePath)
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

            string Headings = System.IO.File.ReadLines(thePath).FirstOrDefault();

            if (Headings != null)
            {
                string[] Columns = Headings.Split(Delimiter);
               
                if (Columns.Length > 0)
                {
                    RowEvent.Subjects = Columns;
                    EventUpdated.Invoke();
                }
            }
        }
    }
}
