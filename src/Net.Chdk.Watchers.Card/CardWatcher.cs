using System;
using System.Management;

namespace Net.Chdk.Watchers.Card
{
    public sealed class CardWatcher : IDisposable
    {
        private ManagementEventWatcher Watcher { get; set; }

        public void Initialize()
        {
            if (Watcher == null)
            {
                Watcher = new ManagementEventWatcher("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2 OR EventType = 3");
                Watcher.EventArrived += Watcher_EventArrived;
            }
        }

        public void Dispose()
        {
            if (Watcher != null)
            {
                Watcher.EventArrived -= Watcher_EventArrived;
                Watcher.Dispose();
                Watcher = null;
            }
        }

        public void Start()
        {
            Watcher.Start();
        }

        public void Stop()
        {
            Watcher.Stop();
        }

        /// <summary>
        /// Raised when a volume is added.
        /// </summary>
        /// <value>Drive letter.</value>
        public event EventHandler<string> CardAdded;

        /// <summary>
        /// Raised when a volume is removed.
        /// </summary>
        /// <value>Drive letter.</value>
        public event EventHandler<string> CardRemoved;

        private void Watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            var eventType = (ushort)e.NewEvent["EventType"];
            var driveLetter = (string)e.NewEvent["DriveName"];
            switch (eventType)
            {
                case 2:
                    CardAdded?.Invoke(this, driveLetter);
                    break;
                case 3:
                    CardRemoved?.Invoke(this, driveLetter);
                    break;
                default:
                    break;
            }
        }
    }
}
