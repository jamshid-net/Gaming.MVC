using Gaming.Application.Common.Exceptions;
using Gaming.Application.Common.Interfaces;
using Gaming.Domain.Entities;
using Gaming.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gaming.MVC.Areas.Identity.Pages.Account.Manage
{
    public class SetPictureModel : PageModel
    {
       

        private readonly UserManager<User> _userManager;
        private readonly ICurrentUser _currentUser;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SetPictureModel(UserManager<User> userManager, ICurrentUser currentUser, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _currentUser = currentUser;
            _webHostEnvironment = webHostEnvironment;
        }

        public InputModel Input { get; set; }

        public class InputModel
        {

            public IFormFile ProfileImage { get; set; }

        }

        public async Task<IActionResult> OnPostAsync([FromForm] InputModel input)
        {
            if (ModelState.IsValid) 
            {
                var user = await _userManager.FindByIdAsync(_currentUser.UserId);
                if (user is null)
                    throw new NotFoundException("User", _currentUser.UserId);
               
                string rootpath = _webHostEnvironment.WebRootPath;
                string extension = Path.GetExtension(input.ProfileImage.FileName);
                string filename = Guid.NewGuid() + extension;

                string combinedPath = Path.Combine(rootpath + @"\userImages", filename);

                using (var stream = new FileStream(combinedPath, FileMode.Create))
                {
                    await input.ProfileImage.CopyToAsync(stream);

                }
                user.ProfilePicture ="/userImages/"+ filename;
                
                await _userManager.UpdateAsync(user);
                
                return RedirectToPage("./Index");

            }

            return Page();


        }
    }
}
