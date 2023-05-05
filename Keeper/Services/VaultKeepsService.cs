namespace Keeper.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;

  public VaultKeepsService(VaultKeepsRepository repo)
  {
    _repo = repo;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId, Vault vault)
  {
    if (vault.CreatorId == userId)
    {
      vaultKeepData.CreatorId = userId;
      int vaultKeepId = _repo.CreateVaultKeep(vaultKeepData);
      VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
      return vaultKeep;
    }
    else
    {
      throw new Exception("You are not permitted to add to another user's Vault.");
    }
  }

  internal string DeleteVaultKeep(string userId, int vaultKeepId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
    if (vaultKeep.CreatorId == userId)
    {
      _repo.DeleteVaultKeep(vaultKeepId);
      return $"The VaultKeep at ID {vaultKeepId} has been successfully deleted.";
    }
    else
    {
      throw new Exception("You cannot delete another user's VaultKeep.");
    }
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _repo.GetVaultKeepById(vaultKeepId);
    if (vaultKeep == null)
    {
      throw new Exception($"Invalid VaultKeep ID: {vaultKeepId}");
    }
    return vaultKeep;
  }

  internal List<KeepInVault> GetVaultKeepsByVaultId(int vaultId)
  {
    List<KeepInVault> keepsInVault = _repo.GetVaultKeepsByVaultId(vaultId);
    return keepsInVault;
  }
}
