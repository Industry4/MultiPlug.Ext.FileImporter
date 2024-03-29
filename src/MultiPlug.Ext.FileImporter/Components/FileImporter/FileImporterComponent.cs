﻿using System;
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
