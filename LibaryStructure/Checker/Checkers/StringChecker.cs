using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryStructure.Checker.Checkers
{
    public class StringChecker : Checker<string>, IChecker
    {
        public override bool CheckInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            setInput(input);

            return true;
        }
    }
}
