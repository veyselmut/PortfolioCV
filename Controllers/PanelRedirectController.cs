using Microsoft.AspNetCore.Mvc;

namespace PortfolioCV.Controllers
{
    public class PanelRedirectController : Controller
    {
        // Legacy routes from the old AdminController
        [Route("admin")]
        [Route("dashboard")]
        [Route("welcome-message")]
        [Route("profile")]
        [Route("personal-info")]
        [Route("educations")]
        [Route("experiences")]
        [Route("skills")]
        [Route("projects")]
        [Route("languages")]
        [Route("certificates")]
        [Route("references")]
        [Route("social-media")]
        [Route("messages")]
        [Route("manage-services")]
        public IActionResult RedirectToNewPanel()
        {
            // Redirect to the React Admin Panel
            // In development, this is typically http://localhost:5173
            // You can adjust this URL based on your hosting environment
            return Redirect("http://localhost:5173/");
        }
    }
}
