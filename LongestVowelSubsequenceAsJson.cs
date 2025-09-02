using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class LongestVowelSubsequenceAsJsonTask2
{
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        if (words == null)
            return JsonSerializer.Serialize(new List<object>());

        var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        var results = new List<object>();

        foreach (string word in words)
        {
            string longestSequence = "";
            string currentSequence = "";
            
            foreach (char c in word)
            {
                if (vowels.Contains(c))
                {
                    currentSequence += c;
                }
                else
                {
                    if (currentSequence.Length > longestSequence.Length)
                    {
                        longestSequence = currentSequence;
                    }
                    currentSequence = "";
                }
            }

            if (currentSequence.Length > longestSequence.Length)
            {
                longestSequence = currentSequence;
            }

            results.Add(new
            {
                word = word,
                sequence = longestSequence,
                length = longestSequence.Length
            });
        }

        return JsonSerializer.Serialize(results);
    }
}