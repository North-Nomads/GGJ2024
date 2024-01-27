using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace GGJ.UI
{
    public class ScreenBlur : MonoBehaviour
    {
        [Header("Blur")]
        [SerializeField] private CustomPassVolume uiBlur;
        [SerializeField] private float maxBlurValue;
        [SerializeField] private float maxBlurTime;
        [Tooltip("Shader variable name starting with _")] 
        [SerializeField] private string propertyName;
        [Header("Blackout")]
        [SerializeField] private Image blackoutImage;
        [SerializeField] private float targetBlackoutOpacity;
        
        private Material _material;

        private bool _isScreenBlurredNow;
        private float _currentAnimationTime;
        
        // Blur variables
        private float _currentBlurAnimationValue;
        private float _minBlurAnimationValue;
        private float _maxBlurAnimationValue;
        
        // Blackout variables
        private float _currentBlackoutAnimationValue;
        private float _minBlackoutAnimationValue;
        private float _maxBlackoutAnimationValue;
        private Color _temporaryColor;

        private void Start()
        {
            foreach (var pass in uiBlur.customPasses)
                if (pass is FullScreenCustomPass f)
                    _material = f.fullscreenPassMaterial;
            
            _material.SetFloat(propertyName, 0);
        }

        private void Update()
        {
            if (_currentAnimationTime > maxBlurTime)
                return;

            // Lerp animation
            _currentAnimationTime += Time.deltaTime;
            _currentBlurAnimationValue = Mathf.Lerp(_minBlurAnimationValue, _maxBlurAnimationValue, _currentAnimationTime / maxBlurTime);
            _currentBlackoutAnimationValue = Mathf.Lerp(_minBlackoutAnimationValue, _maxBlackoutAnimationValue, _currentAnimationTime / maxBlurTime);
            
            // Set value of a propertyName (that is set in shadergraph) to lerped value
            _material.SetFloat(propertyName, _currentBlurAnimationValue);
            
            // Set a value of alpha channel of a color to lerped value
            _temporaryColor = blackoutImage.color;
            _temporaryColor.a = _currentBlackoutAnimationValue;
            blackoutImage.color = _temporaryColor;
        }

        public void ToggleBlurring()
        {
            SetBlurringValues(!_isScreenBlurredNow);
        }

        public void SetBlurringValues(bool isBlurring)
        {
            if (isBlurring == _isScreenBlurredNow)
                return;
            
            _isScreenBlurredNow = isBlurring;
            if (isBlurring)
            {
                _minBlurAnimationValue = 0;
                _maxBlurAnimationValue = maxBlurValue;

                _minBlackoutAnimationValue = 0;
                _maxBlackoutAnimationValue = targetBlackoutOpacity;
                
                _currentAnimationTime = 0;
                return;
            }
            _minBlurAnimationValue = maxBlurValue;
            _maxBlurAnimationValue = 0;

            _minBlackoutAnimationValue = targetBlackoutOpacity;
            _maxBlackoutAnimationValue = 0;
            
            _currentAnimationTime = 0;
        }
    }
}