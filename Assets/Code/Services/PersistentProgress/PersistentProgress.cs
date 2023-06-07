using Code.Data.Progress;
using Code.Services.SaveLoad;

namespace Code.Services.PersistentProgress
{
    public class PersistentProgress : IPersistentProgress
    {
        public PlayerProgress Progress { get; set; }
        private readonly ISaveLoad _saveLoad;

        public PersistentProgress(ISaveLoad saveLoad) => _saveLoad = saveLoad;

        public void SetNewMeterRecord(float metersRecord)
        {
            Progress.MetersRecord = metersRecord;
            _saveLoad.SaveProgress(Progress);
        }
    }
}