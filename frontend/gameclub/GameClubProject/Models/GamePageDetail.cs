using IGDB.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System;

namespace GameClubProject.Models
{
    public class GamePageDetail : Game
    {
        public Game game { get; set; }
        public List<GameGenres> game_Genres { get; set; }
        public List<GamePlatforms> game_Platforms { get; set; }
        public List<Review> Reviews { get; set; }
    }



    public class Gamer : Game 
     {
        [JsonProperty(PropertyName = "id")]
        [Key]
        public int gameId { get; set; }
        public string name { get; set; }
        public double rating { get; set; }
        public double popularity { get; set; }
        public string summary { get; set; }        
        public int user_id { get; set; }
        public Cover cover { get; set; }
        [NotMapped]
        public double first_release_date { get; set; }
        //public DateTime release_date { get; set; }
        public List<Screenshot> screenshots { get; set; }
        public List<GameVideo> videos { get; set; }
        [NotMapped]
        public List<Genre> genres { get; set; }
        [NotMapped]
        public List<Platform> platforms { get; set; }
        public List<Expansion> expansions { get; set; }
        [NotMapped]
        public List<InvolvedCompany> involved_companies { get; set; }
        // Many to Many Relationship
        public List<GamePlatforms> game_Platforms { get; set; }
        public List<GameGenres> game_Genres { get; set; }
        public List<GameCompanies> game_Companies { get; set; }
        public List<Review> Reviews { get; set; }
    }

    public class GameCompanies : Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int gameCompany_id { get; set; }
        public int company_id { get; set; }
        public int gameId { get; set; }
        public Company company { get; set; }
        public Game game { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;


    }


    public class Expansion
    {
        [JsonProperty(PropertyName = "id")]
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public ExpansionCover cover { get; set; }
        public int gameId { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;

    }


    public class ExpansionCover
    {
        [JsonProperty(PropertyName = "id")]
        [Key]
        public int expansionCover_id { get; set; }
        public int gameId { get; set; }
        public int expansion_id { get; set; }
        public string image_id { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;

    }


    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int review_id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Review should be atleast 5 characters long")]
        [MaxLength(200, ErrorMessage = "Review exceeded the 200 character limit")]
        [Display(Name = "Review")]
        public string text { get; set; }

        public int user_id { get; set; }
        public int gameId { get; set; }
        public UserAccount reviewer { get; set; }
        public Game reviewedGames { get; set; }
        public List<Like> likeCounts { get; set; }
        public List<ReviewResponse> responses { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;


    }
    public class ReviewResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reviewResponse_id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Comment should be atleast 5 characters long")]
        [MaxLength(100, ErrorMessage = "Comment exceeded the 100 character limit")]
        [Display(Name = "Comment")]
        public string response { get; set; }

        public int user_id { get; set; }
        public int review_id { get; set; }
        public UserAccount respondent { get; set; }
        public Review theReview { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;


    }

    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int like_id { get; set; }
        public int user_id { get; set; }
        public int review_id { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;


    }

}

