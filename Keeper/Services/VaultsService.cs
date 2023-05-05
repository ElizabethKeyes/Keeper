namespace Keeper.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;

  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    int vaultId = _repo.CreateVault(vaultData);
    Vault vault = GetVaultById(vaultId);
    return vault;
  }

  internal string DeleteVault(string userId, int vaultId)
  {
    Vault vault = GetVaultById(vaultId);
    if (userId == vault.CreatorId)
    {
      _repo.DeleteVault(vaultId);
      return $"The vault at ID {vaultId} has been deleted.";
    }
    else
    {
      throw new Exception("You cannot delete another user's Vault.");
    }
  }

  internal Vault EditVault(string userId, int vaultId, Vault vaultData)
  {
    Vault ogVault = GetVaultById(vaultId);
    if (ogVault.CreatorId == userId)
    {
      ogVault.Name = vaultData.Name ?? ogVault.Name;
      ogVault.Description = vaultData.Description ?? ogVault.Description;
      ogVault.Img = vaultData.Img ?? ogVault.Img;

      _repo.EditVault(ogVault);
      return ogVault;
    }
    else
    {
      throw new Exception("You are not authorized to edit another user's Vault.");
    }
  }

  internal Vault GetVaultById(int vaultId)
  {
    Vault vault = _repo.GetVaultById(vaultId);
    if (vault == null)
    {
      throw new Exception($"No vault found at ID {vaultId}");
    }
    return vault;
  }
}
