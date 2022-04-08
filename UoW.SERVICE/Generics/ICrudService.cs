using System;
using System.Collections.Generic;
using System.Text;

namespace UoW.SERVICE.Generics
{
    public interface ICrudService<TEntity,TEntityDto>
    {
        TEntityDto Insert(TEntityDto entityDto);
        TEntityDto Update(TEntityDto entityDto);
        void Delete(int id);
        TEntityDto Get(int id);
        List<TEntityDto> GetAll();
    }
}
