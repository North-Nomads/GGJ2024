using System.Collections;
using System.Collections.Generic;
using GGJ.Infrastructure.Services;
using GGJ.Infrastructure.Services.Services.SaveLoad;
using UnityEngine;

namespace GGJ.Infrastructure.Factories
{
    /// <summary>
    /// Represents a factory to instantiate some objects
    /// </summary>
    public interface IGameFactory : IService
    {
        /// <summary>
        /// A container of objects that can write a progress themselves
        /// </summary>
        List<ISavedProgressWriter> ProgressWriters { get; }
        
        /// <summary>
        /// A container of objects that can read a progress and load it after
        /// </summary>
        List<ISavedProgressReader> ProgressReaders { get; }
        
        /// <summary>
        /// Creates a character with custom position
        /// </summary>
        /// <param name="at">Next position on character</param>
        /// <returns>Character <see cref="GameObject"/></returns>
        GameObject CreateCharacter(Vector3 at);
        
        /// <summary>
        /// Creates a HUD
        /// </summary>
        /// <returns>HUD <see cref="GameObject"/></returns>
        GameObject CreateHud();
        
        /// <summary>
        /// Cleans a <see cref="ProgressReaders"/> and <see cref="ProgressWriters"/> containers
        /// </summary>
        void CleanUp();
    }
}