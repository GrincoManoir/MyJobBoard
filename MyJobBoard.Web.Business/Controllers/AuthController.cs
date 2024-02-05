using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using System.Net;

namespace MyJobBoard.Web.Business.Controllers
{
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthController(SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        /// <summary>
        /// Retourne une 200ok si la session de l'utilisateur est toujours valide une 401 sinon
        /// </summary>
        /// <returns></returns>
        [HttpGet("checkSession")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CheckSession()
        {
            return Ok();
        }

        [HttpGet("logout")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> Logout()
        {
            
            await _signInManager.SignOutAsync();
            var accessToken = _signInManager.Context.Request.Headers.Authorization;
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                _tokenService.AddToBlacklist(accessToken!);
            }
            return Ok();

        }
    }
}
