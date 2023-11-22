using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryStructure.Checker.Checkers
{
    public class StringChecker : IChecker
    {
        public object convertedInput { get; private set; }

        public bool CheckInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            convertedInput = input;
            return true;
        }
    }
}
