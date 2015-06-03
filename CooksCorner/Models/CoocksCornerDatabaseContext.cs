using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CooksCorner.Models
{
    public class CooksCornerDatabaseContext: DbContext
    {
        public DbSet<Video> Videos { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<WrittenTutorial> WrittenTutorials { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        
    }
}