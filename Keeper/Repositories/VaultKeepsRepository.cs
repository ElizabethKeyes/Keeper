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

  internal List<VaultKeep> GetVaultKeepsByVaultId(int vaultId)
  {
    string sql = @"
    SELECT
    vaultKeeps.*,
    keeps.*,
    accounts.*
    FROM vaultKeeps
    JOIN keeps ON keeps.id = vaultKeeps.keepId
    JOIN accounts ON accounts.id = vaultKeeps.creatorId
    WHERE vaultKeeps.vaultId = vaultId;
    ";

    List<VaultKeep> vaultKeeps = _db.Query<VaultKeep, Keep, Profile, VaultKeep>(sql, (vaultKeep, keep, account) =>
    {
      vaultKeep.Keep = keep;
      vaultKeep.Creator = account;
      return vaultKeep;
    }, new { vaultId }).ToList();

    return vaultKeeps;
  }
}
