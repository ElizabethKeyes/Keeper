namespace Keeper.Models;

public class VaultKeep
{
  public int Id { get; set; }
  public int VaultId { get; set; }
  public int KeepId { get; set; }
  public string CreatorId { get; set; }
  public Keep Keep { get; set; }
  public Profile Creator { get; set; }
}

public class KeepInVault : Keep
{
  public int VaultKeepId { get; set; }
}