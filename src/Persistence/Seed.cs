using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>{
                    new AppUser { DisplayName = "Test1", UserName = "Test1", Email = "test1@gmail.com" },
                    new AppUser { DisplayName = "Fake 1", UserName = "Fake1", Email = "fake1@test.com" },
                    new AppUser { DisplayName = "Fake 2", UserName = "Fake2", Email = "fake2@test.com" }
                };

                foreach(var user in users){
                    await userManager.CreateAsync(user, "Password1234!");
                }
            }

            if (context.Activities.Any()) return;

            var activities = new List<Activity> 
            {
                new Activity
                {
                    Title = "Past Activity 1",
                    Date = DateTime.UtcNow.AddMonths(-2),
                    Description = "Activity 2 months ago",
                    Category = "drinks",
                    City = "London",
                    Venue = "Pub"
                },
                new Activity
                {
                    Title = "Past Activity 2",
                    Date = DateTime.UtcNow.AddMonths(-10),
                    Description = "Activity 10 months ago",
                    Category = "culture",
                    City = "Paris",
                    Venue = "Louvre"
                },
                new Activity
                {
                    Title = "Future Activity 2",
                    Date = DateTime.UtcNow.AddMonths(8),
                    Description = "Activity 8 months in future",
                    Category = "film",
                    City = "London",
                    Venue = "Cinema"
                }
            };

            await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}