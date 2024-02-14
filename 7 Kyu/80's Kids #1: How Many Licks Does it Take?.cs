using System;
using System.Collections.Generic;
using System.Linq;

public class Kata
{
    public string TotalLicks(Dictionary<string, int> env)
    {
        int licks = 252 + env.Values.Sum(x => x); //default licks = 252
        string licksSentence = $"It took {licks} licks to get to the tootsie roll center of a tootsie pop.";
        if (env.Count == 0)
          return licksSentence; 
        List<string> toughestChallange = env.Where(x => x.Value > 0 && x.Value == env.Values.Max(x => x)).Select(x => x.Key).ToList();
        string toughestChallangeSentence = toughestChallange.Count != 0 ? $" The toughest challenge was {toughestChallange[0]}." : "";
        return licksSentence + toughestChallangeSentence;
    }
}
