﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Core.Services;

namespace Sample.Core
{
    public class Calculator : ICalculator
    {
        public int Add(int no1, int no2)
        {
            var sum = no1 + no2;
            return sum;
        }
    }
}
