using UnityEngine;

namespace Assets.Scripts.Settings
{
    public sealed class GameSettings
    {
        private ISettingsRepository _repository = new SettingsRepositoryPlayerPrefs();

        public static GameSettings Instance { get; } = new GameSettings();

        public bool IsCustomBallColorInUse
        {
            get => _repository.isCustomColorInUse;
            set => _repository.UseCustomColor(value);
        }

        public Color CustomBallColor
        {
            get => _repository.LoadBallColor();
            set => _repository.SaveBallColor(value);
        }

        private GameSettings()
        {
        }
    }
}