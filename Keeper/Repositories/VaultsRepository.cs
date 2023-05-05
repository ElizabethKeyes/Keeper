namespace Keeper.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateVault(Vault vaultData)
  {
    string sql = @"
    INSERT INTO vaults
    (name, description, img, isPrivate, creatorId)
    VALUES
    (@Name, @Description, @Img, @IsPrivate, @CreatorId);

    SELECT LAST_INSERT_ID();
    ";

    int id = _db.ExecuteScalar<int>(sql, vaultData);
    return id;
  }

  internal Vault GetVaultById(int vaultId)
  {
    string sql = @"
    SELECT
    vaults.*,
    accounts.*
    FROM vaults
    JOIN accounts ON vaults.creatorId = accounts.id
    WHERE vaults.id = @vaultId;
    ";

    Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { vaultId }).FirstOrDefault();

    return vault;
  }
}
