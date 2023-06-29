using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamBudget.Models;
using StreamBudget.Services.Abstract;

namespace StreamBudget.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchInfoController : ControllerBase
    {
        private readonly IStreamAvailService _streamAvailService;
        private readonly UserManager<IdentityUser> _userManager;
        ////NEED TO IMPLEMENT "person" repository FIRST.
        //private readonly IPersonRepository<Person> _personRepo;

        ////NEED TO ADD "person" repository HERE.
        public SearchInfoController(IStreamAvailService streamAvailService, UserManager<IdentityUser> userManager)
        {
            _streamAvailService = streamAvailService;
            _userManager = userManager;
        }

        //string aspId = _userManager.GetUserId(User);
    }
}
