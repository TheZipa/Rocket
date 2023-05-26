using Code.Data.StaticData;
using Code.Services.StaticData.StaticDataProvider;

namespace Code.Services.StaticData
{
    public class StaticData : IStaticData
    {
        public PrefabsData Prefabs { get; private set; }
        
        private readonly IStaticDataProvider _staticDataProvider;

        public StaticData(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public void LoadStaticData()
        {
            Prefabs = _staticDataProvider.LoadPrefabsData();
        }
    }
}