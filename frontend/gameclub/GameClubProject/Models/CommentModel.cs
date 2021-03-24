using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameClubProject.Models
{
    public class ReviewModel
    {        
        public string UserID { get; set; }
        public string GameID { get; set; }
        [MinLength(5)]
        [MaxLength(255)]
        public string UserReview { get; set; }

    }
}
