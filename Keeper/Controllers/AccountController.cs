namespace Keeper.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultsService _vaultsService;
  private readonly VaultKeepsService _vaultKeepsService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService, VaultKeepsService vaultKeepsService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vaultsService = vaultsService;
    _vaultKeepsService = vaultKeepsService;
  }

  [HttpPut]
  [Authorize]
  public async Task<ActionResult<Account>> Edit([FromBody] Account editData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userEmail = userInfo.Email;
      Account editedAccount = _accountService.Edit(editData, userEmail);
      return Ok(editedAccount);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaultKeeps")]
  [Authorize]
  public async Task<ActionResult<List<VaultKeep>>> GetMyVaultKeeps()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      List<VaultKeep> vaultKeeps = _vaultKeepsService.GetMyVaultKeeps(userId);
      return Ok(vaultKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  [Authorize]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      List<Vault> vaults = _vaultsService.GetMyVaults(userId);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
