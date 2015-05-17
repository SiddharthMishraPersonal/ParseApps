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
using SoapBox.Core.Arena;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using System.Media;

namespace SoapBox.Demo.PinBall
{
    public class PinBallLauncher
    {
        public const float LAUNCHER_WIDTH = PinBall.PIN_BALL_RADIUS * 2f + 2f;
        public const float PLUNGER_WIDTH = PinBall.PIN_BALL_RADIUS;
        public const float STROKE_LENGTH = 50f;

        public IEnumerable<IArenaBody> Bodies
        {
            get
            {
                return m_Bodies;
            }
        }
        private Collection<IArenaBody> m_Bodies = new Collection<IArenaBody>();

        public PinBallLauncher(PinBallTable table)
        {
            m_Bodies.Add(new PinBallLauncherCasing(table));
            m_plunger = new PinBallLauncherPlunger(table);
            m_Bodies.Add(m_plunger);
        }

        private PinBallLauncherPlunger m_plunger = null;

        public bool PlungerPulled
        {
            get
            {
                return m_plunger.IsPulled;
            }
            set
            {
                m_plunger.IsPulled = value;
            }
        }
    }

}
