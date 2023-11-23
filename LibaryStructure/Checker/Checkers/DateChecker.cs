using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryStructure.Checker.Checkers
{
    public class DateChecker : Checker<DateTime>, IChecker
    {

        public  override bool CheckInput(string input)
        {
            var dateCheck = DateTime.TryParse(input, out DateTime dateValue);
            if (!dateCheck)
                return false;

            setInput(dateValue);
            return true;
        }
    }
}
