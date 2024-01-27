using GGJ.Inventory;
using UnityEngine;

namespace GGJ.Fishing
{
	public abstract class CatchableItemBase : MonoBehaviour
	{
		[SerializeField] private ItemInfo item;

		public ItemInfo Item => item;

		public abstract void OnCatchEnd(PlayerFishing fisher);
	}
}