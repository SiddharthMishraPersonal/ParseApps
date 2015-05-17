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
    public class PinBallRectangularSensor : AbstractViewModel
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

        public PinBallRectangularSensor(PinBallTable table, Point p1, Point p2)
        {
            m_table = table;
            m_width = (float)Math.Abs(p2.X - p1.X);
            m_height = (float)Math.Abs(p2.Y - p1.Y);
            m_angle = 0f;
            m_center = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            foreach (PinBall ball in m_table.PinBalls)
            {
                ball.Update += OnUpdate;
            }
        }

        public PinBallRectangularSensor(PinBallTable table, float x, float y, float width, float height, float angle)
        {
            m_table = table;
            m_width = width;
            m_height = height;
            m_angle = angle;
            m_center = new Point(x, y);
            foreach (PinBall ball in m_table.PinBalls)
            {
                ball.Update += OnUpdate;
            }
        }

        private float m_width;
        private float m_height;
        private float m_angle;
        private Point m_center;
        private PinBallTable m_table = null;

        private void OnUpdate(object sender, EventArgs e)
        {
            bool sensed = false;
            foreach (PinBall ball in m_table.PinBalls)
            {
                // Have to account for rotation.  Rather than rotating the rectangle,
                // just rotate the ball position about the center of the rectangle, 
                // but in the opposite direction.
                double ballOffsetX = ball.State.Position.X - m_center.X;
                double ballOffsetY = ball.State.Position.Y - m_center.Y;
                double ballDistance = Math.Sqrt(Math.Pow(ballOffsetX, 2) + Math.Pow(ballOffsetY, 2));
                double ballAngle = Math.Atan2(ballOffsetY, ballOffsetX);
                double newBallAngle = ballAngle - m_angle;
                double newBallOffsetX = Math.Cos(newBallAngle) * ballDistance;
                double newBallOffsetY = Math.Sin(newBallAngle) * ballDistance;

                if (newBallOffsetX >= (-m_width / 2f - PinBall.PIN_BALL_RADIUS)
                    && newBallOffsetX <= (m_width / 2f + PinBall.PIN_BALL_RADIUS)
                    && newBallOffsetY >= (-m_height / 2f - PinBall.PIN_BALL_RADIUS)
                    && newBallOffsetY <= (m_height / 2f + PinBall.PIN_BALL_RADIUS))
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
            NotifyPropertyChangedHelper.CreateArgs<PinBallRectangularSensor>(o => o.Active);
        #endregion

    }
}
