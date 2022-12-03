using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.ApiModels;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : Controller
    {
        private readonly DonationDBContext dbContext;
        public CandidateController(DonationDBContext dbContext)
        {
           this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCandidates()
        {
            return Ok(await dbContext.DCandidates.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCandidateById([FromRoute] Guid id)
        {
            var dbcandidate = dbContext.DCandidates.Find(id);
            if (dbcandidate != null)
            {
                return Ok(dbcandidate);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidate(CandidateApiModel candidate)
        {
            var newcandidate = new DCandidate()
            {
                id = Guid.NewGuid(),
                address = candidate.address,
                mobile = candidate.mobile,
                email = candidate.email,
                fullName = candidate.fullName,
                age = candidate.age,
                bloogGroop = candidate.bloogGroop,
            }; //mapping (ApiModel -> db model)

            await dbContext.DCandidates.AddAsync(newcandidate);
            await dbContext.SaveChangesAsync();

            return Ok(newcandidate);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCandidate([FromRoute] Guid id, CandidateApiModel candidate)
        {
            var dbcandidate = dbContext.DCandidates.Find(id);
            if (candidate != null)
            {
                dbcandidate.email = candidate.email;
                dbcandidate.address = candidate.address;
                dbcandidate.mobile = candidate.mobile;
                dbcandidate.age = candidate.age;
                dbcandidate.bloogGroop = candidate.bloogGroop;
                dbcandidate.fullName = candidate.fullName;

                await dbContext.SaveChangesAsync();
                return Ok(dbcandidate);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCandidate([FromRoute] Guid id)
        {
            var dbcandidate = dbContext.DCandidates.Find(id);
            if (dbcandidate != null)
            {
                dbContext.Remove(dbcandidate);
                await dbContext.SaveChangesAsync();

                return Ok(dbcandidate);
            }

            return NotFound();
        }
    }
}
