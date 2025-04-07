using BusinessInterfaceLayer;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace WepAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : Controller
    {
        private readonly ITokenRepository iTokenRepository;

        public TokenController(ITokenRepository _iTokenRepository)
        {
            iTokenRepository = _iTokenRepository;
        }

        [HttpGet(Name = "RetrieveTokens")]
        public List<TokenModel> RetrieveTokens()
        {
            return iTokenRepository.RetrieveTokens(); 
        }

        [HttpGet("RetrieveTokenTable")]
        public async Task<List<TokenDataGridModel>> RetrieveTokenTable()
        {
            try
            {
                return await iTokenRepository.RetrieveTokenTable();
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("AddOrUpdateToken")]
        public async Task<IActionResult> AddOrUpdateToken(TokenModel token)
        {
            if (token == null)
            {
                return BadRequest();
            }
            try
            {
                var response = await iTokenRepository.AddOrUpdateTokenAsync(token);
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
