using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace lab_4_5.Controls
{
    public partial class StatusIndicator : UserControl
    {
        public static readonly RoutedEvent StatusChangedEvent = EventManager.RegisterRoutedEvent(
            "StatusChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(StatusIndicator));

        public static readonly RoutedEvent PreviewStatusChangedEvent = EventManager.RegisterRoutedEvent(
            "PreviewStatusChanged", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(StatusIndicator));

        public event RoutedEventHandler StatusChanged
        {
            add { AddHandler(StatusChangedEvent, value); }
            remove { RemoveHandler(StatusChangedEvent, value); }
        }

        public event RoutedEventHandler PreviewStatusChanged
        {
            add { AddHandler(PreviewStatusChangedEvent, value); }
            remove { RemoveHandler(PreviewStatusChangedEvent, value); }
        }

        public static void AddStatusChangedHandler(DependencyObject d, RoutedEventHandler handler)
        {
            if (d is UIElement uie) uie.AddHandler(StatusChangedEvent, handler);
        }
        public static void RemoveStatusChangedHandler(DependencyObject d, RoutedEventHandler handler)
        {
            if (d is UIElement uie) uie.RemoveHandler(StatusChangedEvent, handler);
        }

        public static void AddPreviewStatusChangedHandler(DependencyObject d, RoutedEventHandler handler)
        {
            if (d is UIElement uie) uie.AddHandler(PreviewStatusChangedEvent, handler);
        }
        public static void RemovePreviewStatusChangedHandler(DependencyObject d, RoutedEventHandler handler)
        {
            if (d is UIElement uie) uie.RemoveHandler(PreviewStatusChangedEvent, handler);
        }

        public StatusIndicator() { InitializeComponent(); }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            RaiseEvent(new RoutedEventArgs(PreviewStatusChangedEvent));
            RaiseEvent(new RoutedEventArgs(StatusChangedEvent));
        }
    }
}