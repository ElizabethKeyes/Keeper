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

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _repo.GetKeepById(keepId);
    return keep;
  }
}
