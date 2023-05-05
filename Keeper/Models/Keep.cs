namespace Keeper.Models;

public class Keep
{
  public int Id { get; set; }
  public string CreatorId { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public int Views { get; set; } = 0;
  public Profile Creator { get; set; }
  public int Kept { get; set; } = 0;

}

// Views will be incremented by the service when a request is made to get that keep

// Kept will be a count that is accomplished through the SQL statement in the repository layer
