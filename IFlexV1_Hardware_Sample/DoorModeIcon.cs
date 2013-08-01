using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using OpenOptions.dnaFusion.Flex.V1;

namespace IFlexV1_Hardware_Sample
{
    public class DoorModeIcon : ContentControl, INotifyPropertyChanged
    {
        public DoorModeIcon()
        {
            DefaultStyleKey = typeof(DoorModeIcon);
        }

        public static readonly DependencyProperty DoorModeProperty = DependencyProperty.Register(
            "DoorMode", typeof(DNADoorMode), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public DNADoorMode DoorMode
        {
            get { return (DNADoorMode)GetValue(DoorModeProperty); }
            set
            {
                SetValue(DoorModeProperty, value);
                OnPropertyChanged("DoorMode");
            }
        }

        private static void OnDoorModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DoorModeIcon source = d as DoorModeIcon;
            source.UpdateText();
        }

        private void UpdateText()
        {
            ControlTemplate template = null;

            switch (DoorMode)
            {
                case DNADoorMode.Disable:
                    template = DisabledContentTemplate;
                    break;
                case DNADoorMode.Unlocked:
                    template = UnlockedContentTemplate;
                    break;
                case DNADoorMode.Locked:
                    template = LockedContentTemplate;
                    break;
                case DNADoorMode.FacilityCodeOnly:
                    template = FacilityCodeOnlyContentTemplate;
                    break;
                case DNADoorMode.CardOnly:
                    template = CardOnlyContentTemplate;
                    break;
                case DNADoorMode.PinOnly:
                    template = PinOnlyContentTemplate;
                    break;
                case DNADoorMode.CardAndPinRequired:
                    template = CardAndPinRequiredContentTemplate;
                    break;
                case DNADoorMode.CardOrPinRequired:
                    template = CardOrPinRequiredContentTemplate;
                    break;
            }

            if ((int)DoorMode == DefaultMode - 1 && (int)DoorMode == OfflineMode - 1)
            {
                Foreground = DefaultAndOfflineModeForeground;
                ToolTipService.SetToolTip(this, "Default and Offline Reader Mode");
            }
            else
                if ((int)DoorMode == DefaultMode - 1)
                {
                    Foreground = DefaultModeForeground;
                    ToolTipService.SetToolTip(this, "Default Reader Mode");
                }
                else
                    if ((int)DoorMode == OfflineMode - 1)
                    {
                        Foreground = OfflineModeForeground;
                        ToolTipService.SetToolTip(this, "Offline Reader Mode");
                    }
                    else
                    {
                        Foreground = StandardForeground;
                        ToolTipService.SetToolTip(this, "");
                    }

            if (template != null)
                Template = template;
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, args);
            }
        }

        public static readonly DependencyProperty DisabledContentTemplateProperty = DependencyProperty.Register(
            "DisabledContentTemplate", typeof(ControlTemplate), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public ControlTemplate DisabledContentTemplate
        {
            get { return (ControlTemplate)GetValue(DisabledContentTemplateProperty); }
            set { SetValue(DisabledContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty UnlockedContentTemplateProperty = DependencyProperty.Register(
            "UnlockedContentTemplate", typeof(ControlTemplate), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public ControlTemplate UnlockedContentTemplate
        {
            get { return (ControlTemplate)GetValue(UnlockedContentTemplateProperty); }
            set { SetValue(UnlockedContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty LockedContentTemplateProperty = DependencyProperty.Register(
            "LockedContentTemplate", typeof(ControlTemplate), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public ControlTemplate LockedContentTemplate
        {
            get { return (ControlTemplate)GetValue(LockedContentTemplateProperty); }
            set { SetValue(LockedContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty FacilityCodeOnlyContentTemplateProperty = DependencyProperty.Register(
            "FacilityCodeOnlyContentTemplate", typeof(ControlTemplate), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public ControlTemplate FacilityCodeOnlyContentTemplate
        {
            get { return (ControlTemplate)GetValue(FacilityCodeOnlyContentTemplateProperty); }
            set { SetValue(FacilityCodeOnlyContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty CardOnlyContentTemplateProperty = DependencyProperty.Register(
            "CardOnlyContentTemplate", typeof(ControlTemplate), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public ControlTemplate CardOnlyContentTemplate
        {
            get { return (ControlTemplate)GetValue(CardOnlyContentTemplateProperty); }
            set { SetValue(CardOnlyContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty PinOnlyContentTemplateProperty = DependencyProperty.Register(
            "PinOnlyContentTemplate", typeof(ControlTemplate), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public ControlTemplate PinOnlyContentTemplate
        {
            get { return (ControlTemplate)GetValue(PinOnlyContentTemplateProperty); }
            set { SetValue(PinOnlyContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty CardAndPinRequiredContentTemplateProperty = DependencyProperty.Register(
            "CardAndPinRequiredContentTemplate", typeof(ControlTemplate), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public ControlTemplate CardAndPinRequiredContentTemplate
        {
            get { return (ControlTemplate)GetValue(CardAndPinRequiredContentTemplateProperty); }
            set { SetValue(CardAndPinRequiredContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty CardOrPinRequiredContentTemplateProperty = DependencyProperty.Register(
            "CardOrPinRequiredContentTemplate", typeof(ControlTemplate), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public ControlTemplate CardOrPinRequiredContentTemplate
        {
            get { return (ControlTemplate)GetValue(CardOrPinRequiredContentTemplateProperty); }
            set { SetValue(CardOrPinRequiredContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty StandardForegroundProperty = DependencyProperty.Register(
            "StandardForeground", typeof(Brush), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public Brush StandardForeground
        {
            get { return (Brush)GetValue(StandardForegroundProperty); }
            set { SetValue(StandardForegroundProperty, value); }
        }

        public static readonly DependencyProperty DefaultModeForegroundProperty = DependencyProperty.Register(
            "DefaultModeForeground", typeof(Brush), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public Brush DefaultModeForeground
        {
            get { return (Brush)GetValue(DefaultModeForegroundProperty); }
            set { SetValue(DefaultModeForegroundProperty, value); }
        }

        public static readonly DependencyProperty OfflineModeForegroundProperty = DependencyProperty.Register(
            "OfflineModeForeground", typeof(Brush), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public Brush OfflineModeForeground
        {
            get { return (Brush)GetValue(OfflineModeForegroundProperty); }
            set { SetValue(OfflineModeForegroundProperty, value); }
        }

        public static readonly DependencyProperty DefaultAndOfflineModeForegroundProperty = DependencyProperty.Register(
            "DefaultAndOfflineModeForeground", typeof(Brush), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public Brush DefaultAndOfflineModeForeground
        {
            get { return (Brush)GetValue(DefaultAndOfflineModeForegroundProperty); }
            set { SetValue(DefaultAndOfflineModeForegroundProperty, value); }
        }

        public static readonly DependencyProperty DefaultModeProperty = DependencyProperty.Register(
            "DefaultMode", typeof(int), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public int DefaultMode
        {
            get { return (int)GetValue(DefaultModeProperty); }
            set { SetValue(DefaultModeProperty, value); }
        }

        public static readonly DependencyProperty OfflineModeProperty = DependencyProperty.Register(
            "OfflineMode", typeof(int), typeof(DoorModeIcon), new PropertyMetadata(OnDoorModePropertyChanged));

        public int OfflineMode
        {
            get { return (int)GetValue(OfflineModeProperty); }
            set { SetValue(OfflineModeProperty, value); }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
