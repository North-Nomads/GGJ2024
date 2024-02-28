using UnityEngine;

namespace GGJ.Infrastructure
{
    public interface ICoroutineStopper : ICoroutineRunner
    {
        void StopCoroutine(Coroutine coroutine);
    }
}