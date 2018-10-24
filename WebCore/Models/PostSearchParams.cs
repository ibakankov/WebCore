using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Models
{
    public class PostSearchParams
    {
        public string[] Tags { get; set; }
        public int[] Authors { get; set; }
        public string Content { get; set; }
    }
}
