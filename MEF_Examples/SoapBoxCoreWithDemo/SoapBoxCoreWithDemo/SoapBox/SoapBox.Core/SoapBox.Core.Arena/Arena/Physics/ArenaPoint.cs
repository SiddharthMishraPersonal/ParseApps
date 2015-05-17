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
using System.ComponentModel;

namespace SoapBox.Core.Arena
{
    /// <summary>
    /// Defines a ViewModel for a two dimensional point
    /// to represent things like position in the arena.
    /// </summary>
    public sealed class ArenaPoint : AbstractViewModel
    {
        public ArenaPoint()
        {
        }

        public ArenaPoint(float x, float y)
        {
            m_X = x;
            m_Y = y;
        }

        #region "X"
        public float X
        {
            get
            {
                return m_X;
            }
            set
            {
                if (m_X != value)
                {
                    m_X = value;
                    NotifyPropertyChanged(m_XArgs);
                }
            }
        }
        private float m_X = 0.0f;
        static readonly PropertyChangedEventArgs m_XArgs =
            NotifyPropertyChangedHelper.CreateArgs<ArenaPoint>(o => o.X);
        #endregion
        #region "Y"
        public float Y
        {
            get
            {
                return m_Y;
            }
            set
            {
                if (m_Y != value)
                {
                    m_Y = value;
                    NotifyPropertyChanged(m_YArgs);
                }
            }
        }
        private float m_Y = 0.0f;
        static readonly PropertyChangedEventArgs m_YArgs =
            NotifyPropertyChangedHelper.CreateArgs<ArenaPoint>(o => o.Y);
        #endregion
    }
}
