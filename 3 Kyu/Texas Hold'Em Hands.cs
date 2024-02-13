using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public enum Result
{
    Win,
    Loss,
    Tie
}

public class Kata
{
    public List<string> outcomes = new List<string> { "nothing", "pair", "two pair", "three-of-a-kind", "straight", "flush", "full house", "four-of-a-kind", "straight-flush" };
    public static List<char> values = new List<char> { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
    public static List<string> indexes = new List<string> { "1,2,3,4,5", "1,2,3,4,6", "1,2,3,4,7", "1,2,3,5,6", "1,2,3,5,7", "1,2,3,6,7",
                                                            "1,2,4,5,6", "1,2,4,5,7", "1,2,4,6,7",
                                                            "1,2,5,6,7",
                                                            "1,3,4,5,6", "1,3,4,5,7", "1,3,4,6,7", "1,3,5,6,7",
                                                            "1,4,5,6,7",
                                                            "2,3,4,5,6", "2,3,4,5,7", "2,3,4,6,7", 
                                                            "2,3,5,6,7", "2,4,5,6,7",
                                                            "3,4,5,6,7" };
    public List<string> NoComboHand { get; private set; }
    public string PlayerHand { get; private set; }
    public string kicker { get; private set; }
    public string kicker2 { get; private set; }
    public Kata(string hand)
    {
        List<string> separateCards = hand.Split(' ').ToList();
        if (hand.Contains("10"))
        {
            List<string> tens = separateCards.FindAll(x => x.Contains("10"));
            separateCards = separateCards.Where(x => !x.Contains("10")).ToList();
            foreach (string ten in tens) separateCards.Add($"T{ten[2]}");
        }
        Regex regex = new Regex(@"\d{1}\w");
        separateCards = separateCards.Select(x => x[1] == '♠' ? $"{x[0]}S" :
                                                  x[1] == '♦' ? $"{x[0]}D" :
                                                  x[1] == '♣' ? $"{x[0]}C" :
                                                  x[1] == '♥' ? $"{x[0]}H" :
                                                  x).ToList();
        List<string> numbers = separateCards.Where(x => regex.IsMatch(x)).ToList();
        List<string> letters = separateCards.Where(x => !regex.IsMatch(x)).Select(x => x[0] == 'T' ? $"0{x[1]}" :
                                                                                       x[0] == 'J' ? $"1{x[1]}" :
                                                                                       x[0] == 'Q' ? $"2{x[1]}" :
                                                                                       x[0] == 'K' ? $"3{x[1]}" :
                                                                                       x[0] == 'A' ? $"4{x[1]}" :
                                                                                       x).ToList();
        numbers.Sort();
        letters.Sort();
        letters = letters.Select(x => x[0] == '0' ? $"T{x[1]}" :
                                      x[0] == '1' ? $"J{x[1]}" :
                                      x[0] == '2' ? $"Q{x[1]}" :
                                      x[0] == '3' ? $"K{x[1]}" :
                                      x[0] == '4' ? $"A{x[1]}" :
                                      x).ToList();
        separateCards.Clear();
        foreach (string number in numbers) separateCards.Add(number);
        foreach (string letter in letters) separateCards.Add(letter);
        PlayerHand = String.Concat<string>(separateCards.Select(x => x + ' ')).TrimEnd(' ');
        kicker2 = "";
    }
    public static (string type, string[] ranks) Hand(string[] holeCards, string[] communityCards)
    {
        string str = "";
        List<string> allCards = holeCards.ToList();
        foreach (string s in communityCards) allCards.Add(s);
        List<int> index = indexes[0].Split(',').Select(x => Convert.ToInt32(x)).ToList();
        foreach (int i in index) str += allCards[i - 1] + " ";
        string tempHand = str.TrimEnd(' ');
        Kata strongestHand = new Kata(tempHand);
        str = "";
        for (int j = 0; j < indexes.Count; j++)
        {
            str = ""; index.Clear(); tempHand = "";
            index = indexes[j].Split(',').Select(x => Convert.ToInt32(x)).ToList();
            foreach (int i in index) str += allCards[i - 1] + " ";
            tempHand = str.TrimEnd(' ');
            Kata handToCheck = new Kata(tempHand);
            if (strongestHand.CompareWith(handToCheck) == 0) continue;
            else strongestHand = handToCheck;
        }
        List<string> output = new List<string>();
        List<string> cardsToParse = strongestHand.PlayerHand.Split(' ', StringSplitOptions.RemoveEmptyEntries).Reverse().Select(x => Convert.ToString(x[0])).ToList();
        string outcome = FindOutcome(strongestHand);
        if (outcome == "three-of-a-kind" || outcome == "pair" || outcome == "four-of-a-kind") output.Add(Convert.ToString(strongestHand.kicker[0]));
        if (outcome == "two pair") { output.Add(Convert.ToString(strongestHand.kicker[0])); output.Add(Convert.ToString(strongestHand.kicker2[0])); }
        if (outcome == "full house") { output.Add(Convert.ToString(strongestHand.kicker[0])); output.Add(Convert.ToString(strongestHand.NoComboHand[0][0])); }
        foreach (string card in cardsToParse) if (!output.Contains(card)) output.Add(card);
        output = output.Select(x => x[0] == 'T' ? $"10" : x).ToList();
        return (outcome, output.ToArray());
    }
    public Result CompareWith(Kata hand)
    {
        if (outcomes.FindIndex(x => x == FindOutcome(this)) > outcomes.FindIndex(x => x == FindOutcome(hand))) return (Result)0;
        else if (outcomes.FindIndex(x => x == FindOutcome(this)) < outcomes.FindIndex(x => x == FindOutcome(hand))) return (Result)1;
        else
        {
            if (values.FindIndex(x => x == kicker[0]) > values.FindIndex(x => x == hand.kicker[0])) return (Result)0;
            else if (values.FindIndex(x => x == kicker[0]) < values.FindIndex(x => x == hand.kicker[0])) return (Result)1;
            else
            {
                if (FindOutcome(this) == "flush" || FindOutcome(this) == "straight" || FindOutcome(this) == "straight-flush")
                {
                    NoComboHand = PlayerHand.Split(' ').ToList();
                    hand.NoComboHand = hand.PlayerHand.Split(' ').ToList();
                }
                if (FindOutcome(this) == "two pair")
                {
                    if (values.FindIndex(x => x == kicker2[0]) > values.FindIndex(x => x == hand.kicker2[0])) return (Result)0;
                    else if (values.FindIndex(x => x == kicker2[0]) < values.FindIndex(x => x == hand.kicker2[0])) return (Result)1;
                    else if (values.FindIndex(x => x == NoComboHand[0][0]) > values.FindIndex(x => x == hand.NoComboHand[0][0])) return (Result)0;
                    else if (values.FindIndex(x => x == NoComboHand[0][0]) < values.FindIndex(x => x == hand.NoComboHand[0][0])) return (Result)1;
                    else return (Result)2;
                }
                for (int i = this.NoComboHand.Count - 1; i >= 0; i--)
                {
                    if (values.FindIndex(x => x == NoComboHand[i][0]) > values.FindIndex(x => x == hand.NoComboHand[i][0])) return (Result)0;
                    else if (values.FindIndex(x => x == NoComboHand[i][0]) < values.FindIndex(x => x == hand.NoComboHand[i][0])) return (Result)1;
                    else continue;
                }
                return (Result)2;
            }
        }
    }
    public static string FindOutcome(Kata hand)
    {
        List<string> separateCards = hand.PlayerHand.Split(' ').ToList();
        List<int> suitOccurrences = new List<int>();
        string kicker = "";
        string kicker2 = "";
        List<string> noComboHand = new List<string>();
        foreach (string card in separateCards) suitOccurrences.Add(separateCards.FindAll(x => x[1] == card[1]).Count());
        if (IsStraightFlush(separateCards, ref kicker)) { hand.kicker = kicker; return "straight-flush"; }
        if (IsFourOfAKind(separateCards, ref kicker, ref noComboHand)) { hand.kicker = kicker; hand.NoComboHand = noComboHand; return "four-of-a-kind"; }
        if (IsFullHouse(separateCards, ref kicker, ref noComboHand)) { hand.kicker = kicker; hand.NoComboHand = noComboHand; return "full house"; }
        if (IsFlush(separateCards, suitOccurrences, ref kicker)) { hand.kicker = kicker; return "flush"; }
        if (IsStraight(separateCards, ref kicker)) { hand.kicker = kicker; return "straight"; }
        if (IsThreeOfAKind(separateCards, ref kicker, ref noComboHand)) { hand.kicker = kicker; hand.NoComboHand = noComboHand; return "three-of-a-kind"; }
        if (IsTwoPairs(separateCards, ref kicker, ref kicker2, ref noComboHand)) { hand.kicker = kicker; hand.NoComboHand = noComboHand; hand.kicker2 = kicker2; return "two pair"; }
        if (IsPair(separateCards, ref kicker, ref noComboHand)) { hand.kicker = kicker; hand.NoComboHand = noComboHand; return "pair"; }
        hand.kicker = kicker; hand.NoComboHand = noComboHand;
        return "nothing";
    }

    static bool IsStraightFlush(List<string> separateCards, ref string kicker)
    {
        char expectedValue = separateCards[0][0];
        int valueIndex = values.FindIndex(x => x == expectedValue);
        char firstSuit = separateCards[0][1];
        foreach (string card in separateCards)
        {
            if (card[0] == expectedValue && card[1] == firstSuit)
            {
                if (valueIndex >= values.Count) return false;
                valueIndex++;
                if (valueIndex < values.Count) expectedValue = values[valueIndex];
                continue;
            }
            else return false;
        }
        kicker = separateCards[0];
        return true;
    }
    static bool IsFourOfAKind(List<string> separateCards, ref string kicker, ref List<string> noComboHand)
    {
        List<string> combo = new List<string>();
        foreach (string card in separateCards)
        {
            if (separateCards.FindAll(x => x[0] == card[0]).ToList().Count() == 4) combo.Add(card);
            else noComboHand.Add(card);
        }
        if (combo.Count == 4)
        {
            kicker = separateCards[2];
            return true;
        }
        else
        {
            noComboHand.Clear();
            return false;
        }
    }
    static bool IsFullHouse(List<string> separateCards, ref string kicker, ref List<string> noComboHand)
    {
        List<string> list1 = separateCards.FindAll(x => x[0] == separateCards[0][0]).ToList();
        List<string> list2 = separateCards.FindAll(x => x[0] == separateCards[4][0]).ToList();
        if ((list1.Count == 3 && list2.Count == 2) || (list1.Count == 2 && list2.Count == 3))
        {
            if (list1.Count == 3)
            {
                kicker = list1[0];
                foreach (string card in list2) noComboHand.Add(card);
            }
            if (list2.Count == 3)
            {
                kicker = list2[0];
                foreach (string card in list1) noComboHand.Add(card);
            }
            return true;
        }
        else
        {
            noComboHand.Clear();
            return false;
        }
    }
    static bool IsFlush(List<string> separateCards, List<int> suitOccurrences, ref string kicker)
    {

        if (suitOccurrences[0] == 5)
        {
            kicker = separateCards[4];
            return true;
        }
        else return false;
    }
    static bool IsStraight(List<string> separateCards, ref string kicker)
    {
        //if (separateCards[0][0] == '2' && separateCards[1][0] == '3' && separateCards[2][0] == '4' && separateCards[3][0] == '5' && separateCards[4][0] == 'A') return true;
        char expectedValue = separateCards[0][0];
        int valueIndex = values.FindIndex(x => x == expectedValue);
        foreach (string card in separateCards)
        {
            if (card[0] == expectedValue)
            {
                if (valueIndex >= values.Count) return false;
                valueIndex++;
                if (valueIndex < values.Count) expectedValue = values[valueIndex];
                continue;
            }
            else return false;
        }
        kicker = separateCards[1];
        return true;
    }
    static bool IsThreeOfAKind(List<string> separateCards, ref string kicker, ref List<string> noComboHand)
    {
        List<string> combo = new List<string>();
        foreach (string card in separateCards)
        {
            if (separateCards.FindAll(x => x[0] == card[0]).ToList().Count() == 3)
            {
                kicker = card;
                combo.Add(card);
            }
            else noComboHand.Add(card);
        }
        if (combo.Count == 3) return true;
        noComboHand.Clear();
        return false;
    }
    static bool IsTwoPairs(List<string> separateCards, ref string kicker, ref string kicker2, ref List<string> noComboHand)
    {
        List<string> combo = new List<string>();
        foreach (string card in separateCards)
        {
            if (separateCards.FindAll(x => x[0] == card[0]).ToList().Count() == 2) combo.Add(card);
            else noComboHand.Add(card);
        }

        List<int> valuesOccurrences = new List<int>();
        foreach (string card in separateCards) valuesOccurrences.Add(separateCards.FindAll(x => x[0] == card[0]).Count());
        valuesOccurrences.Sort();
        if (valuesOccurrences[0] == 1 && valuesOccurrences[1] == 2 && valuesOccurrences[4] == 2)
        {
            kicker = separateCards[3];
            kicker2 = separateCards[1];
            return true;
        }
        else
        {
            noComboHand.Clear();
            return false;
        }
    }
    static bool IsPair(List<string> separateCards, ref string kicker, ref List<string> noComboHand)
    {
        List<int> valuesOccurrences = new List<int>();
        List<string> pair = new List<string>();
        foreach (string card in separateCards) valuesOccurrences.Add(separateCards.FindAll(x => x[0] == card[0]).Count());
        if (valuesOccurrences.Contains(2))
        {
            foreach (string card in separateCards)
            {
                if (separateCards.FindAll(x => x[0] == card[0]).ToList().Count() == 2) pair.Add(card);
                else noComboHand.Add(card);
            }
            kicker = pair[0];
            pair.Clear();
            return true;
        }
        kicker = separateCards[4];
        noComboHand.Clear();
        for (int i = 0; i < separateCards.Count - 1; i++) noComboHand.Add(separateCards[i]);
        return false;
    }
}
