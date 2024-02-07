using GGJ.Data;

namespace GGJ.Infrastructure.Services.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}