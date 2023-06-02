using Code.Data.StaticData;
using Code.Infrastructure.ServiceContainer;

namespace Code.Services.StaticData.StaticDataProvider
{
    public interface IStaticDataProvider : IService
    {
        PrefabsData LoadPrefabsData();
        MainConfiguration LoadMainConfiguration();
    }
}