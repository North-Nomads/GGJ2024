using GGJ.Data;

namespace GGJ.Infrastructure.Services.Services.SaveLoad
{
    /// <summary>
    /// Represents a simple reader of progress for any object
    /// </summary>
    public interface ISavedProgressReader
    {
        /// <summary>
        /// Will load a progress of object from <see cref="PlayerProgress"/>
        /// </summary>
        /// <param name="progress"></param>
        void Load(PlayerProgress progress);
    }
}