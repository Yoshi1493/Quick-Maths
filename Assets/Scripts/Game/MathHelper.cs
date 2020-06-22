using System.Collections.Generic;
using UnityEngine;

public static class MathHelper
{
    const string IntDisplayFormat = "N0";

    //return random number within the range (min, max) (exclusive)
    public static int GetRandomNumber((int min, int max) range)
    {
        return Random.Range(range.min + 1, range.max);
    }

    //return random value of <numbers>, excluding the first value
    public static int GetRandomNumber(List<int> numbers)
    {
        return numbers[GetRandomNumber((0, numbers.Count))];
    }

    //return list of factors of <num>, excluding 1 and itself
    public static List<int> GetFactors(int num)
    {
        List<int> factors = new List<int>();

        int max = (int)Mathf.Sqrt(num);
        for (int i = 2; i <= max; i++)
        {
            if (num % i == 0)
            {
                factors.Add(i);
                if (i != num / i)
                {
                    factors.Add(num / i);
                }
            }
        }

        return factors;
    }

    //return 10^num
    public static int TenToThePowerOf(int num)
    {
        return (int)Mathf.Pow(10, num);
    }

    //return i as a string in number format
    public static string ConvertToString(int i)
    {
        return i.ToString(IntDisplayFormat);
    }
}