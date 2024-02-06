using GGJ.Data;

namespace GGJ.Infrastructure.Services.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}