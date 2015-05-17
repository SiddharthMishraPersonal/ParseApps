#region "SoapBox.Demo License"
/// <header module="SoapBox.Demo"> 
/// Copyright (C) 2009 SoapBox Automation Inc., All Rights Reserved.
/// Contact: SoapBox Automation Licencing (license@soapboxautomation.com)
/// 
/// This file is part of SoapBox Demo.
/// 
/// GNU Lesser General Public License Usage
/// SoapBox Demo is free software: you can redistribute it and/or modify 
/// it under the terms of the GNU Lesser General Public License
/// as published by the Free Software Foundation, either version 3 of the
/// License, or (at your option) any later version.
/// 
/// SoapBox Demo is distributed in the hope that it will be useful, 
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Lesser General Public License for more details.
/// 
/// You should have received a copy of the GNU Lesser General Public License 
/// along with SoapBox Demo. If not, see <http://www.gnu.org/licenses/>.
/// </header>
#endregion
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoapBox.Core.Arena;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;
using SoapBox.Core;

namespace SoapBox.Demo.PinBall
{
    public class PinBallLevelTriggerSprite : AbstractSprite
    {
        public PinBallLevelTriggerSprite()
        {
            ZIndex = -1;
            Geometry = new RectangleGeometry(
                new Rect(-PinBallLevelTrigger.WIDTH / 2f, -PinBallLevelTrigger.HEIGHT / 2f, PinBallLevelTrigger.WIDTH, PinBallLevelTrigger.HEIGHT), 3, 3);
        }

        #region "IsActive"
        public bool IsActive
        {
            get
            {
                return m_IsActive;
            }
            set
            {
                if (m_IsActive != value)
                {
                    m_IsActive = value;
                    NotifyPropertyChanged(m_IsActiveArgs);
                }
            }
        }
        private bool m_IsActive = false;
        static readonly PropertyChangedEventArgs m_IsActiveArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallLevelTriggerSprite>(o => o.IsActive);
        #endregion
    }
}
