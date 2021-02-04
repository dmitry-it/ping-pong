using UnityEngine;

namespace Settings
{
    public interface ISettingsRepository
    {
        void SaveBallColor(Color color);
        Color LoadBallColor();

        void UseCustomColor(bool enabled);

        bool IsCustomColorInUse { get;  }
    }
}