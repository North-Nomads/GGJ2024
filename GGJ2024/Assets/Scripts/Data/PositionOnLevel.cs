using System;

namespace GGJ.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public string Level { get; set; }
        public Vector3Data Position { get; set; }

        public PositionOnLevel(string initialLevel)
        {
            Level = initialLevel;
        }

        public PositionOnLevel(string level, Vector3Data position)
        {
            Level = level;
            Position = position;
        }
    }
}