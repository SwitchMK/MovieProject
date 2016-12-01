using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models.Responses
{
    public class FeedbackResponse
    {
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfPublication { get; set; }
    }
}
