using System;
using System.ComponentModel.DataAnnotations.Schema;
using MarkdownSharp;

namespace Epic.Models
{
    public class Post
    {
        private readonly static Markdown md = new Markdown();

        public int Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        [NotMapped]
        public string ContentHtml 
        {
            get
            {
                return md.Transform (Content);
            }
        }
    }
}