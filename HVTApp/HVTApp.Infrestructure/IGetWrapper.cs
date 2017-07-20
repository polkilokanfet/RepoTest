namespace HVTApp.Infrastructure
{
    public interface IGetWrapper
    {
        void AddWrapperInDictionary(IWrapper<IBaseEntity> wrapper);

        TWrapper GetWrapper<TWrapper>(IBaseEntity model)
            where TWrapper : class, IWrapper<IBaseEntity>;

        TWrapper GetWrapper<TWrapper>()
            where TWrapper : class, IWrapper<IBaseEntity>;

        void RemoveWrapper(IBaseEntity model);
    }
}