using System.Collections;
using System.Collections.Generic;
using GGJ.Infrastructure.Services;
using GGJ.Infrastructure.Services.Services.SaveLoad;
using UnityEngine;

namespace GGJ.Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressWriter> ProgressWriters { get; }
        List<ISavedProgressReader> ProgressReaders { get; }
        GameObject CreateCharacter(Vector3 at);
        GameObject CreateHud();
        void CleanUp();
    }
}