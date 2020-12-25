using System;
using System.IO;
using EmailService;

namespace LoggingService
{
    public class EmailLogger: IEmailLogger
    {

        private readonly IEmailSender _sender;

        public EmailLogger(IEmailSender sender)
        {
            _sender = sender;
        }

        public void OnChanged(FileSystemEventArgs e)
        {
            var message = new Message(new string[] { "aorazbay09@gmail.com" }, "Changed", $"{e.FullPath} changed", e.FullPath);
            _sender.SendEmail(message);
        }

        public void OnCreated(FileSystemEventArgs e)
        {
            var message = new Message(new string[] { "aorazbay09@gmail.com" }, "Created", $"{e.FullPath} Created", e.FullPath);
            _sender.SendEmail(message);
        }

        public void OnDeleted(FileSystemEventArgs e)
        {
            var message = new Message(new string[] { "aorazbay09@gmail.com" }, "Deleted", $"{e.FullPath} DELETED", e.FullPath);
            _sender.SendEmail(message);
        }

        public void OnRenamed(RenamedEventArgs e)
        {
            var message = new Message(new string[] { "aorazbay09@gmail.com" }, "Renamed", $"{e.OldFullPath}   RENAMED TO   {e.FullPath}", e.FullPath);
            _sender.SendEmail(message);
        }
    }
}
