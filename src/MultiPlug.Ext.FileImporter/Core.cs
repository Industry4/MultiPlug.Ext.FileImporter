using System;
using System.Linq;
using MultiPlug.Base;
using MultiPlug.Base.Exchange;
using MultiPlug.Ext.FileImporter.Models.Components.FileImporter;
using System.Runtime.Serialization;
using MultiPlug.Ext.FileImporter.Components.FileImporter;
using System.Collections.Generic;

namespace MultiPlug.Ext.FileImporter
{
    public class Core : MultiPlugBase
    {
        private static Core m_Instance = null;

        internal event Action EventsUpdated;
        internal Event[] Events { get; private set; }

        [DataMember]
        public FileImporterComponent[] Files { get; private set; } = new FileImporterComponent[0];

        public static Core Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Core();
                }
                return m_Instance;
            }
        }

        private Core()
        {
            Events = new Event[0];
        }

        internal void Load(FileImporterProperties[] theProperties)
        {
            if( theProperties == null)
            {
                return;
            }

            List<FileImporterComponent> NewImporters = null;

            foreach(FileImporterProperties Item in theProperties)
            {
                if(string.IsNullOrEmpty(Item.Guid))
                {
                    Item.Guid = Guid.NewGuid().ToString().Substring(0,8);
                    FileImporterComponent NewImporter = new FileImporterComponent(Item.Guid);
                    NewImporter.EventUpdated += AggregateEvents;
                    NewImporter.UpdateProperties(Item);

                    if( NewImporters == null)
                    {
                        NewImporters = new List<FileImporterComponent>();
                    }

                    NewImporters.Add(NewImporter);
                }
                else
                {
                    FileImporterComponent Search = Files.FirstOrDefault(File => File.Guid == Item.Guid);

                    if( Search == null)
                    {
                        FileImporterComponent NewImporter = new FileImporterComponent(Item.Guid);
                        NewImporter.EventUpdated += AggregateEvents;

                        NewImporter.UpdateProperties(Item);

                        if (NewImporters == null)
                        {
                            NewImporters = new List<FileImporterComponent>();
                        }

                        NewImporters.Add(NewImporter);
                    }
                    else
                    {
                        Search.UpdateProperties(Item);
                    }
                }
            }

            if(NewImporters != null)
            {
                List<FileImporterComponent> Files = this.Files.ToList();
                Files.AddRange(NewImporters);
                this.Files = Files.ToArray();
            }

            AggregateEvents();
        }

        internal bool DeleteFile(string theGuid)
        {
            FileImporterComponent Search = Files.FirstOrDefault(Lane => Lane.Guid == theGuid);

            if (Search == null)
            {
                return false;
            }
            else
            {
                Search.EventUpdated -= AggregateEvents;
                var FilesList = Files.ToList();
                FilesList.Remove(Search);
                Files = FilesList.ToArray();
                AggregateEvents();
                return true;
            }
        }

        private void AggregateEvents()
        {
            List<Event> FileEvents = new List<Event>();

            foreach(var item in Files)
            {
                FileEvents.Add(item.RowEvent);
            }

            Events = FileEvents.ToArray();

            EventsUpdated?.Invoke();
        }
    }
}
