﻿#region "SoapBox.Core License"
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
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;

namespace SoapBox.Core
{
    public abstract class AbstractButton : AbstractCommandControl, IButton
    {
        public AbstractButton()
            : base()
        {
            CanExecuteChanged += delegate
            {
                NotifyPropertyChanged(m_IconArgs);
            };
        }

        #region " Text "
        /// <summary>
        /// This is the text displayed in the button itself.
        /// Best to set this property in the derived class's constructor.
        /// </summary>
        public string Text
        {
            get
            {
                return m_Text;
            }
            protected set
            {
                if (m_Text != value)
                {
                    m_Text = value;
                    NotifyPropertyChanged(m_TextArgs);
                }
            }
        }
        private string m_Text = null;
        static readonly PropertyChangedEventArgs m_TextArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractButton>(o => o.Text);
        #endregion

        #region " Icon "
        /// <summary>
        /// Optional icon that can be displayed in the button.
        /// </summary>
        public BitmapSource Icon
        {
            get
            {
                if (EnableCondition.Condition)
                {
                    return IconFull;
                }
                else
                {
                    return IconGray;
                }
            }
            protected set
            {
                if (IconFull != value)
                {
                    IconFull = value;
                    NotifyPropertyChanged(m_IconArgs);
                }
            }
        }
        static readonly PropertyChangedEventArgs m_IconArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractButton>(o => o.Icon);

        private BitmapSource IconFull
        {
            get
            {
                return m_IconFull;
            }
            set
            {
                if (m_IconFull != value)
                {
                    m_IconFull = value;
                    if (m_IconFull != null)
                    {
                        IconGray = ConvertFullToGray(m_IconFull);
                    }
                    else
                    {
                        IconGray = null;
                    }
                }
            }
        }
        private BitmapSource m_IconFull = null;
        private BitmapSource IconGray { get; set; }

        private BitmapSource ConvertFullToGray(BitmapSource full)
        {
            FormatConvertedBitmap gray = new FormatConvertedBitmap();

            gray.BeginInit();
            gray.Source = full;
            gray.DestinationFormat = PixelFormats.Gray32Float;
            gray.EndInit();

            return gray;
        }

        /// <summary>
        /// This is a helper function so you can assign the Icon directly
        /// from a Bitmap, such as one from a resources file.
        /// </summary>
        /// <param name="value"></param>
        protected void SetIconFromBitmap(System.Drawing.Bitmap value)
        {
            BitmapSource b = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                value.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            Icon = b;
        }

        #endregion

    }
}
