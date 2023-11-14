using elasticTickets.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace elasticTickets.Models
{
    public class Movie
    {
        
        
        public string id { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string imageURL { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public List<Actor> movieActorsIds { get; set; }
        public string movieCinema { get; set; }
        public string movieProducer { get; set; }

        public MovieCategory movieCategory { get; set; }
        public List<Movie> movies { get; set; }


    }   
}
