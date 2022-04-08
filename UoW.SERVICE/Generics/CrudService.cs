using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.CORE.Interface;
using UoW.DATA;
using UoW.DOMAIN;
using UoW.SERVICE.Dtos;

namespace UoW.SERVICE.Generics
{
    public class CrudService<TEntity, TEntityDto> : ICrudService<TEntity, TEntityDto> where TEntity : BaseEntity where TEntityDto : BaseEntityDto
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        public CrudService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        public TEntityDto Get(int id)
        {
            var entity = _repository.GetById(id).Result;
            var entityDto = _mapper.Map<TEntity, TEntityDto>(entity);
            return entityDto;
        }

        public virtual List<TEntityDto> GetAll()
        {
            var entities = _repository.All().Result;
            var entityDtos = _mapper.Map<IList<TEntity>,List<TEntityDto>>(entities);
            return entityDtos;
        }

        public TEntityDto Insert(TEntityDto entityDto)
        {
            var entity = _mapper.Map<TEntityDto, TEntity>(entityDto);
            _repository.Add(entity);
            _repository.Save();
            entityDto.ID = entity.ID;
            return entityDto;
        }

        public TEntityDto Update(TEntityDto entityDto)
        {
            var entity = _mapper.Map<TEntityDto, TEntity>(entityDto);
            _repository.Update(entity);
            _repository.Save();
            return entityDto;
        }
    }
}
