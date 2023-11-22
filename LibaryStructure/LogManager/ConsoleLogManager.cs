using LibaryStructure.Checker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryStructure.LogManager
{
    public class ConsoleLogManager : ILogManager
    {
        CheckerHandler _checkerHandler;
        public ConsoleLogManager()
        {
            _checkerHandler = new CheckerHandler();
        }
        public bool yesOrNoBox(string content)
        {
            Console.WriteLine($"{content} (Y/N)?");
            var response = Read<string>();
            while (response.Trim().ToLower() != "y" && response.Trim().ToLower() != "n")
            {
                Console.WriteLine("Invalid answer!");
                response = Read<string>();
            }

            bool responseBool = response.Trim().ToLower() == "y";
            return responseBool;
        }

        public T Read<T>()
        {
            var input = Console.ReadLine();
            var result = _checkerHandler.GetConvertedInput<T>(input);
            return result;
        }

        public void Write(object content)
        {
            if (string.IsNullOrEmpty(content?.ToString()))
                throw new ArgumentException("Invalid Message");

            var contentString = content.ToString();

            Console.WriteLine(content);
        }
    }
}
