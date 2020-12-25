using System;
using System.IO;

namespace LoggingService
{
    public interface IEmailLogger
    {
        void OnChanged(FileSystemEventArgs e);
        void OnDeleted(FileSystemEventArgs e);
        void OnRenamed(RenamedEventArgs e);
        void OnCreated(FileSystemEventArgs e);
    }
}
