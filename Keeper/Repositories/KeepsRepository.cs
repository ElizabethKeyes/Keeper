namespace Keeper.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"
    INSERT INTO keeps
    (creatorId, name, description, img)
    VALUES
    (@CreatorId, @Name, @Description, @Img);

    SELECT
    keeps.*,
    COUNT(vaultKeeps.id) AS kept,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
    WHERE keeps.id = LAST_INSERT_ID()
    GROUP BY (keeps.id);
    ";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }, keepData).FirstOrDefault();
    return keep;
  }

  internal void DeleteKeep(int keepId)
  {
    string sql = @"
    DELETE FROM
    keeps
    WHERE id = @keepId;
    ";

    _db.Execute(sql, new { keepId });
  }

  internal void EditKeep(Keep ogKeep)
  {
    string sql = @"
    UPDATE keeps
    SET
    Description = @Description,
    Img = @Img,
    Name = @Name,
    Views = @Views
    WHERE id = @Id;
    ";

    _db.Execute(sql, ogKeep);
  }

  internal List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT
    keeps.*,
    COUNT(vaultKeeps.id) AS kept,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
    GROUP BY (keeps.id);
    ";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();

    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
    SELECT
    keeps.*,
    COUNT(vaultKeeps.id) AS kept,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
    WHERE keeps.id = @keepId
    GROUP BY (keeps.id);
    ";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { keepId }).FirstOrDefault();
    return keep;
  }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  {
    string sql = @"
    SELECT
    keeps.*,
    COUNT(vaultKeeps.id) AS kept
    FROM keeps
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
    WHERE keeps.creatorId = @profileId
    GROUP BY(keeps.id);
    ";

    List<Keep> keeps = _db.Query<Keep>(sql, new { profileId }).ToList();
    return keeps;
  }
}
