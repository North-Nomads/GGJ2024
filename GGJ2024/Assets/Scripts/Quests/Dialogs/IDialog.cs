using System;
using System.Collections;
using GGJ.Infrastructure;
using UnityEngine;

namespace GGJ.Dialogs
{
    public interface IDialog
    {
        event Action DialogStarted;
        event Action DialogEnded;

        void Initialize(GameObject character, DialogView dialogView);
        IEnumerator ShowDialog(ICoroutineRunner coroutineRunner, string title, string[] dialogVariants);
        void ShowPhrase(ICoroutineRunner coroutineRunner, string title, string dialogVariant);
    }
}