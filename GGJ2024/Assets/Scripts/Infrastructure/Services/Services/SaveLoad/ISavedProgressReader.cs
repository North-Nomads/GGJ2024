using GGJ.Data;

namespace GGJ.Infrastructure.Services.Services.SaveLoad
{
    public interface ISavedProgressReader
    {
        void Load(PlayerProgress progress);
    }
}