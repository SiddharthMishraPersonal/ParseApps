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
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;

namespace SoapBox.Core
{
    public abstract class AbstractLayoutItem : AbstractExtension, ILayoutItem
    {
        #region " ILayoutItem Implementation "

        #region " Name "
        /// <summary>
        /// Used to uniquely identify the layout item.
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                if (m_Name != value)
                {
                    m_Name = value;
                    NotifyPropertyChanged(m_NameArgs);
                }
            }
        }
        private string m_Name = string.Empty;
        static readonly PropertyChangedEventArgs m_NameArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractLayoutItem>(o => o.Name);

        #endregion

        #region " Title "
        /// <summary>
        /// Shows up as a title of the layout item.
        /// </summary>
        public string Title
        {
            get
            {
                return m_Title;
            }
            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                if (m_Title != value)
                {
                    m_Title = value;
                    NotifyPropertyChanged(m_TitleArgs);
                }
            }
        }
        private string m_Title = string.Empty;
        static readonly PropertyChangedEventArgs m_TitleArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractLayoutItem>(o => o.Title);

        #endregion

        public virtual void OnGotFocus(object sender, RoutedEventArgs e) { }
        public virtual void OnLostFocus(object sender, RoutedEventArgs e) { }

        #endregion

    }
}
