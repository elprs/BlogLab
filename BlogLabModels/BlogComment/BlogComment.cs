using System;
using System.Collections.Generic;
using System.Text;

namespace BlogLabModels.BlogComment
{
    public class BlogComment : BlogCommentCreate
    {
        public string UserName { get; set; }

        public int ApplicationUserId { get; set; }

        public DateTime PublishDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
