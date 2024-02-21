using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GGJ.Fishing.Minigames
{
	public class SimpleFishingMinigame : MonoBehaviour
	{
		[SerializeField] private Image indicator;
		[SerializeField] private RectTransform fish;
        [SerializeField] private float passiveSpeed;
        [SerializeField] private float activeSpeed;
        [SerializeField] private float difficulty;
        [SerializeField] private int progressSuccessValue;
        [SerializeField] private int progressFailureValue;

        private float _speed;
        private float _direction;
        private float _cd;
        private float _progress = 10;

        public event System.EventHandler<bool> OnGameEnded;

        private void Update()
        {
            UpdateSpeed();
            MoveFish();
            if (_progress <= 0)
            {
                OnGameEnded(this, false);
                Destroy(gameObject);
            }
            else if (_progress >= 100)
            {
                OnGameEnded(this, true);
                Destroy(gameObject);
            }
            indicator.fillAmount = _progress / 100;
        }
        private void MoveFish()
        {
            fish.anchoredPosition += Vector2.right * _speed;
            float xPos = Mathf.Abs(fish.anchoredPosition.x);
            if (xPos < 200)
            {
                _progress += progressSuccessValue;
            }
            else if (xPos < 300)
            {
                _progress -= progressFailureValue;
            }
            else
            {
                _progress = 0;
            }
        }

        private void UpdateSpeed()
        {
            _cd -= Time.deltaTime;
            if (_cd <= 0)
            {
                _direction = Random.value - 0.5f;
                _cd = Random.value / difficulty;
            }
            _speed = passiveSpeed * Mathf.Sign(_direction);
            if (Mouse.current.leftButton.isPressed)
            {
                _speed += activeSpeed;
            }
            else if (Mouse.current.rightButton.isPressed)
            {
                _speed -= activeSpeed;
            }
        }
    }
}