using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data.Static;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //RecordLabels
                if (!context.RecordLabels.Any())
                {
                    context.RecordLabels.AddRange(new List<RecordLabel>()
                    {
                        new RecordLabel()
                            {
                                Name = "SBM Label",
                                Logo = "https://scontent-frx5-1.xx.fbcdn.net/v/t1.6435-9/156674007_3648338581870205_3416992955041150705_n.jpg?_nc_cat=100&ccb=1-5&_nc_sid=174925&_nc_ohc=UlU55AdvwVcAX_0cZl1&_nc_ht=scontent-frx5-1.xx&oh=00_AT9vp-mNWzv6Y6iPQs2nk4Vr67G5Ledbc6IVefOun0xm-Q&oe=623EAACD",
                                Description = "Polish hip-hop record label"
                            },
                        new RecordLabel()
                            {
                                Name = "StepRecords",
                                Logo = "https://scontent-frx5-1.xx.fbcdn.net/v/t1.6435-9/51593477_2472102519529248_7478098087416692736_n.png?_nc_cat=111&ccb=1-5&_nc_sid=09cbfe&_nc_ohc=vSYKdg48wrYAX_n90ZG&_nc_ht=scontent-frx5-1.xx&oh=00_AT93z1hdU0VaBMxSskLGuVxQlcHB62Pdn_fEspQRtq1vXw&oe=623CC5ED",
                                Description = "Polish hip-hop record label"
                            },
                    });
                    context.SaveChanges();


                }
                //Singers
                if (!context.Singers.Any())
                {
                    context.Singers.AddRange(new List<Singer>()
                    {
                        new Singer()
                            {
                                FullName = "Mata",
                                Bio = "Michał Matczak - Polish rapper, singer and songwriter",
                                ProfilePictureUrl = "https://sbmstore.pl/userdata/public/gfx/6618/6480-mata-digital-cover.jpg",
                            },
                        new Singer()
                            {
                                FullName = "Jan-Rapowanie",
                                Bio = "Jan Stanisław Pasula - Polish rapper and songwriter",
                                ProfilePictureUrl = "https://i1.sndcdn.com/artworks-000624345673-38mpn3-t500x500.jpg",
                            },
                    });
                    context.SaveChanges();

                }
                //Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                            {
                                FullName = "Francis",
                                Bio = "@prodbyfrancis_",
                                ProfilePictureUrl = "https://images.genius.com/4cea5a814d91c6e1985aa9b2b30fbeec.301x301x1.png",
                            },
                        new Producer()
                            {
                                FullName = "Dj Johny",
                                Bio = "@legendarny_djjohny",
                                ProfilePictureUrl = "https://images.genius.com/742d4a30efa0daeda3b5ab587a5df301.300x300x1.jpg",
                            },
                    });
                    context.SaveChanges();

                }
                //Songs
                if (!context.Songs.Any())
                {
                    context.Songs.AddRange(new List<Song>()
                    {
                        new Song()
                            {
                                Name = "SZAFIR",
                                Lyrics = "",
                                Price = 9.99,
                                ImageUrl = "https://t2.genius.com/unsafe/261x261/https%3A%2F%2Fimages.genius.com%2F24e6d8035c30107c1b8e497e389071c7.640x640x1.jpg",
                                FullName = "Mata - Szafir",
                                SongCategory = Models.Data.SongCategory.Rap,
                                RecordLabelId = 1,
                                ProducerId = 1

                        },
                        new Song()
                            {
                                Name = "TRYB SAMOLOTOWY",
                                Lyrics = "",
                                Price = 7.99,
                                ImageUrl = "https://images.genius.com/c92d785407eb9d94ba84e3e28b1f3df7.640x640x1.jpg",
                                FullName = "Jan-rapowanie - TRYB SAMOLOTOWY",
                                SongCategory = Models.Data.SongCategory.Rap,
                                RecordLabelId = 1,
                                ProducerId = 2
                            },
                    });
                    context.SaveChanges();

                }
                //Singers_Songs
                if (!context.Singers_Songs.Any())
                {
                    context.Singers_Songs.AddRange(new List<Singer_Song>()
                    {
                        new Singer_Song()
                            {
                                SingerId = 1,
                                SongId = 1
                            },
                        new Singer_Song()
                            {
                                SingerId = 2,
                                SongId = 2
                            },
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@music.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "@Admin241@");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@music.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "@User241@");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
