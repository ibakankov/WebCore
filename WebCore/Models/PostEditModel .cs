using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Models
{
    public class PostEditModel
    {
        public string Text { get; set; } = string.Empty;

        public int AuthorId { get; set; }

        public string[] Tags { get; set; }
    }
}
