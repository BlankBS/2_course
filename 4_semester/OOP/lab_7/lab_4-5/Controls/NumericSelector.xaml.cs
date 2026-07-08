using System;
using System.Windows;
using System.Windows.Controls;

namespace lab_4_5.Controls
{
    public partial class NumericSelector : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericSelector),
                new FrameworkPropertyMetadata(1,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnValueChanged),
                    new CoerceValueCallback(CoerceValue)),
                new ValidateValueCallback(ValidateValue));

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static bool ValidateValue(object value)
        {
            return (int)value >= -100;
        }

        private static object CoerceValue(DependencyObject d, object baseValue)
        {
            int val = (int)baseValue;
            if (val < 0) return 0;
            if (val > 99) return 99;
            return val;
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        public NumericSelector()
        {
            InitializeComponent();
        }

        private void BtnDown_Click(object sender, RoutedEventArgs e) => Value--;
        private void BtnUp_Click(object sender, RoutedEventArgs e) => Value++;
    }
}