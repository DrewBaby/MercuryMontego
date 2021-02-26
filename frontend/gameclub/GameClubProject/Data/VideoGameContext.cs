using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameClubProject.Models;

namespace GameClubProject.Data
{
    public class VideoGameContext : DbContext
    {
        public VideoGameContext(DbContextOptions<VideoGameContext> options) : base(options)
        {

        }

        public DbSet<VideoGame> VideoGame { get; set; }
        public DbSet<Character> Character { get; set; }
        public DbSet<Company> Company { get; set; }

    }
}
