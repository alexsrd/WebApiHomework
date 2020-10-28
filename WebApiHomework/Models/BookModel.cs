using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBookCatalog.Models
{
    public class BookModel
    {
        public string Genre { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int YearPublish { get; set; }
        public string PublisherName { get; set; }
    }
}
