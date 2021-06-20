using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeTelegramBot.Domain.POCOs
{
    public class Channel : Entity
    {
        public string name { get; set; }
        public short priority { get; set; } = 1;
        public DateTime? last_check { get; set; }

        [InverseProperty("AuthorOfChannel")]
        public List<Video> Videos { get; set; }
    }
}
