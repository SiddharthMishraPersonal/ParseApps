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

    public class PinBallLauncherPlunger : AbstractArenaDynamicBody
    {
        public PinBallLauncherPlunger(PinBallTable table)
        {
            m_table = table;
            Mass = 25f;
            Friction = 0.1f;
            Restitution = 0.9f;
            IgnoresGravity = true;
            InitialX = (table.DimensionX / 2f) - (PinBallLauncher.LAUNCHER_WIDTH / 2f);
            InitialY = -(table.DimensionY / 2f) - PinBallLauncher.STROKE_LENGTH - PinBallLauncherCasingSprite.BALL_DEPRESSION_DEPTH ;
            Sprite = new PinBallLauncherPlungerSprite(this);
        }

        private PinBallTable m_table;

        public override void OnUpdate()
        {
            Impulse.Y = 0;
            Force.Y = 0;
            if (IsPulled)
            {
                Force.Y = -11000f * Mass;
            }
            else
            {
                Force.Y = 11000f * Mass; 
            }
        }

        public bool IsPulled
        {
            get
            {
                return m_IsPulled;
            }
            set
            {
                m_IsPulled = value;
            }
        }
        private bool m_IsPulled = false;

        public override void OnCollision(IArenaBody otherBody)
        {
            base.OnCollision(otherBody);
            if (otherBody is PinBall)
            {
                // cheat and give the pinball some more inertia :)
                if (this.State.Velocity.Y > 20f)
                {
                    ((PinBall)otherBody).ApplyImpulse(new ArenaVector(0f, 2400f));
                    SoundManager.PlayWav(Resources.Sounds.doodoodoodoo);
                }
            }
        }
    }

}
