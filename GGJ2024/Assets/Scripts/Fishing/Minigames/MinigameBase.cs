using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GGJ.Fishing.Minigames
{
	public abstract class MinigameBase : MonoBehaviour
	{
		private TaskCompletionSource<bool> _gameEndTaskSource;

		public Task<bool> StartGameAsync()
		{
			_gameEndTaskSource = new TaskCompletionSource<bool>();
			return _gameEndTaskSource.Task;
		}

		protected void OnWin()
		{
			Debug.Log($"Won {GetType()}");
			_gameEndTaskSource.SetResult(true);
		}

		protected void OnLose()
		{
            Debug.Log($"Lost {GetType()}");
            _gameEndTaskSource.SetResult(false);
        }
    }
}