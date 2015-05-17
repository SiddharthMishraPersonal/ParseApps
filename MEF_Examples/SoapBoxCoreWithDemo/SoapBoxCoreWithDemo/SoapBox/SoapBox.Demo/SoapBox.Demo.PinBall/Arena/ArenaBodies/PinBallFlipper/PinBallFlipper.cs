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
using System.Media;
using System.ComponentModel;
using SoapBox.Core;

namespace SoapBox.Demo.PinBall
{
    public class PinBallFlipper : AbstractArenaPivotingBody
    {
        public const float WIDE_WIDTH = 30.0f;
        public const float NARROW_WIDTH = 10.0f;
        public const float CENTER_TO_CENTER = 106.0f;

        public PinBallFlipper(float x, float y, float angle, float mass)
        {
            InitialX = x;
            InitialY = y;
            InitialAngle = angle;

            Mass = mass;
            Friction = PinBallTable.HARD_SURFACE_FRICTION;
            Restitution = PinBallTable.HARD_SURFACE_RESTITUTION;
            PivotFriction = 0.05f;
            PivotPoint.X = 0f;
            PivotPoint.Y = -(CENTER_TO_CENTER / 2.0f);

            Sprite = new PinBallFlipperSprite();
        }

        #region "ControlTorque"
        public float ControlTorque
        {
            get
            {
                return m_ControlTorque;
            }
            set
            {
                if (m_ControlTorque != value)
                {
                    m_ControlTorque = value;
                    NotifyPropertyChanged(m_ControlTorqueArgs);
                }
            }
        }
        private float m_ControlTorque = 0f;
        static readonly PropertyChangedEventArgs m_ControlTorqueArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallFlipper>(o => o.ControlTorque);
        #endregion

        public override void OnUpdate()
        {
            base.OnUpdate();
            //Torque = ControlTorque;
            Force.X = -(float)Math.Cos(State.Angle) * ControlTorque / (CENTER_TO_CENTER/2f);
            Force.Y = -(float)Math.Sin(State.Angle) * ControlTorque / (CENTER_TO_CENTER/2f);
        }
    }
}
