using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryStructure.Checker.Checkers
{
    public class IntChecker : IChecker
    {
        public object convertedInput { get; private set; }

        public bool CheckInput(string input)
        {
            var intCheck = int.TryParse(input, out int intValue);
            if (!intCheck)
                return false;

            convertedInput = intValue;
            return true;
        }
    }
}
