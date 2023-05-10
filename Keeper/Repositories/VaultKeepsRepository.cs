namespace Keeper.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"
    INSERT INTO vaultKeeps
    (creatorId, vaultId, keepId)
    VALUES
    (@CreatorId, @VaultId, @KeepId);

    SELECT LAST_INSERT_ID();
    ";

    int vaultKeepId = _db.ExecuteScalar<int>(sql, vaultKeepData);
    return vaultKeepId;
  }

  internal void DeleteVaultKeep(int vaultKeepId)
  {
    string sql = @"
    DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;
    ";

    _db.Execute(sql, new { vaultKeepId });
  }

  internal List<VaultKeep> GetMyVaultKeeps(string userId)
  {
    string sql = @"
    SELECT
    *
    FROM vaultKeeps
    WHERE vaultKeeps.creatorId = @userId;
    ";

    List<VaultKeep> vaultKeeps = _db.Query<VaultKeep>(sql, new { userId }).ToList();
    return vaultKeeps;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = @"
    SELECT
    *
    FROM vaultKeeps
    WHERE vaultKeeps.id = @vaultKeepId;
    ";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
    return vaultKeep;
  }

  internal List<KeepInVault> GetVaultKeepsByVaultId(int vaultId)
  {
    string sql = @"
    SELECT 
    keeps.*,
    accounts.*,
    vaultKeeps.id
    FROM vaultKeeps
    JOIN keeps ON vaultKeeps.keepId = keeps.id
    JOIN accounts ON keeps.creatorId = accounts.id
    WHERE vaultKeeps.vaultId = @vaultId;
    ";

    List<KeepInVault> keepsInVault = _db.Query<KeepInVault, Profile, VaultKeep, KeepInVault>(sql, (keep, account, vaultKeep) =>
    {
      keep.Creator = account;
      keep.VaultKeepId = vaultKeep.Id;
      return keep;
    }, new { vaultId }).ToList();

    return keepsInVault;
  }
}
