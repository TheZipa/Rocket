using Code.Data.Progress;
using Code.Extensions;
using UnityEngine;

namespace Code.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoad
    {
        private const string ProgressKey = "Progress";

        public void SaveProgress(PlayerProgress progress) => 
            PlayerPrefs.SetString(ProgressKey, progress.ToJson());

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}