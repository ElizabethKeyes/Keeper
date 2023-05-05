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

  internal void DeleteVault(int vaultId)
  {
    string sql = @"
    DELETE FROM
    vaults
    WHERE id = @vaultId;
    ";

    _db.Execute(sql, new { vaultId });
  }

  internal void EditVault(Vault ogVault)
  {
    string sql = @"
    UPDATE vaults
    SET
    Name = @Name,
    Description = @Description,
    Img = @Img
    WHERE id = @Id;
    ";

    _db.Execute(sql, ogVault);
  }

  internal List<Vault> GetMyVaults(string userId)
  {
    string sql = @"
    SELECT
    *
    FROM vaults
    WHERE vaults.creatorId = @userId;
    ";

    List<Vault> vaults = _db.Query<Vault>(sql, new { userId }).ToList();
    return vaults;
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

  internal List<Vault> GetVaultsByProfileId(string profileId)
  {
    string sql = @"
    SELECT
    *
    FROM vaults
    WHERE vaults.creatorId = @profileId;
    ";

    List<Vault> vaults = _db.Query<Vault>(sql, new { profileId }).ToList();
    return vaults;
  }
}
