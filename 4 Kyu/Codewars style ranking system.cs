using System;
using System.Collections.Generic;
using System.Linq;

public class User 
{
  public int rank { get; set; }
  public int progress{ get; set; }
  public List<int> validRanks = new List<int> { -8, -7, -6, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 6, 7, 8 };
  public void incProgress(int actRank) 
  {
    if (!validRanks.Contains(actRank)) throw new ArgumentException();
    int value = 0;
    int rankIndex = validRanks.FindIndex(x => x == rank);
    int actRankIndex = validRanks.FindIndex(x => x == actRank);
    if (rank == actRank) value = 3;
    else if (rankIndex - 1  == actRankIndex) value = 1;
    else if (rank - actRank < 0) value = 10 * (rankIndex - actRankIndex) * (rankIndex - actRankIndex);
    changeProgress(value);
  }
  public void changeProgress(int value) {
    if (progress + value < 100) progress += value;
    else 
    {
      while (progress + value >= 100)
      {
        if (rank < 8) rank++;
        if (rank == 0) rank++;
        value -= 100;
      }
      progress += value;
    }
    if (rank == 8) progress = 0;
  }
  public User() 
  {
    rank = -8;
    progress = 0;
  }
}
