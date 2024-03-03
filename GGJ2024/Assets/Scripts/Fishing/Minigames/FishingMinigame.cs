using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GGJ.Fishing.Minigames
{
	public class FishingMinigame : MonoBehaviour
	{
        [SerializeField] private float passiveSpeed;
        [SerializeField] private float activeSpeed;
        [SerializeField] private float difficulty;
        [SerializeField] private float progressSuccessValue;
        [SerializeField] private float progressFailureValue;
        [SerializeField] private MinigameUI ui;

        private float _floatPosition;
        private float _progress;
        private float _currentSpeed;

        public event System.EventHandler<bool> OnGameEnded;

        private void Update()
        {
            UpdateSpeed();
            _floatPosition += _currentSpeed * Time.deltaTime;
            float distance = Mathf.Abs(_floatPosition);
            Debug.Log(distance);
            if (distance > 1f)
            {
                OnGameEnded(this, false);
                return;
            }
            else if (distance > 0.6f)
            {
                _progress -= progressFailureValue * Time.deltaTime;
            }
            else
            {
                _progress += progressSuccessValue * Time.deltaTime;
            }
            if (_progress > 1)
            {
                OnGameEnded(this, true);
                return;
            }
            ui.NotifyUpdate(_floatPosition, _progress);
        }

        private void UpdateSpeed()
        {
            if (Mouse.current.leftButton.isPressed)
            {
                _currentSpeed = activeSpeed;
            }
            else
            {
                float direction = Mathf.Sign(Random.value - 0.5f);
                _currentSpeed = passiveSpeed * direction;
            }
        }
    }
}