using LibaryDomain.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDomain.Entities
{
    public class LibraryEntity<T> : ILibraryEntity<T>
    {
        private  T _entity;
        private IValidator<T> _validator;

        public LibraryEntity(IValidator<T> validator)
        {
            _validator = validator;
        }
        public virtual bool IsValid()
        {
            return _validator.IsValid(_entity);
        }

        public virtual string GetInvalid()
        {
            _validator.IsValid(_entity);
            return _validator._invalidFields;
        }

        public virtual void SetEntity(T entity)
        {
            _entity = entity;
        }
    }
}
