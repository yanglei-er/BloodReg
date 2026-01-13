using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace BloodReg.Extensions
{
    public partial class TextBoxAttachedProperties
    {
        [GeneratedRegex("[^0-9]+")]
        private static partial Regex MyRegex();

        public static bool GetIsOnlyNumber(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsOnlyNumberProperty);
        }

        public static void SetIsOnlyNumber(DependencyObject obj, bool value)
        {
            obj.SetValue(IsOnlyNumberProperty, value);
        }

        public static readonly DependencyProperty IsOnlyNumberProperty =
            DependencyProperty.RegisterAttached("IsOnlyNumber", typeof(bool), typeof(TextBox), new PropertyMetadata(false,
                (s, e) =>
                {
                    if (s is TextBox textBox)
                    {
                        textBox.SetValue(InputMethod.IsInputMethodEnabledProperty, !(bool)e.NewValue);
                        textBox.PreviewTextInput += TxtInput;
                    }
                }));

        private static void TxtInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = MyRegex().IsMatch(e.Text);
        }
    }
}
