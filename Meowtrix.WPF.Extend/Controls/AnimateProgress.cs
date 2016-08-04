using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Meowtrix.WPF.Extend.Controls
{
    public class AnimateProgress : ProgressBar
    {

        private FrameworkElement PART_Indicator;
        private FrameworkElement PART_Track;

        public InitAnimateFrom InitAnimateFrom
        {
            get { return (InitAnimateFrom)GetValue(InitAnimateFromProperty); }
            set { SetValue(InitAnimateFromProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitAnimateFrom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitAnimateFromProperty =
            DependencyProperty.Register(nameof(InitAnimateFrom), typeof(InitAnimateFrom), typeof(AnimateProgress), new PropertyMetadata(InitAnimateFrom.None));

        public double CustomAnimateFrom
        {
            get { return (double)GetValue(CustomAnimateFromProperty); }
            set { SetValue(CustomAnimateFromProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CustomAnimateFrom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomAnimateFromProperty =
            DependencyProperty.Register(nameof(CustomAnimateFrom), typeof(double), typeof(AnimateProgress), new PropertyMetadata(0.0));

        public TimeSpan AnimateDuration
        {
            get { return (TimeSpan)GetValue(AnimateDurationProperty); }
            set { SetValue(AnimateDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimateDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimateDurationProperty =
            DependencyProperty.Register(nameof(AnimateDuration), typeof(TimeSpan), typeof(AnimateProgress), new PropertyMetadata(TimeSpan.FromSeconds(1)));

        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EasingFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register(nameof(EasingFunction), typeof(IEasingFunction), typeof(AnimateProgress), new PropertyMetadata(new CircleEase { EasingMode = EasingMode.EaseOut }));

        protected override void OnValueChanged(double oldValue, double newValue) => DoAnimation(oldValue, newValue);

        private void DoAnimation(double fromValue, double toValue)
        {
            _fromValue = fromValue;
            _toValue = toValue;
            animateStartTime = stopwatch.Elapsed;
            CompositionTarget.Rendering += OnRendering;
        }

        private double _fromValue, _toValue;
        private double _trackWidth;
        private TimeSpan animateStartTime;
        private static Stopwatch stopwatch = Stopwatch.StartNew();

        private void OnRendering(object sender, EventArgs e)
        {
            TimeSpan during = stopwatch.Elapsed - animateStartTime;
            if (during > AnimateDuration)
            {
                SetIndicator(_toValue);
                CompositionTarget.Rendering -= OnRendering;
                return;
            }
            double rate = EasingFunction.Ease(during.TotalSeconds / AnimateDuration.TotalSeconds);
            SetIndicator(_fromValue + rate * (_toValue - _fromValue));
        }

        private double InRange(double value) => value > Maximum ? Maximum : value < Minimum ? Minimum : value;

        public override void OnApplyTemplate()
        {
            if (PART_Track != null) PART_Track.SizeChanged -= OnTrackSizeChanged;
            PART_Indicator = GetTemplateChild(nameof(PART_Indicator)) as FrameworkElement;
            PART_Track = GetTemplateChild(nameof(PART_Track)) as FrameworkElement;
            if (PART_Track != null) PART_Track.SizeChanged += OnTrackSizeChanged;
            switch (InitAnimateFrom)
            {
                case InitAnimateFrom.None:
                    CompositionTarget.Rendering -= OnRendering;
                    SetIndicator(Value);
                    break;
                case InitAnimateFrom.Minimum:
                    DoAnimation(Minimum, Value);
                    break;
                case InitAnimateFrom.Maximum:
                    DoAnimation(Maximum, Value);
                    break;
                case InitAnimateFrom.Custom:
                    DoAnimation(CustomAnimateFrom, Value);
                    break;
            }
        }

        private void OnTrackSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _trackWidth = PART_Track.ActualWidth;
            SetIndicator(Value);
        }

        private void SetIndicator(double value)
        {
            if (PART_Track != null && PART_Indicator != null)
            {
                double rate = Maximum <= Minimum ? 0 : ((InRange(value) - Minimum) / (Maximum - Minimum));
                PART_Indicator.Width = _trackWidth * rate;
            }
        }
    }
    public enum InitAnimateFrom { None, Minimum, Maximum, Custom }
}
