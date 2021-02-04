using UnityEngine;

namespace Settings
{
    public class SettingsRepositoryPlayerPrefs : ISettingsRepository
    {
        public void SaveBallColor(Color colorHex)
        {
            PlayerPrefs.SetString("ball_color", "#"+ColorUtility.ToHtmlStringRGBA(colorHex));
           
        }

        public Color LoadBallColor()
        {
            return ColorUtility.TryParseHtmlString(PlayerPrefs.GetString("ball_color"), out var color) 
                ? color : Color.white;
        }

        public void UseCustomColor(bool enabled)
        {
            PlayerPrefs.SetInt("use_custom_ball", enabled ? 1 : -1);
        }

        public bool IsCustomColorInUse => PlayerPrefs.GetInt("use_custom_ball") > 0;
    }
}