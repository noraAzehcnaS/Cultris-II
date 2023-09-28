using System;
using Xamarin.Forms;

namespace Cultris_II.ViewModels.Base
{
   public class LazyButton : BaseVM
    {
        #region Properties
        private bool _isEnabled = true, _isVisible = true;
        private string _text;
        private Color _textColor, _backgroundColor;

        public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value, "", () => ButtonEnabledChange(value)); }
        public bool IsVisible { get => _isVisible; set => SetProperty(ref _isVisible, value); }
        public string Text { get => _text; set => SetProperty(ref _text, value); }
        public Color TextColor { get => _textColor; set => SetProperty(ref _textColor, value); }
        public Color BackgroundColor { get => _backgroundColor; set => SetProperty(ref _backgroundColor, value); }

        private Command _command = new Command(value => { });
        public Command Command { get => _command; private set => SetProperty(ref _command, value); }
        #endregion
        private Action CommandAction { set => Command = new Command(value, () => IsEnabled); }

        private void ButtonEnabledChange(bool enabled)
        {
            if (enabled) { SetSettings(SettingsA); }
            _command.ChangeCanExecute();
        }
        public class LazyButtonSettings
        {
            public string Text { get; set; }
            public Color TextColor { get; set; }
            public Color BackgroundColor { get; set; }
        }

        public LazyButtonSettings SettingsA = new LazyButtonSettings { BackgroundColor = Color.Green, TextColor = Color.White, Text = "Function A" };
        public LazyButtonSettings SettingsB = new LazyButtonSettings { BackgroundColor = Color.Red, TextColor = Color.White, Text = "Function B" };

        private void SetSettings(LazyButtonSettings settings)
        {
            if (settings == null) { return; }
            Text = settings.Text;
            TextColor = settings.TextColor;
            BackgroundColor = settings.BackgroundColor;
            _settings = settings;
        }
        private LazyButtonSettings _settings = new LazyButtonSettings();

        public LazyButton()
        {
            SetSettings(SettingsA);
            CommandAction = () => Toggle(null, null, SettingsA, SettingsB);
        }

        public LazyButton(Action actionA, string textA)
        {
            SettingsA.Text = textA;
            SetSettings(SettingsA);
            CommandAction = actionA;
        }

        public LazyButton(Action actionA, string textA, Action actionB, string TextB)
        {
            SettingsA.Text = textA;
            SettingsB.Text = TextB;
            SetSettings(SettingsA);
            CommandAction = () => Toggle(actionA, actionB, SettingsA, SettingsB);
        }

        private void Toggle(Action actionA, Action actionB, LazyButtonSettings sA, LazyButtonSettings sB)
        {
            if (_settings.Equals(sA))
            {
                actionA?.Invoke();
                SetSettings(sB);
            }
            else
            {
                actionB?.Invoke();
                SetSettings(sA);
            }
        }
    }
}
