﻿using System;

namespace BaseEngine.Extensions
{
    public static class RandomExtensions
    {
        public static double NextDouble(this Random random, double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }
}
