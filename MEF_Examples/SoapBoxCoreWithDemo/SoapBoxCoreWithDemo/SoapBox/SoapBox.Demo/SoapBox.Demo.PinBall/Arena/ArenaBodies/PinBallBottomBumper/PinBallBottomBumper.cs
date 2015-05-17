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

namespace SoapBox.Demo.PinBall
{
    public enum PinBallBottomBumperSide
    {
        Right = 1,
        Left
    }

    public class PinBallBottomBumper : AbstractArenaStationaryBody
    {
        public const float BUMPER_STRENGTH = 10f;
        public const float CORNER_RADIUS = 15f;
        public const float X_DIMENSION = 80f; // x dimension between center of bottom and middle/top corners
        public const float Y_DIMENSION1 = 34f; // y dimension between center of bottom and middle corners
        public const float Y_DIMENSION2 = 140f; // y dimension between center of bottom and top corners

        public PinBallBottomBumper(PinBallTable table,
            PinBallBottomBumperSide side)
        {
            m_side = side;

            Friction = 0.9f;
            Restitution = 0.9f;

            // the left side is on the negative X side, right side is positive X
            float sideMultiplier = 1f;
            if (side == PinBallBottomBumperSide.Left)
            {
                sideMultiplier = -1f;
            }

            float effectiveTableWidth = table.DimensionX - PinBallLauncher.LAUNCHER_WIDTH - PinBallTableEdge.SIDE_WIDTH;

            // The bottom bumpers aren't centered due to the launcher guide.  
            // Calculate the offset
            float xOffset = -(table.DimensionX - effectiveTableWidth) / 2f;

            InitialX = sideMultiplier * 150f + xOffset;
            InitialY = -265f;

            Sprite = new PinBallBottomBumperSprite(table, side);

            // Calculate the position of the ball sensor
            float ballSensorX = InitialX + sideMultiplier * X_DIMENSION / 2f;
            float ballSensorY = InitialY + Y_DIMENSION2 / 2f;
            float ballSensorWidth = 0.65f * (float)Math.Sqrt(X_DIMENSION * X_DIMENSION + Y_DIMENSION2 * Y_DIMENSION2);
            float ballSensorHeight = PinBall.PIN_BALL_RADIUS * 2f + CORNER_RADIUS;
            float ballSensorAngle = (float)Math.Atan2(Y_DIMENSION2, sideMultiplier * X_DIMENSION);

            // Create the ball sensor (table has to add this)
            m_sensor = new PinBallRectangularSensor(table, ballSensorX, ballSensorY, ballSensorWidth, ballSensorHeight, ballSensorAngle);

            // Store the impulse vector (for hitting the ball)
            m_impulse = new ArenaVector(
                -sideMultiplier * BUMPER_STRENGTH * Y_DIMENSION2, // x
                BUMPER_STRENGTH * X_DIMENSION // y
                );
        }

        private PinBallBottomBumperSide m_side;
        private PinBallRectangularSensor m_sensor = null;
        private ArenaVector m_impulse = null;

        public PinBallRectangularSensor Sensor
        {
            get
            {
                return m_sensor;
            }
        }

        public override void OnCollision(IArenaBody otherBody)
        {
            base.OnCollision(otherBody);
            if (otherBody is PinBall)
            {
                // Use the pinball sensor to determine if the ball hit the 
                // "active" part of the bumper.
                if (m_sensor.Active)
                {
                    PinBall ball = (PinBall)otherBody;

                    ball.ApplyImpulse(m_impulse);

                    SoundManager.PlayWav(Resources.Sounds.flip);
                }
            }
        }

    }
}
