using Microsoft.AspNetCore.Mvc;

namespace QuartzSceduleSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSceduleController : ControllerBase
    {
        private readonly IQuartzJobScheduler _quartzJobScheduler;

        public TestSceduleController(IQuartzJobScheduler quartzJobScheduler)
        {
            _quartzJobScheduler = quartzJobScheduler;
        }

        //Place for Scheduled Jobs - not need right now
    }
}
