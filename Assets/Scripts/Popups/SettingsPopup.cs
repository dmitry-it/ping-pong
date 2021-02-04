using Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Popups
{
    public class SettingsPopup : Popup
    {
        [SerializeField] private Toggle useCustomColorToggle;
        [SerializeField] private CanvasGroup colorChangeCanvasGroup;
        [SerializeField] private Slider colorR;
        [SerializeField] private Slider colorG;
        [SerializeField] private Slider colorB;
        [SerializeField] private Image preview;
        private Color _color;

        private void Start()
        {
            useCustomColorToggle.onValueChanged.AddListener(OnColorToggleValueChange);
            useCustomColorToggle.isOn = GameSettings.Instance.IsCustomBallColorInUse;

            _color = GameSettings.Instance.CustomBallColor;

            UpdateColor();

            colorR.value = _color.r;
            colorR.onValueChanged.AddListener(newValue =>
            {
                _color.r = newValue;
                UpdateColor();
            });
            colorG.value = _color.g;
            colorG.onValueChanged.AddListener(newValue =>
            {
                _color.g = newValue;
                UpdateColor();
            });
            colorB.value = _color.b;
            colorB.onValueChanged.AddListener(newValue =>
            {
                _color.b = newValue;
                UpdateColor();
            });
        }

        private void UpdateColor()
        {
            preview.color = _color;
            GameSettings.Instance.CustomBallColor = _color;
        }

        private void OnColorToggleValueChange(bool value)
        {
            colorChangeCanvasGroup.interactable = value;
            colorChangeCanvasGroup.blocksRaycasts = value;
            GameSettings.Instance.IsCustomBallColorInUse = value;
        }
    }
}