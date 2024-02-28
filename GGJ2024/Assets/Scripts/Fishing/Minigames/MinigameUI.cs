using UnityEngine;
using UnityEngine.UI;

namespace GGJ.Fishing
{
	public class MinigameUI : MonoBehaviour
	{
		[SerializeField] private Image fishIcon;
		[SerializeField] private Image progressIndicator;
		[SerializeField] private float referenceWidth;
		[SerializeField] private RectTransform targetTransform;
		[SerializeField] private Image leftBorder;
		[SerializeField] private Image rightBorder;

        private void Start()
        {
			leftBorder.rectTransform.anchoredPosition = new(targetTransform.anchoredPosition.x - referenceWidth * 0.3f, 0);
			rightBorder.rectTransform.anchoredPosition = new(targetTransform.anchoredPosition.x + referenceWidth * 0.3f, 0);
        }

		public void NotifyUpdate(float position, float progress)
		{
			fishIcon.rectTransform.anchoredPosition = new(targetTransform.anchoredPosition.x + referenceWidth * position, 0);
			progressIndicator.fillAmount = progress;
		}
	}
}