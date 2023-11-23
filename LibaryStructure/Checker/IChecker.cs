using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryStructure.Checker
{
    public interface IChecker
    {
        bool CheckInput(string input);

        object ReturnInput();
    }
}
