using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cultris_II.ViewModels.Base
{
    public class ButtonVM : BaseVM
    {
        private bool _isEnabled = false, _isVisible = false;
        private string _text;
        private Color _textColor, _backgroundColor;

        public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value, "", () => ButtonEnabledChange(value)); }
        public bool IsVisible { get => _isVisible; set => SetProperty(ref _isVisible, value); }
        public string Text { get => _text; set => SetProperty(ref _text, value); }
        public Color TextColor { get => _textColor; set => SetProperty(ref _textColor, value); }
        public Color BackgroundColor { get => _backgroundColor; set => SetProperty(ref _backgroundColor, value); }

        private Command<object> _command = new Command<object>(value => { });
        public Command<object> Command { get => _command; private set => SetProperty(ref _command, value); }
        public Action<object> ActionCommand { set => Command = new Command<object>(value, obj => IsEnabled); }

        private void ButtonEnabledChange(bool enabled)
        {
            if (enabled) { BackgroundColor = Settings.BackgroundColor; }
            _command.ChangeCanExecute();
        }
        private void SetSettings(ButtonSettings settings)
        {
            if (settings != null)
            {
                IsVisible = true;
                IsEnabled = true;
                Text = settings.Text;
                TextColor = settings.TextColor;
                BackgroundColor = settings.BackgroundColor;
            }
            else
            {
                IsVisible = false;
                IsEnabled = false;
            }
            _settings = settings;
        }
        private ButtonSettings _settings = new ButtonSettings();
        public ButtonSettings Settings { get=> _settings; set => SetSettings(value); }

        public class ButtonSettings
        {
            public string Text { get; set; }
            public Color TextColor { get; set; }
            public Color BackgroundColor { get; set; }
        }
    }
}
