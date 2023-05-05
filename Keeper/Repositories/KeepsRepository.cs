namespace Keeper.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateKeep(Keep keepData)
  {
    string sql = @"
    INSERT INTO keeps
    (creatorId, name, description, img)
    VALUES
    (@CreatorId, @Name, @Description, @Img);

    SELECT LAST_INSERT_ID();
    ";

    int keepId = _db.ExecuteScalar<int>(sql, keepData);
    return keepId;
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
    Name = @Name
    WHERE id = @Id;
    ";

    _db.Execute(sql, ogKeep);
  }

  internal List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId;
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
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    WHERE keeps.id = @keepId;";

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
    *
    FROM keeps
    WHERE keeps.creatorId = @profileId;
    ";

    List<Keep> keeps = _db.Query<Keep>(sql, new { profileId }).ToList();
    return keeps;
  }
}
