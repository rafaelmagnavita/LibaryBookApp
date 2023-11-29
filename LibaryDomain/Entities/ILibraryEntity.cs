using LibaryDomain.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDomain.Entities
{
    public interface ILibraryEntity<T>
    {
        bool IsValid();
        string GetInvalid();
        void SetEntity(T entity);

    }
}
