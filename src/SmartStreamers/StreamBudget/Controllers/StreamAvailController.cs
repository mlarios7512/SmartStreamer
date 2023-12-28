using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamBudget.Models.DTO.StreamAvail;
using StreamBudget.Services.Abstract;

namespace StreamBudget.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamAvailController : ControllerBase
    {
        private readonly IStreamAvailService _streamAvailService;

        public StreamAvailController(IStreamAvailService streamAvailService)
        {
            _streamAvailService = streamAvailService;
        }
    }
}
