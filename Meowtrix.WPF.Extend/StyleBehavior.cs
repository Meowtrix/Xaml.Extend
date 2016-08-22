using System;
using System.Windows;
using Meowtrix.Linq;

namespace Meowtrix.WPF.Extend
{
    public static class StyleBehavior
    {
        public static Style Merge(params Style[] styles)
        {
            var newstyle = new Style();
            foreach (var style in styles)
            {
                if (newstyle.TargetType == null) newstyle.TargetType = style.TargetType;
                else if (newstyle.TargetType != style.TargetType) throw new ArgumentException("Styles must target to same type");
                newstyle.Triggers.AddRange(style.Triggers);
                newstyle.Setters.AddRange(style.Setters);
                newstyle.Resources.MergedDictionaries.Add(style.Resources);
            }
            newstyle.Seal();
            return newstyle;
        }

        public static object GetDynamicBaseStyle(FrameworkElement obj) => (object)obj.GetValue(DynamicBaseStyleProperty);

        public static void SetDynamicBaseStyle(FrameworkElement obj, object value) => obj.SetValue(DynamicBaseStyleProperty, value);

        // Using a DependencyProperty as the backing store for DynamicBaseStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DynamicBaseStyleProperty =
            DependencyProperty.RegisterAttached("DynamicBaseStyle", typeof(object), typeof(StyleBehavior), new PropertyMetadata(null, OnDynamicBaseStyleChanged));

        private static void OnDynamicBaseStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue) return;
            var c = d as FrameworkElement;
            if (c == null) throw new NotSupportedException("DynamicBaseStyle is only available on FrameworkElement");
            var key = e.NewValue;
            if (key != null)
                c.SetResourceReference(BasedOnStyleProperty, key);
            else
                c.ClearValue(BasedOnStyleProperty);
        }

        public static Style GetBasedOnStyle(FrameworkElement obj) => (Style)obj.GetValue(BasedOnStyleProperty);

        public static void SetBasedOnStyle(FrameworkElement obj, Style value) => obj.SetValue(BasedOnStyleProperty, value);

        // Using a DependencyProperty as the backing store for BasedOnStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BasedOnStyleProperty =
            DependencyProperty.RegisterAttached("BasedOnStyle", typeof(Style), typeof(StyleBehavior), new PropertyMetadata(null, OnBasedOnStyleChanged));

        private static void OnBasedOnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue) return;
            FrameworkElement c = d as FrameworkElement;
            if (c == null) throw new NotSupportedException("DynamicBaseStyle is only available on FrameworkElement");
            var baseStyle = e.NewValue as Style;
            var originalStyle = GetOriginalStyle(c);
            if (originalStyle == null)
            {
                originalStyle = c.Style;
                SetOriginalStyle(c, originalStyle);
            }
            c.Style = Merge(baseStyle, originalStyle);
        }

        public static Style GetOriginalStyle(FrameworkElement obj) => (Style)obj.GetValue(OriginalStyleProperty);

        public static void SetOriginalStyle(FrameworkElement obj, Style value) => obj.SetValue(OriginalStyleProperty, value);

        // Using a DependencyProperty as the backing store for OriginalStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginalStyleProperty =
            DependencyProperty.RegisterAttached("OriginalStyle", typeof(Style), typeof(StyleBehavior), new PropertyMetadata(null));
    }
}
