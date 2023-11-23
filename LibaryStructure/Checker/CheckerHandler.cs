using LibaryStructure.Checker.Checkers;
using LibaryStructure.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryStructure.Checker
{
    public class CheckerHandler
    {
        private IChecker _checker;
        private Type _type;
        private ILogManager _console;

        public CheckerHandler(ILogManager console)
        {
            _console = console;
        }

        private void ChooseChecker()
        {
            switch (_type)
            {
                case Type intType when intType == typeof(int):
                    if (_checker?.GetType() != typeof(IntChecker))
                        _checker = new IntChecker();
                    break;
                case Type stringType when stringType == typeof(string):
                    if (_checker?.GetType() != typeof(StringChecker))
                        _checker = new StringChecker();
                    break;
                case Type dateTimeType when dateTimeType == typeof(DateTime):
                    if (_checker?.GetType() != typeof(DateChecker))
                        _checker = new DateChecker();
                    break;
            }
        }

        public T GetConvertedInput<T>(string input)
        {
            _type = typeof(T);
            ChooseChecker();

            if (_checker == null)
                throw new ArgumentNullException("Invalid Type!");

            var isExpectedValue = _checker.CheckInput(input);
            while (!isExpectedValue)
            {
                _console.Write($"Invalid Input, please digit a valid {typeof(T)}!");
                input = _console.simpleRead();
                isExpectedValue = _checker.CheckInput(input);
            }
            return (T)_checker.ReturnInput();
        }
    }
}
