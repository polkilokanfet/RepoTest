using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;

namespace HVTApp.TestDataGenerator
{
    public class TestBaseRepository<TModel, TWrapper> : IRepository<TModel, TWrapper>
        where TModel : class, IBaseEntity
        where TWrapper : class, IWrapper<TModel>
    {
        private readonly IDictionary<IBaseEntity, IWrapper<IBaseEntity>> _wrappers;
        private readonly IGetWrapper _getWrapper;

        public TestBaseRepository(IDictionary<IBaseEntity, IWrapper<IBaseEntity>> wrappers, IGetWrapper getWrapper)
        {
            _wrappers = wrappers;
            _getWrapper = getWrapper;
        }

        public void Add(TWrapper entity)
        {
            _wrappers.Add(entity.Model, entity);
        }

        public void AddRange(IEnumerable<TWrapper> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public void Delete(TWrapper entity)
        {
            _wrappers.Remove(entity.Model);
        }

        public void DeleteRange(IEnumerable<TWrapper> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public IEnumerable<TWrapper> Find(Func<TWrapper, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public List<TWrapper> GetAll()
        {
            return _wrappers.Values.Where(x => x.GetType() == typeof(TWrapper)).Select(x => (TWrapper)x).ToList();
        }

        public TWrapper GetWrapper()
        {
            throw new NotImplementedException();
        }

        public TWrapper GetWrapper(TModel model)
        {
            throw new NotImplementedException();
        }
    }
}
