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
    public class PinBallStop : AbstractArenaPivotingBody
    {
        public const float WIDTH = PinBall.PIN_BALL_RADIUS * 2.25f;
        public const float HEIGHT = PinBall.PIN_BALL_RADIUS;

        public PinBallStop(float x, float y, float angle)
        {
            InitialX = x;
            InitialY = y;
            InitialAngle = angle;

            Mass = 10.0f;
            Friction = 0.2f;
            PivotFriction = 0.05f;
            PivotPoint.X = 0f;
            PivotPoint.Y = 0f;

            Sprite = new PinBallStopSprite();
        }

        #region "Release"
        public bool Release
        {
            get
            {
                return m_Release;
            }
            set
            {
                if (m_Release != value)
                {
                    m_Release = value;
                    NotifyPropertyChanged(m_ReleaseArgs);
                }
            }
        }
        private bool m_Release = false;
        static readonly PropertyChangedEventArgs m_ReleaseArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallStop>(o => o.Release);
        #endregion

        public override void OnUpdate()
        {
            if (Release)
            {
                Torque = 50000f;
            }
            else
            {
                Torque = -50000f;
            }
        }
    }
}
