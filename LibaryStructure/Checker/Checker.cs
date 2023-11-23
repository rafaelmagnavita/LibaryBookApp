using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryStructure.Checker
{
    public class Checker<T> : IChecker
    {
        public T _convertedInput { get; private set; }

        protected void setInput(T value)
        {
            _convertedInput = value;
        }

        public virtual bool CheckInput(string input)
        {
            throw new NotImplementedException();
        }

        public object ReturnInput()
        {
            return _convertedInput;
        }
    }
}
