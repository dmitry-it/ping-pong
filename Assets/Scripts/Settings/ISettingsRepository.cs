using UnityEngine;

namespace Settings
{
    public interface ISettingsRepository
    {
        bool IsCustomColorInUse { get; }
        void SaveBallColor(Color color);
        Color LoadBallColor();

        void UseCustomColor(bool enabled);
    }
}