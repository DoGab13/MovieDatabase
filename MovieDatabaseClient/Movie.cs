using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabaseClient
{
    public class Movie
    {
        public string? Title { get; set; }
        public string? Star { get; set; }
        public string? Category { get; set; }
        public DateTime? Year { get; set; }
        public string? Type { get; set; }

        internal static Movie FromCsv(string line)
        {
            string[] splits = line.Split('|');
            Movie movie = new Movie();
            movie.Title = splits[0];
            movie.Star = splits[1];
            movie.Category = splits[2];
            movie.Type = splits[3];
            movie.Year = DateTime.Parse(splits[4]);
            return movie;
        }

        public string ToCsv()
        {
            return $"{Title}|{Star}|{Category}|{Type}|{Year}";
        }

        public override string ToString()
        {
            return $"{Star} - {Title}";
        }
        

    }

}

