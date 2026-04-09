using Microsoft.AspNetCore.Identity;
using BookShoppingCartMvc1.Constants;

namespace BookShoppingCartMvc1.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service) 
        {
            var userMgr = service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();
            //adding some roles to db
            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));

            //create admin user
            var adminUser = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            var userInDb = await userMgr.FindByEmailAsync(adminUser.Email);
            if (userInDb is null)
            {
                await userMgr.CreateAsync(adminUser, "Admin@123");
                await userMgr.AddToRoleAsync(adminUser, Roles.Admin.ToString());
            }



            

        }
    }
}
