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

namespace SoapBox.Demo.PinBall
{
    public class PinBall : AbstractArenaDynamicBody
    {
        public const float PIN_BALL_RADIUS = 20.0f;

        public PinBall(PinBallTable table, Point startingPoint)
        {
            Mass = 1.8f;
            Friction = 0.0001f;
            Restitution = 0.5f;

            m_table = table;
            InitialX = (float)startingPoint.X;
            InitialY = (float)startingPoint.Y;

            Sprite = new PinBallSprite();
        }

        private PinBallTable m_table = null;

        private readonly ArenaVector m_impulseSum = new ArenaVector();

        public void ApplyImpulse(ArenaVector impulse)
        {
            if (impulse == null)
            {
                throw new ArgumentOutOfRangeException("impulse");
            }
            m_impulseSum.X += impulse.X;
            m_impulseSum.Y += impulse.Y;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Update(this, new EventArgs());
            Impulse.X = m_impulseSum.X;
            Impulse.Y = m_impulseSum.Y;
            m_impulseSum.X = 0;
            m_impulseSum.Y = 0;
        }

        public event EventHandler Update;

    }

}
