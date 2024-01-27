using UnityEngine;

namespace GGJ.Fishing
{
	public abstract class CatchableItemBase : MonoBehaviour
	{
		public abstract void OnCatchEnd(PlayerFishing fisher);
	}
}