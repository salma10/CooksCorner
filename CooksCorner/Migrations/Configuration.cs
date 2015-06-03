namespace CooksCorner.Migrations
{
    using CooksCorner.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<CooksCorner.Models.CooksCornerDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CooksCorner.Models.CooksCornerDatabaseContext context)
        {
            SeedMembership();
            //SeedFaq(context);
            //SeedGenre(context);
            //SeedVideRecipe(context);
            //SeedWrittebRecipe(context);
        }
        // For creating Admin user using seed data
        private void SeedMembership()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                    "UserProfile", "UserId", "UserName", autoCreateTables: true);
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;
            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("Root", false) == null)
            {
                membership.CreateUserAndAccount("Root", "11111111");
            }
            if (!roles.GetRolesForUser("Root").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "Root" }, new[] { "Admin" });
            }
        }

        private void SeedFaq(CooksCorner.Models.CooksCornerDatabaseContext context)
        {
            context.Faqs.AddOrUpdate(F => F.Question,
                            new Faq()
                            {
                                Question = "How to remove extra salt from chicken curry?",
                                Answer = "You can use vinegar or lemon to remove extra salt from chicken curry."
                            },
                            new Faq()
                            {
                                Question = "How do you get rid of excess chilies in Indian curry?",
                                Answer = "You may add some thick coconut milk, a little salt and a dash of lime juice if necessary."
                            }
                    );
        }// end of seedFaq

        private void SeedGenre(CooksCorner.Models.CooksCornerDatabaseContext context)
        {
            context.Genres.AddOrUpdate(G => G.GenreId,
                            new Genre()
                            {
                                GenreId = 1,
                                Name = "Banladeshi Recipes",
                                Description = "Almost all Bangladeshi recipes are available here"
                            },
                            new Genre()
                            {
                                GenreId = 2,
                                Name = "Indian Recipes",
                                Description = "Almost all Indian recipes are available here"
                            },
                           new Genre()
                           {
                               GenreId = 3,
                               Name = "Chinese Recipes",
                               Description = "Almost all Chinese recipes are available here"
                           },
                           new Genre()
                           {
                               GenreId = 4,
                               Name = "Thai Recipes",
                               Description = "Almost all Thai recipes are available here"
                           }
                    );
        }// end of Genre seed

        private void SeedVideRecipe(CooksCorner.Models.CooksCornerDatabaseContext context)
        {
            context.Videos.AddOrUpdate(V => V.Title,
                            new Video()
                            {
                                GenreId = 1,
                                Title = "Potato Balls Recipe",
                                Description = "Almost all Bangladeshi recipes are available here",
                                VideortUrl = "https://www.youtube.com/embed/nqXz8hhAYGo"
                            },
                            new Video()
                            {
                                GenreId = 2,
                                Title = "Chicken Curry Recipe",
                                Description = "TRADTIONAL INDIAN MARRIAGE RECIPES, Indian food cusine",
                                VideortUrl = "https://www.youtube.com/embed/8u1VR23Tj4A"
                            }

                    );
        }// end of Video Recipe seed

        private void SeedWrittebRecipe(CooksCorner.Models.CooksCornerDatabaseContext context)
        {
            context.WrittenTutorials.AddOrUpdate(R => R.Title,
                            new WrittenTutorial()
                            {
                                GenreId = 1,
                                Title = "Potato Balls Recipe",
                                Description = "Simple Meals dishes: how to make crispy balls of potato. Here, you have a homemade potatoes balls. They are very easy to make.",
                            },
                            new WrittenTutorial()
                            {
                                GenreId = 2,
                                Title = "Potato Balls Recipe",
                                Description = "Simple Meals dishes: how to make crispy balls of potato. Here, you have a homemade potatoes balls. They are very easy to make.",
                            }


                    );
        }// end of Video Recipe seed

    }
}

