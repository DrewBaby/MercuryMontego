using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace GameClubProject.Models
{
    public class RatingForm
    {
        [Required]
        [Display(Name = "Minimum Rating")]
        [Range(1, 100, ErrorMessage = "Min Rating should be bwtween 1-100")]
        public double MinRating { get; set; }

        [Required]
        [Display(Name = "Maximum Rating")]
        [Range(1, 100, ErrorMessage = " Max Rating should be bwtween 1-100")]
        public double MaxRating { get; set; }

    }
}
