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
        [HttpPost]
        public async Task<IActionResult> PostAsync(CancellationToken cancellationToken)
        {
            //create a new job sample job
            
            return Ok("Job is scheduled");
        }
    }
}