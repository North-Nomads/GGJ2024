using GGJ.Data;

namespace GGJ.Infrastructure.Services.Services.SaveLoad
{
    /// <summary>
    /// Represents a base interface for save and load service
    /// </summary>
    public interface ISaveLoadService : IService
    {
        /// <summary>
        /// Will saved progress to any implementation of progress service
        /// </summary>
        void SaveProgress();
        
        /// <summary>
        /// Will loaded progress from any implementation of progress service
        /// </summary>
        /// <returns>An object of <see cref="PlayerProgress"/> that represents an info about player progress</returns>
        PlayerProgress LoadProgress();
    }
}