using System;

namespace GGJ.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData { get; private set; }

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
        }
    }
}