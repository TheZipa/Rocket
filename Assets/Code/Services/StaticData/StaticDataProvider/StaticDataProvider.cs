using Code.Data.StaticData;
using UnityEngine;

namespace Code.Services.StaticData.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private const string PrefabsDataPath = "StaticData/Prefabs Data";

        public PrefabsData LoadPrefabsData() => Resources.Load<PrefabsData>(PrefabsDataPath);
    }
}