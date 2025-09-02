using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;



public static class MaxIncreasingSubArrayAsJsonTask1
{
    public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
    {

        if (numbers == null || numbers.Count == 0)
            return JsonSerializer.Serialize(new List<int>());

        List<int> maxSubarray = new List<int>();
        int maxSum = int.MinValue;

        List<int> currentSubarray = new List<int>();
        int currentSum = 0;

        for (int i = 0; i < numbers.Count; i++)
        {
            if (currentSubarray.Count == 0 || numbers[i] > currentSubarray.Last())
            {
                currentSubarray.Add(numbers[i]);
                currentSum += numbers[i];
            }
            else
            {
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxSubarray = new List<int>(currentSubarray);
                }

                currentSubarray = new List<int> { numbers[i] };
                currentSum = numbers[i];
            }
        }

        if (currentSum > maxSum)
        {
            maxSum = currentSum;
            maxSubarray = new List<int>(currentSubarray);
        }

        return JsonSerializer.Serialize(maxSubarray);
    }
}