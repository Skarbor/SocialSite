using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.Post
{
    public class Post
    {
        public int Id { get; set; }
        public ApplicationUser User  { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
