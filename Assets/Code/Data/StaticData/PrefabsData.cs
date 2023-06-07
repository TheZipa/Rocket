using Code.Core.Environment;
using Code.Core.Rocket;
using Code.Core.UI;
using Code.Core.UI.Gameplay;
using Code.Core.UI.Menu;
using UnityEngine;

namespace Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "Prefabs Data", menuName = "Static Data/Prefabs Data")]
    public class PrefabsData : ScriptableObject
    {
        [Header("Gameplay")]
        public EnvironmentPart[] EnvironmentPartPrefabs;
        public EnvironmentPart StartEnvironmentPartPrefab;
        public Rocket RocketPrefab;
        [Header("UI")]
        public GameObject RootCanvasPrefab;
        public MainMenu MainMenuPrefab;
        public GameOverWindow GameOverWindowPrefab;
        public MeterCounterView MeterCounterViewPrefab;
    }
}