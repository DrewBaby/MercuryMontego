using System;

namespace IGDB.Models
{
  public class Franchise : ITimestamps, IIdentifier, IHasChecksum
  {
    public DateTimeOffset? Created_At { get; set; }
    public string Checksum { get; set; }
    public DateTimeOffset? Updated_At { get; set; }
    public long? Id { get; set; }
    public IdentitiesOrValues<Game> Games { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Url { get; set; }
  }
}