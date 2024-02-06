using GGJ.Data;

namespace GGJ.Infrastructure.Services.Services.SaveLoad
{
    public interface ISavedProgressWriter : ISavedProgressReader
    {
        void Save(PlayerProgress progress);
    }
}