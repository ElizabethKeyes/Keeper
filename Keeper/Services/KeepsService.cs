namespace Keeper.Services;

public class KeepsService
{
  private readonly KeepsRepository _repo;

  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    int keepId = _repo.CreateKeep(keepData);
    Keep keep = GetKeepById(keepId);
    return keep;
  }

  internal string DeleteKeep(string userId, int keepId)
  {
    Keep keep = GetKeepById(keepId);
    if (keep.CreatorId == userId)
    {
      _repo.DeleteKeep(keepId);
      return $"The keep with an id of {keepId} has been successfully deleted.";
    }
    else
    {
      throw new Exception("You cannot delete another user's Keep.");
    }
  }

  internal Keep EditKeep(string userId, int keepId, Keep keepData)
  {
    Keep ogKeep = GetKeepById(keepId);
    if (ogKeep.CreatorId == userId)
    {
      ogKeep.Name = keepData.Name ?? ogKeep.Name;
      ogKeep.Description = keepData.Description ?? ogKeep.Description;
      ogKeep.Img = keepData.Img ?? ogKeep.Img;
      _repo.EditKeep(ogKeep);
      return ogKeep;
    }
    else
    {
      throw new Exception("You are not authorized to edit another user's keep.");
    }
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repo.GetAllKeeps();
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _repo.GetKeepById(keepId);
    if (keep == null)
    {
      throw new Exception($"Invalid Keep ID: {keepId}");
    }
    keep.Views++;
    return keep;
  }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  {
    List<Keep> keeps = _repo.GetKeepsByProfileId(profileId);
    return keeps;
  }
}
