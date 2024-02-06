using GGJ.Data;

namespace GGJ.Infrastructure.Services.Services.PersistentProgress
{
    /// <summary>
    /// A basic interface for progress service
    /// </summary>
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}