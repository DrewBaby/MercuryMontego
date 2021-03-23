using System;

namespace IGDB.Models
{
  public interface ITimestamps
  {
    DateTimeOffset? Created_At { get; set; }
    DateTimeOffset? Updated_At { get; set; }
  }
}