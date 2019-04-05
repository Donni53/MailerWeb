using MailKit;

namespace MailerWeb.Models
{
    public class MailFolder
    {
        public MailFolder(IMailFolder folder)
        {
            Id = folder.Id;
            Name = folder.Name;
            Count = folder.Count;
            Recent = folder.Recent;
            Unread = folder.Unread;
            Size = folder.Size;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Recent { get; set; }
        public int Unread { get; set; }
        public ulong? Size { get; set; }
    }
}