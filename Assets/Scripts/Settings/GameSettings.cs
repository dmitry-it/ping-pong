using UnityEngine;

namespace Settings
{
    public sealed class GameSettings
    {
        private readonly ISettingsRepository _repository = new SettingsRepositoryPlayerPrefs();

        public static GameSettings Instance { get; } = new GameSettings();

        public bool IsCustomBallColorInUse
        {
            get => _repository.IsCustomColorInUse;
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