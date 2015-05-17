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
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;

namespace SoapBox.Core.Arena
{
    public sealed class ArenaBodyState : AbstractViewModel
    {

        #region "Position"
        public ArenaPoint Position
        {
            get
            {
                return m_Position;
            }
        }
        private readonly ArenaPoint m_Position = new ArenaPoint();
        #endregion
        #region "Angle"
        public float Angle
        {
            get
            {
                return m_Angle;
            }
            internal set
            {
                if (m_Angle != value)
                {
                    m_Angle = value;
                    NotifyPropertyChanged(m_AngleArgs);
                }
            }
        }
        private float m_Angle;
        static readonly PropertyChangedEventArgs m_AngleArgs =
            NotifyPropertyChangedHelper.CreateArgs<ArenaBodyState>(o => o.Angle);
        #endregion
        #region "Velocity"
        public ArenaVector Velocity
        {
            get
            {
                return m_Velocity;
            }
        }
        private readonly ArenaVector m_Velocity = new ArenaVector();
        #endregion

        #region "ScreenX"
        public float ScreenX
        {
            get
            {
                return m_ScreenX;
            }
            internal set
            {
                if (m_ScreenX != value)
                {
                    m_ScreenX = value;
                    NotifyPropertyChanged(m_ScreenXArgs);
                }
            }
        }
        private float m_ScreenX;
        static readonly PropertyChangedEventArgs m_ScreenXArgs =
            NotifyPropertyChangedHelper.CreateArgs<ArenaBodyState>(o => o.ScreenX);
        #endregion
        #region "ScreenY"
        public float ScreenY
        {
            get
            {
                return m_ScreenY;
            }
            internal set
            {
                if (m_ScreenY != value)
                {
                    m_ScreenY = value;
                    NotifyPropertyChanged(m_ScreenYArgs);
                }
            }
        }
        private float m_ScreenY;
        static readonly PropertyChangedEventArgs m_ScreenYArgs =
            NotifyPropertyChangedHelper.CreateArgs<ArenaBodyState>(o => o.ScreenY);
        #endregion
        #region "ScreenAngle"
        public float ScreenAngle
        {
            get
            {
                return m_ScreenAngle;
            }
            internal set
            {
                if (m_ScreenAngle != value)
                {
                    m_ScreenAngle = value;
                    NotifyPropertyChanged(m_ScreenAngleArgs);
                }
            }
        }
        private float m_ScreenAngle;
        static readonly PropertyChangedEventArgs m_ScreenAngleArgs =
            NotifyPropertyChangedHelper.CreateArgs<ArenaBodyState>(o => o.ScreenAngle);
        #endregion
        #region "Scale"
        public float Scale
        {
            get
            {
                return m_Scale;
            }
            internal set
            {
                if (m_Scale != value)
                {
                    m_Scale = value;
                    NotifyPropertyChanged(m_ScaleArgs);
                }
            }
        }
        private float m_Scale;
        static readonly PropertyChangedEventArgs m_ScaleArgs =
            NotifyPropertyChangedHelper.CreateArgs<ArenaBodyState>(o => o.Scale);
        #endregion

    }
}
