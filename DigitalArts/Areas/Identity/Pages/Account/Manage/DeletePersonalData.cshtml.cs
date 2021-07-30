using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DigitalArts.Data;
using DigitalArts.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DigitalArts.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<Artist> userManager;
        private readonly SignInManager<Artist> signInManager;
        private readonly ILogger<DeletePersonalDataModel> logger;
        private readonly DigitalArtsDbContext data;

        public DeletePersonalDataModel(
            UserManager<Artist> userManager,
            SignInManager<Artist> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            DigitalArtsDbContext data)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.data = data;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
            }

            RequirePassword = await this.userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
            }

            RequirePassword = await this.userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await this.userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            var arts = this.data.Arts
                .Where(a => a.ArtistId == this.userManager.GetUserId(User))
                .Select(a => a)
                .ToList();

            foreach (var art in arts)
            {
                data.Remove(art);
            }

            await data.SaveChangesAsync();

            var result = await this.userManager.DeleteAsync(user);
            var userId = await this.userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await this.signInManager.SignOutAsync();

            this.logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}
