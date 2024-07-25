using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;
using Talabat_.APIS.Errors;

namespace Talabat_.APIS.Controllers
{
    public class BuggyController : BaseAPIController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")] //Get : baseurl/api/Buggy/notfound
        public ActionResult GetNOtFoundRequest()
        {
            var product = _context.products.Find(1000);
            if (product is null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(product);
        }

        [HttpGet("servererror")] //Get : baseurl/api/Buggy/servererror
        public ActionResult GetServerError()
        {
            var product = _context.products.Find(1000);
            var result = product.ToString();

            return Ok(result);
        }

        [HttpGet("badrequest")] //Get : baseurl/api/Buggy/badrequest
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")] //Get : baseurl/api/Buggy/badrequest
        public ActionResult GetBadRequest(int? id) // validation error
        {
            return Ok();
        }

        [HttpGet("unauthorized")] //Get : baseurl/api/Buggy/unauthorized
        public ActionResult GetUnauthorizedError()
        {
            return Unauthorized(new ApiResponse(401));
        }
    }
}
