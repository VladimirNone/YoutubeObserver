using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeTelegramBot.Domain.POCOs
{
    public class Video : Entity
    {
        public string name { get; set; }
        public bool not_interest { get; set; }
        public string channel_id { get; set; }
        public string[] tags { get; set; }
        public string url { get; set; }
        public bool watched { get; set; }
        public DateTime published { get; set; }

        [ForeignKey("channel_id")]
        public Channel AuthorOfChannel { get; set; }
    }
}
