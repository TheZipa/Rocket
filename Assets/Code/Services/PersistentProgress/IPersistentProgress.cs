using Code.Data;
using Code.Data.Progress;
using Code.Infrastructure.ServiceContainer;

namespace Code.Services.PersistentProgress
{
    public interface IPersistentProgress : IService
    {
        PlayerProgress Progress { get; set; }
        void SetNewMeterRecord(float metersRecord);
        void SetCollectables(CollectableProgressData collectableProgressData);
    }
}