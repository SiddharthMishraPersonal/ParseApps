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
using System.ComponentModel;
using SoapBox.Core;

namespace SoapBox.Demo.PinBall
{
    public class PinBallTargetPiston : AbstractArenaDynamicBody
    {
        const float FORCE = 50000f;

        public PinBallTargetPiston(float x, float y, float angle, bool latched)
        {
            m_latched = latched;

            Mass = 50f;
            IgnoresGravity = true;
            Friction = 0.1f;
            Restitution = 0.9f;
            InitialX = x;
            InitialY = y;
            InitialAngle = angle;
            Sprite = new PinBallTargetPistonSprite();
        }

        private bool m_latched = false;

        public override void OnCollision(IArenaBody otherBody)
        {
            base.OnCollision(otherBody);
            if (otherBody is PinBall)
            {
                if (!m_latched)
                {
                    Hit = !Hit;
                }
                else
                {
                    Hit = true;
               }
            }
        }

        
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!Hit)
            {
                // Want the force to be applied *out of* the casing
                // Angle 0 means the target is pointing straight up
                this.Force.X = (float)(-Math.Sin(InitialAngle) * FORCE);
                this.Force.Y = (float)(Math.Cos(InitialAngle) * FORCE);
            }
            else
            {
                this.Force.X = (float)(Math.Sin(InitialAngle) * FORCE);
                this.Force.Y = (float)(-Math.Cos(InitialAngle) * FORCE);
            }
        }

        #region "Hit"
        public bool Hit
        {
            get
            {
                return m_Hit;
            }
            set
            {
                if (m_Hit != value)
                {
                    m_Hit = value;
                    NotifyPropertyChanged(m_HitArgs);
                }
            }
        }
        private bool m_Hit = false;
        static readonly PropertyChangedEventArgs m_HitArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallTargetPiston>(o => o.Hit);
        #endregion
    }
}
