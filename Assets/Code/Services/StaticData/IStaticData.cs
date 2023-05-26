using Code.Data.StaticData;
using Code.Infrastructure.ServiceContainer;

namespace Code.Services.StaticData
{
    public interface IStaticData : IService
    {
        PrefabsData Prefabs { get; }
        void LoadStaticData();
    }
}