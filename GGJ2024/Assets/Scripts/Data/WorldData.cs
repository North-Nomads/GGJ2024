using System;

namespace GGJ.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel { get; private set; }
        
        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
        }
    }
}