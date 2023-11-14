using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace elasticTickets.Models
{
    public class Cinema
    {

        
        public string id{ get; set; }

        [Display(Name = "Cinema Logo")]
        //[Required(ErrorMessage = "Cinema logo is required")]
        public string logo { get; set; }

        [Display(Name = "Cinema Name")]
        //[Required(ErrorMessage = "Cinema name is required")]
        public string name { get; set; }

        [Display(Name = "Description")]
        //[Required(ErrorMessage = "Cinema description is required")]
        public string description { get; set; }

        public List<Cinema> cinemas{ get; set; }

    }
}
