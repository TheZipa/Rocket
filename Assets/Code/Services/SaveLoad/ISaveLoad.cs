using Code.Data.Progress;
using Code.Infrastructure.ServiceContainer;

namespace Code.Services.SaveLoad
{
    public interface ISaveLoad : IService
    {
        void SaveProgress(PlayerProgress progress);
        PlayerProgress LoadProgress();
    }
}