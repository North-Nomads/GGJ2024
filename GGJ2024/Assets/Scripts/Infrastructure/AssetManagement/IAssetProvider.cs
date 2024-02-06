using GGJ.Infrastructure.Services;
using UnityEngine;

namespace GGJ.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}