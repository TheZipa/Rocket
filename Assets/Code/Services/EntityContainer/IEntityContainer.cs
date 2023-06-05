using Code.Infrastructure.ServiceContainer;

namespace Code.Services.EntityContainer
{
    public interface IEntityContainer : IService
    {
        void RegisterEntity<TEntity>(TEntity entity) where TEntity : class, IFactoryEntity;
        TEntity GetEntity<TEntity>();
        void Dispose();
    }
}