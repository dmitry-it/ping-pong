using UnityEngine;

namespace Assets.Scripts.Settings
{
    public interface ISettingsRepository
    {
        void SaveBallColor(Color color);
        Color LoadBallColor();

        void UseCustomColor(bool enabled);

        bool isCustomColorInUse { get;  }
    }
}