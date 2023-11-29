using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDomain.Validator
{
    public interface IValidator<T>
    {
        string _invalidFields { get; set; }

        public bool IsValid(T entity);

    }
}
