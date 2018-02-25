using System;
using System.Collections.Generic;

namespace ForumApp.Models{
    public class TopicViewModel{
        public Topic topic { get; set; }
        public List<Posting> posting { get; set; }
    }
}