using System;
using Newtonsoft.Json;

namespace IGDB.Models
{
  public class PlatformVersionReleaseDate : ITimestamps, IIdentifier, IHasChecksum
  {
    public ReleaseDateCategory? Category { get; set; }
    public string Checksum { get; set; }
    public DateTimeOffset? Created_At { get; set; }
    public DateTimeOffset? Date { get; set; }
    public string Human { get; set; }
    public long? Id { get; set; }
    [JsonProperty("m")]
    public int? Month { get; set; }
    public IdentityOrValue<PlatformVersion> PlatformVersion { get; set; }
    public ReleaseDateRegion? Region { get; set; }
    public DateTimeOffset? Updated_At { get; set; }
    [JsonProperty("y")]
    public int? Year { get; set; }
  }
}