using System;
using System.Collections.Generic;

#nullable disable

namespace BookTickets.Models
{
    public partial class Category
    {
        public Category()
        {
            Movies = new HashSet<Movie>();
        }

        public int IdCategory { get; set; }
        public string NameCategory { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
