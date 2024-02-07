using GGJ.Infrastructure.Services;
using UnityEngine;

namespace GGJ.Infrastructure.AssetManagement
{
    /// <summary>
    /// Represents an AssetProvider that loads <see cref="GameObject"/>
    /// </summary>
    public interface IAssetProvider : IService
    {
        /// <summary>
        /// Instantiate object from path
        /// </summary>
        /// <param name="path">Path to prefab</param>
        /// <returns>Instantiated <see cref="GameObject"/></returns>
        GameObject Instantiate(string path);
        
        /// <summary>
        /// Instantiate object from path with custom position
        /// </summary>
        /// <param name="path">Path to prefab</param>
        /// <param name="at">Custom position</param>
        /// <returns>Instantiated <see cref="GameObject"/></returns>
        GameObject Instantiate(string path, Vector3 at);
    }
}