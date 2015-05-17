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
    public enum PinBallBottomRampSide
    {
        Right = 1,
        Left
    }

    public class PinBallBottomRamp : AbstractArenaStationaryBody
    {
        public const float RAMP_HEIGHT = 150f;
        public const float MIDDLE_GAP = PinBall.PIN_BALL_RADIUS * 4f;
        public const float BOTTOM_POSITION = -PinBallTable.TABLE_DIMENSION_Y/2f + PinBallReturnRamp.HEIGHT + PinBall.PIN_BALL_RADIUS * 2f;

        public PinBallBottomRamp(PinBallTable table,
            PinBallBottomRampSide side)
        {
            Friction = PinBallTable.HARD_SURFACE_FRICTION;
            Restitution = PinBallTable.HARD_SURFACE_RESTITUTION; // lower is a harder (less bouncy) surface

            Sprite = new PinBallBottomRampSprite(table, side);
        }
    }
}
