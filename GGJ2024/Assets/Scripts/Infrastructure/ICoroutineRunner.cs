﻿using System.Collections;
using UnityEngine;

namespace GGJ.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}