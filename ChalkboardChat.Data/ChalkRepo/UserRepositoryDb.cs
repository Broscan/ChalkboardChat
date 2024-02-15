using Microsoft.AspNetCore.Identity;

namespace ChalkboardChat.Data.ChalkRepo;

public class UserRepositoryDb
{
    // Klasser som finns redan i AspNet.Core.Identitry
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public string Username { get; set; }
    public string Password { get; set; }

    // Konstrucktor för att hålla dessa props för användningen nere
    public UserRepositoryDb(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterUser(string username)
    {
        IdentityUser newUser = new IdentityUser
        {
            UserName = username
        };

        var createUserResult = await _userManager.CreateAsync(newUser, Password);

        // Om succedded
        if (createUserResult.Succeeded)
        {
            IdentityUser? userToLogIn = await _userManager.FindByNameAsync(Username);

            var signInResult = await _signInManager.PasswordSignInAsync(userToLogIn, Password, false, false);

            if (signInResult.Succeeded)
            {

            }
        }
        return createUserResult;
    }
}
