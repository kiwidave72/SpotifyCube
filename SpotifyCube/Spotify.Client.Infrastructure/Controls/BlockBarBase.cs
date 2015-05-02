using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics.Contracts;

namespace Spotify.Client.Infrastructure.Controls
{
    public abstract class BlockBarBase : FrameworkElement
    {
        static BlockBarBase()
        {
            BlockBarBase.MinHeightProperty.OverrideMetadata(typeof(BlockBarBase), new FrameworkPropertyMetadata((double)10));
            BlockBarBase.MinWidthProperty.OverrideMetadata(typeof(BlockBarBase), new FrameworkPropertyMetadata((double)10));
            BlockBarBase.ClipToBoundsProperty.OverrideMetadata(typeof(BlockBarBase), new FrameworkPropertyMetadata(true));
        }

        #region properties

        public static readonly DependencyProperty BlockCountProperty =
            DependencyProperty.Register("BlockCount", typeof(int), typeof(BlockBarBase),
            new FrameworkPropertyMetadata((int)5, FrameworkPropertyMetadataOptions.AffectsRender,
                null,
                new CoerceValueCallback(CoerceBlockCount))
            );

        public int BlockCount
        {
            get { return (int)GetValue(BlockCountProperty); }
            set { SetValue(BlockCountProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(BlockBarBase),
            new FrameworkPropertyMetadata((double)0,
                FrameworkPropertyMetadataOptions.AffectsRender,
                null,
                new CoerceValueCallback(CoerceValue))
            );

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty BlockMarginProperty =
            DependencyProperty.Register("BlockMargin", typeof(double), typeof(BlockBarBase),
            new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsRender,
                null,
                new CoerceValueCallback(CoerceBlockMargin))
            );

        public double BlockMargin
        {
            get { return (double)GetValue(BlockMarginProperty); }
            set { SetValue(BlockMarginProperty, value); }
        }

        protected Pen BorderBen
        {
            get
            {
                if (m_borderPen == null || m_borderPen.Brush != Foreground)
                {
                    m_borderPen = new Pen(Foreground, 2);
                    m_borderPen.Freeze();
                }
                return m_borderPen;
            }
        }

        public static readonly DependencyProperty ForegroundProperty = Control.ForegroundProperty.AddOwner(typeof(BlockBarBase), new FrameworkPropertyMetadata(Brushes.Navy, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public static readonly DependencyProperty BackgroundProperty = Control.BackgroundProperty.AddOwner(typeof(BlockBarBase), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        #endregion

        protected static int GetThreshold(double value, int blockCount)
        {
            int blockNumber = Math.Min((int)(value * (blockCount + 1)), blockCount);

            Debug.Assert(blockNumber <= blockCount && blockNumber >= 0);

            return blockNumber;
        }

        private static object CoerceValue(DependencyObject element, object value)
        {
            double input = (double)value;
            if (input < 0 || double.IsNaN(input))
            {
                return 0.0;
            }
            else if (input > 1)
            {
                return input / 100.0;
            }
            else
            {
                return input;
            }
        }

        private static object CoerceBlockCount(DependencyObject element, object value)
        {
            int input = (int)value;

            if (input < 1)
            {
                return 1;
            }
            else
            {
                return input;
            }
        }

        private static object CoerceBlockMargin(DependencyObject element, object value)
        {
            double input = (double)value;
            if (input < 0 || double.IsNaN(input) || double.IsInfinity(input))
            {
                return 0;
            }
            else
            {
                return input;
            }
        }

        private Pen m_borderPen;
    }
}
