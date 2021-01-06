using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr_server.Models;
using keepr_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr_server.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly VaultsService _vs;
    private readonly KeepsService _ks;

    public ProfilesController(ProfilesService ps, VaultsService vs, KeepsService ks)
    {
      _ps = ps;
      _vs = vs;
      _ks = ks;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Profile>> Get()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_ps.GetOrCreateProfile(userInfo));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<Profile>> GetVaultsByProfile(string id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_vs.GetVaultsByProfile(id, userInfo?.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<Profile>> GetKeepsByProfile(string id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_ks.GetKeepsByProfile(id, userInfo?.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}