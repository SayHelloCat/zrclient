
using System;
using System.Windows;
using System.Windows.Controls;

namespace ZrClient.Common
{
    public class PasswordHelper
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordHelper),
                new PropertyMetadata("", OnPasswordPropertyChanged));

        public static string GetPassword(DependencyObject dobj)
        {
            return dobj.GetValue(PasswordProperty) as string;
        }

        public static void SetPassword(DependencyObject d, string value)
        {
            d.SetValue(PasswordProperty, value);
        }

        public static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = d as PasswordBox;
            if (passwordBox == null) return;
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
            if (!_isUpdate)
                passwordBox.Password = e.NewValue?.ToString();
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordHelper),
             new PropertyMetadata(default(bool), OnAttachPropertyChanged));

        public static bool GetAttach(DependencyObject dobj)
        {
            return (bool)dobj.GetValue(PasswordProperty);
        }

        public static void SetAttach(DependencyObject d, bool value)
        {
            d.SetValue(PasswordProperty, value);
        }

        public static void OnAttachPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = d as PasswordBox;
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }


        private static bool _isUpdate = false;
        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            _isUpdate = true;
            SetPassword(passwordBox, passwordBox.Password);
            _isUpdate = false;
        }
    }
}