using UnityEngine;

namespace GGJ
{
    /// <summary>
    /// This class is only needed for Unity events handling. I hate Unity.
    /// </summary>
    [CreateAssetMenu(fileName = "New location info", menuName = "Location Definition")]
    public class LocationInfo : ScriptableObject
    {
        [SerializeField] private FastTravelUI.GameLocation locationInfo;

        public FastTravelUI.GameLocation Location => locationInfo;

        public override string ToString()
        {
            return locationInfo.ToString();
        }
    }
}


