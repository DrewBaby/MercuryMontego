using System;
using IGDB.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameClubProject.Models
{
    public class GameGenres
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int gameGenre_id { get; set; }
        public int gameId { get; set; }
        public int genre_id { get; set; }

        public Genre genre { get; set; }
        public Game game { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;

    }
}
