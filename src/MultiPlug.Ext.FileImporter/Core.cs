using System;
using MultiPlug.Base;
using MultiPlug.Base.Exchange;

namespace MultiPlug.Ext.FileImporter
{
    public class Core : MultiPlugBase
    {
        private static Core m_Instance = null;

        internal event Action EventsUpdated;
        internal Event[] Events { get; private set; }

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
    }
}
