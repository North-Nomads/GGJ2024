using GGJ.Fishing.Minigames;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGJ.Fishing
{
	public class FishingEventProvider : MonoBehaviour
	{
		[SerializeField] private List<RandomSelectionInfo<MinigameBase>> availableMinigames;
		[SerializeField] private List<RandomSelectionInfo<CatchableItemBase>> availableCatchableItems;

		public MinigameBase GetRandomGame()
		{
			return SelectRandomItem(availableMinigames);
		}

		public CatchableItemBase GetRandomCatchable()
		{
			return SelectRandomItem(availableCatchableItems);
		}

		private static T SelectRandomItem<T>(List<RandomSelectionInfo<T>> items)
		{
			float probabilitySum = items.Where(x => x.Probability > 0).Sum(x => x.Probability);
			float result = UnityEngine.Random.Range(0, probabilitySum);
			foreach (var item in items)
			{
				if (item.Probability <= 0)
					continue;
				result -= item.Probability;
				if (result <= 0)
					return item.Value;
			}
			return items.Last().Value;
		}

		[Serializable]
		public struct RandomSelectionInfo<T>
		{
			[SerializeField] public T Value;

			[SerializeField] public float Probability;
		}
	}
}