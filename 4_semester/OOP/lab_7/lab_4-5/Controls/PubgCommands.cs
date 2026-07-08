using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab_4_5.Controls
{
    public static class PubgCommands
    {
        public static readonly RoutedUICommand ResetFilters = new RoutedUICommand(
            "Сбросить все фильтры", "ResetFilters", typeof(PubgCommands),
            new InputGestureCollection() { new KeyGesture(Key.R, ModifierKeys.Control) }
        );
    }
}
