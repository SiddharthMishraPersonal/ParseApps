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
using System.Threading;

namespace SoapBox.Demo.PinBall
{
    public class PinBallKicker : AbstractArenaStationaryBody
    {
        const float KICKER_STRENGTH = 30f;

        public PinBallKicker(float radius, float x, float y)
        {
            Friction = 0.9f;
            Restitution = 0.9f;

            InitialX = x;
            InitialY = y;

            Sprite = new PinBallKickerSprite(radius);
        }

        public override void OnCollision(IArenaBody otherBody)
        {
            base.OnCollision(otherBody);
            if (otherBody is PinBall)
            {
                PinBall ball = (PinBall)otherBody;
                ball.ApplyImpulse(new ArenaVector(
                    KICKER_STRENGTH * (ball.State.Position.X - State.Position.X),
                    KICKER_STRENGTH * (ball.State.Position.Y - State.Position.Y)));
                NotifyScore();
                SoundManager.PlayWav(Resources.Sounds.tick);
            }
        }

        private void NotifyScore()
        {
            var evt = Score;
            if (evt != null)
            {
                evt(this, new EventArgs());
            }
        }
        public event EventHandler Score;

    }
}
