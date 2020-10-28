using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBookCatalog.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        public List<Genre> Genres { get; set; }
        public Catalog()
        {
            Genres = new List<Genre>();
        }

    }
}
