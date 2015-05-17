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
using System.Windows;
using SoapBox.Core;

namespace SoapBox.Demo.PinBall
{
    public class PinBallLevelTrigger : AbstractArenaDecorationBody
    {
        public const float WIDTH = PinBall.PIN_BALL_RADIUS * 2f * 1.2f;
        public const float HEIGHT = PinBall.PIN_BALL_RADIUS * 4f;

        public PinBallLevelTrigger(PinBallTable table, Point startingPoint)
        {
            m_table = table;

            InitialX = (float)startingPoint.X;
            InitialY = (float)startingPoint.Y;

            m_sensor = new PinBallRectangularSensor(table, InitialX, InitialY, WIDTH, HEIGHT, 0f);
            m_sensor.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(m_sensor_PropertyChanged);

            Sprite = new PinBallLevelTriggerSprite();
        }

        private PinBallTable m_table = null;
        private PinBallRectangularSensor m_sensor = null;

        void m_sensor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == m_ActiveName)
            {
                if (m_lastActive == false && m_sensor.Active == true)
                {
                    // rising edge
                    var evt = BallDetected;
                    if (evt != null)
                    {
                        evt(this, new EventArgs());
                    }
                }
                m_lastActive = m_sensor.Active;
            }
        }
        private bool m_lastActive = false;
        static readonly string m_ActiveName =
            NotifyPropertyChangedHelper.GetPropertyName<PinBallRectangularSensor>(o => o.Active);

        public event EventHandler BallDetected;
    }
}
