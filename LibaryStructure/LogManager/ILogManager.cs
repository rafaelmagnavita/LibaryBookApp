using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryStructure.LogManager
{
    public interface ILogManager
    {
        void Write(object content);
        T Read<T>();
    }
}
