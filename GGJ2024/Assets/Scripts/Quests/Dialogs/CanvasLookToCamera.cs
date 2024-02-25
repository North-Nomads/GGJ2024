using UnityEngine;

namespace GGJ.Dialogs
{
    public class CanvasLookToCamera : MonoBehaviour
    {
        private Camera _camera;
        private RectTransform _rectTransform;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _camera = Camera.main;
        }

        private void LateUpdate() => LookAtCamera();
        
        private void LookAtCamera() => _rectTransform.LookAt(_camera.transform);
    }
}