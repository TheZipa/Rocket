using Code.Data.StaticData;
using UnityEngine;

namespace Code.Services.StaticData.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private const string PrefabsDataPath = "StaticData/Prefabs Data";
        private const string MainConfigurationPath = "StaticData/Main Configuration";

        public PrefabsData LoadPrefabsData() => Resources.Load<PrefabsData>(PrefabsDataPath);

        public MainConfiguration LoadMainConfiguration() => Resources.Load<MainConfiguration>(MainConfigurationPath);
    }
}