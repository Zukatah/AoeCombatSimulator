using System;
using System.Collections.Generic;

namespace AoeCombatSimulator
{
    public static class ExtensionMethods
    {
        static Random rnd = new Random(Environment.TickCount);

        public static void Shuffle<T>(this IList<T> list) // used to shuffle the start formations of the armies (of course melee and ranged units seperately)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
