using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ.Movement
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private float absoluteVerticalRange;
        [SerializeField] private float verticalSensivity;
        private float _userInput;
        private float _newY;
        private float _startPositionY;

        private void Start()
        {
            _startPositionY = transform.position.y;
        }

        public void HandleMouseInput(InputAction.CallbackContext context)
        {
            _userInput = context.ReadValue<float>();
            
            if (_userInput > 0)
                transform.position -= new Vector3(0, Time.deltaTime * verticalSensivity, 0);
            else if (_userInput < 0)
                transform.position += new Vector3(0, Time.deltaTime * verticalSensivity, 0);
            
            print(_userInput);
            if (transform.position.y > _startPositionY + absoluteVerticalRange)
                transform.position = new Vector3(transform.position.x, _startPositionY + absoluteVerticalRange, transform.position.z);
            else if (transform.position.y < _startPositionY - absoluteVerticalRange)
                transform.position = new Vector3(transform.position.x, _startPositionY - absoluteVerticalRange, transform.position.z);

        }
    }
}