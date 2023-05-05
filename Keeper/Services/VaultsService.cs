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

  internal Vault GetVaultById(int vaultId)
  {
    Vault vault = _repo.GetVaultById(vaultId);
    return vault;
  }
}
