using System;

namespace IGDB.Models
{
  public class GameVersion : ITimestamps, IIdentifier, IHasChecksum
  {
    
    public string Checksum { get; set; }
    public DateTimeOffset? Created_At { get; set; }
    public IdentitiesOrValues<GameVersionFeature> Features { get; set; }
    public IdentityOrValue<Game> Game { get; set; }
    public IdentitiesOrValues<Game> Games { get; set; }
    public long? Id { get; set; }
    public DateTimeOffset? Updated_At { get; set; }
    public string Url { get; set; }
  }
}