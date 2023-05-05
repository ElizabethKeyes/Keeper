namespace Keeper.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;

  public VaultKeepsService(VaultKeepsRepository repo)
  {
    _repo = repo;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
  {
    vaultKeepData.CreatorId = userId;
    int vaultKeepId = _repo.CreateVaultKeep(vaultKeepData);
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
    return vaultKeep;
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

  internal List<VaultKeep> GetVaultKeepsByVaultId(int vaultId)
  {
    List<VaultKeep> vaultKeeps = _repo.GetVaultKeepsByVaultId(vaultId);
    return vaultKeeps;
  }
}
