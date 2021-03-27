using System;

namespace IGDB.Models
{
  public class Keyword : ITimestamps, IIdentifier, IHasChecksum
  {
    public string Checksum { get; set; }
    public DateTimeOffset? Created_At { get; set; }
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public DateTimeOffset? Updated_At { get; set; }
    public string Url { get; set; }
  }
}