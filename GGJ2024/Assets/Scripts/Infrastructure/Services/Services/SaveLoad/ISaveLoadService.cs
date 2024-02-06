using GGJ.Data;

namespace GGJ.Infrastructure.Services.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}