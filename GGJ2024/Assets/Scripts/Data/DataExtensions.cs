using UnityEngine;

namespace GGJ.Data
{
    public static class DataExtensions
    {
        public static Vector3Data ToVector3Data(this Vector3 vector) => 
            new(vector.x, vector.y, vector.z);

        public static Vector3 ToVector3Unity(this Vector3Data vector) => 
            new(vector.X, vector.Y, vector.Z);

        public static Vector3 AddX(this Vector3 vector, float value)
        {
            vector.x += value;
            return vector;
        }
        
        public static Vector3 AddY(this Vector3 vector, float value)
        {
            vector.y += value;
            return vector;
        }
        
        public static Vector3 AddZ(this Vector3 vector, float value)
        {
            vector.z += value;
            return vector;
        }

        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);

        public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);
    }
}