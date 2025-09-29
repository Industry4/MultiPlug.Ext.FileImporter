using System;
using System.Collections.Generic;
using MultiPlug.Ext.FileImporter.Components.FileImporter.Util;
using MultiPlug.Ext.FileImporter.Models.Components.FileImporter;
using MultiPlug.Base.Exchange;

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
            bool EventChanges = false;

            if (theNewProperties == null)
            {
                return;
            }
            if (theNewProperties.Guid != Guid)
            {
                return;
            }
            if (theNewProperties.RowEvent != null && Event.Merge(RowEvent, theNewProperties.RowEvent, true))
            {
                EventChanges = true;
            }
            if (theNewProperties.Type != null && theNewProperties.Type != Type)
            {
                Type = theNewProperties.Type;
                RowEvent.Description = "Row of the file type " + Type;
                EventChanges = true;
            }
            if (theNewProperties.Skip != null && theNewProperties.Skip != Skip)
            {
                Skip = theNewProperties.Skip;
            }

            if(EventChanges)
            {
                EventUpdated?.Invoke();
            }
        }

        internal void Import(string thePath)
        {
            using (var CsvTextFieldParser = new CsvTextFieldParser(thePath))
            {
                if (thePath.EndsWith(".tsv", StringComparison.OrdinalIgnoreCase))
                {
                    CsvTextFieldParser.SetDelimiter('\t');
                }
                else if (thePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    CsvTextFieldParser.SetDelimiter(',');
                }
                else
                {
                    return;
                }

                int skipNumber = Skip.Value;

                string[] Columns = CsvTextFieldParser.ReadFields();

                while(Columns != null)
                {
                    if (skipNumber != 0)
                    {
                        skipNumber--;
                        Columns = CsvTextFieldParser.ReadFields();
                        continue;
                    }

                    List<PayloadSubject> PayloadSubjects = new List<PayloadSubject>();

                    for (int i = 0; i < Columns.Length; i++)
                    {
                        if (i <= RowEvent.Subjects.Length)
                        {
                            PayloadSubjects.Add(new PayloadSubject(RowEvent.Subjects[i], Columns[i]));
                        }
                    }

                    if (RowEvent.Subjects.Length > Columns.Length)
                    {
                        int Difference = RowEvent.Subjects.Length - Columns.Length;
                        for (int i = 0; i < Difference; i++)
                        {
                            PayloadSubjects.Add(new PayloadSubject(RowEvent.Subjects[Columns.Length + i], string.Empty));
                        }
                    }

                    RowEvent.Invoke(new Payload(RowEvent.Id, PayloadSubjects.ToArray()));

                    Columns = CsvTextFieldParser.ReadFields();
                }

            }
        }

        internal void ReadHeaders(string thePath)
        {
            using (var CsvTextFieldParser = new CsvTextFieldParser(thePath))
            {
                if (thePath.EndsWith(".tsv", StringComparison.OrdinalIgnoreCase))
                {
                    CsvTextFieldParser.SetDelimiter('\t');
                }
                else if (thePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    CsvTextFieldParser.SetDelimiter(',');
                }
                else
                {
                    return;
                }

                string[] Columns = CsvTextFieldParser.ReadFields();

                if (Columns != null)
                {
                    RowEvent.Subjects = Columns;
                    EventUpdated.Invoke();
                }
            }
        }
    }
}
