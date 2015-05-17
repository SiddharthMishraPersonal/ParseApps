#region "SoapBox.Core License"
/// <header module="SoapBox.Core"> 
/// Copyright (C) 2009 SoapBox Automation Inc., All Rights Reserved.
/// Contact: SoapBox Automation Licencing (license@soapboxautomation.com)
/// 
/// This file is part of SoapBox Core.
/// 
/// Commercial Usage
/// Licensees holding valid SoapBox Automation Commercial licenses may use  
/// this file in accordance with the SoapBox Automation Commercial License
/// Agreement provided with the Software or, alternatively, in accordance 
/// with the terms contained in a written agreement between you and
/// SoapBox Automation Inc.
/// 
/// GNU Lesser General Public License Usage
/// SoapBox Core is free software: you can redistribute it and/or modify 
/// it under the terms of the GNU Lesser General Public License
/// as published by the Free Software Foundation, either version 3 of the
/// License, or (at your option) any later version.
/// 
/// SoapBox Core is distributed in the hope that it will be useful, 
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Lesser General Public License for more details.
/// 
/// You should have received a copy of the GNU Lesser General Public License 
/// along with SoapBox Core. If not, see <http://www.gnu.org/licenses/>.
/// </header>
#endregion
        
using System;
using System.Collections.Generic;
using System.Windows;

namespace SoapBox.Core.Arena
{
    /// <summary>
    /// Inspired by http://stackoverflow.com/questions/1083224/pushing-read-only-gui-properties-back-into-viewmodel
    /// </summary>
    public class SizeObserver
    {
        #region " Observe "

        public static bool GetObserve(FrameworkElement elem)
        {
            return (bool)elem.GetValue(ObserveProperty);
        }

        public static void SetObserve(
          FrameworkElement elem, bool value)
        {
            elem.SetValue(ObserveProperty, value);
        }

        public static readonly DependencyProperty ObserveProperty =
            DependencyProperty.RegisterAttached("Observe", typeof(bool), typeof(SizeObserver),
            new UIPropertyMetadata(false, OnObserveChanged));

        static void OnObserveChanged(
          DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement elem = depObj as FrameworkElement;
            if (elem == null)
                return;

            if (e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
                elem.SizeChanged += OnSizeChanged;
            else
                elem.SizeChanged -= OnSizeChanged;
        }

        static void OnSizeChanged(object sender, RoutedEventArgs e)
        {
            if (!Object.ReferenceEquals(sender, e.OriginalSource))
                return;

            FrameworkElement elem = e.OriginalSource as FrameworkElement;
            if (elem != null)
            {
                SetObservedWidth(elem, elem.ActualWidth);
                SetObservedHeight(elem, elem.ActualHeight);
            }
        }

        #endregion

        #region " ObservedWidth "

        public static double GetObservedWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(ObservedWidthProperty);
        }

        public static void SetObservedWidth(DependencyObject obj, double value)
        {
            obj.SetValue(ObservedWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for ObservedWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObservedWidthProperty =
            DependencyProperty.RegisterAttached("ObservedWidth", typeof(double), typeof(SizeObserver), new UIPropertyMetadata(0.0));

        #endregion

        #region " ObservedHeight "

        public static double GetObservedHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(ObservedHeightProperty);
        }

        public static void SetObservedHeight(DependencyObject obj, double value)
        {
            obj.SetValue(ObservedHeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for ObservedHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObservedHeightProperty =
            DependencyProperty.RegisterAttached("ObservedHeight", typeof(double), typeof(SizeObserver), new UIPropertyMetadata(0.0));

        #endregion
    }
}
