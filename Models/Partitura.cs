using System;
using System.ComponentModel.DataAnnotations;

namespace Partituras.Models
{
    public class Partitura
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Time {get; set;}
        public decimal Price { get; set; }
    }
}