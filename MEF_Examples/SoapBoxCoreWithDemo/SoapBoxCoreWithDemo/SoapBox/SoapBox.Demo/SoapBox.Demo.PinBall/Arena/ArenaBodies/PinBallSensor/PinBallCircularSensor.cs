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
using System.Windows.Media;
using System.Windows;
using System.Collections.ObjectModel;
using SoapBox.Core;
using System.ComponentModel;

namespace SoapBox.Demo.PinBall
{
    public class PinBallCircularSensor : AbstractViewModel
    {
        #region "SensorRadius"
        public float SensorRadius
        {
            get
            {
                return m_SensorRadius;
            }
        }
        private readonly float m_SensorRadius = PinBall.PIN_BALL_RADIUS;
        #endregion

        public PinBallCircularSensor(PinBallTable table, Point startingPoint, float sensorRadius)
        {
            m_table = table;
            m_StartingPoint = startingPoint;
            m_SensorRadius = sensorRadius;
            foreach (PinBall ball in m_table.PinBalls)
            {
                ball.Update += OnUpdate;
            }
        }

        private PinBallTable m_table = null;
        private Point m_StartingPoint;

        private void OnUpdate(object sender, EventArgs e)
        {
            bool sensed = false;
            foreach (PinBall ball in m_table.PinBalls)
            {
                float distance = Convert.ToSingle(Math.Sqrt(Math.Pow((ball.State.Position.X - m_StartingPoint.X), 2) +
                    Math.Pow((ball.State.Position.Y - m_StartingPoint.Y), 2)));
                if (distance < (PinBall.PIN_BALL_RADIUS + SensorRadius))
                {
                    sensed = true;
                }
            }
            Active = sensed;
        }

        #region "Active"
        public bool Active
        {
            get
            {
                return m_Active;
            }
            set
            {
                if (m_Active != value)
                {
                    m_Active = value;
                    NotifyPropertyChanged(m_ActiveArgs);
                }
            }
        }
        private bool m_Active = false;
        static readonly PropertyChangedEventArgs m_ActiveArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallCircularSensor>(o => o.Active);

        #endregion

    }
}
