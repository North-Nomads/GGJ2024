using GGJ.Data;

namespace GGJ.Infrastructure.Services.Services.SaveLoad
{
    /// <summary>
    /// Represents a simple writer of progress for any object
    /// </summary>
    public interface ISavedProgressWriter : ISavedProgressReader
    {
        /// <summary>
        /// Will save an information from object to <see cref="PlayerProgress"/>
        /// </summary>
        /// <param name="progress">Any progress of player</param>
        void Save(PlayerProgress progress);
    }
}