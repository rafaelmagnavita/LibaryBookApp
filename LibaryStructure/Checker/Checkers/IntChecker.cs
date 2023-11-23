using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryStructure.Checker.Checkers
{
    public class IntChecker : Checker<int>, IChecker
    {

        public override bool CheckInput(string input)
        {
            var intCheck = int.TryParse(input, out int intValue);
            if (!intCheck)
                return false;

            setInput(intValue);
            return true;
        }
    }
}
