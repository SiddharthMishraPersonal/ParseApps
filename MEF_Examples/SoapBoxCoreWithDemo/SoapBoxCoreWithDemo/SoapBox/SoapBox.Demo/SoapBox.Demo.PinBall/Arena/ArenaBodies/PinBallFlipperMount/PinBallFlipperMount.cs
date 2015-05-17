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
    public enum PinBallFlipperMountSide
    {
        Right = 1,
        Left
    }

    public class PinBallFlipperMount : AbstractArenaStationaryBody
    {
        public PinBallFlipperMount(PinBallTable table,
            PinBallFlipperMountSide side)
        {
            Friction = PinBallTable.HARD_SURFACE_FRICTION;
            Restitution = PinBallTable.HARD_SURFACE_RESTITUTION; // lower is a harder (less bouncy) surface

            // the left side is on the negative X side, right side is positive X
            float sideMultiplier = 1f;
            if (side == PinBallFlipperMountSide.Left)
            {
                sideMultiplier = -1f;
            }

            float effectiveTableWidth = table.DimensionX - PinBallLauncher.LAUNCHER_WIDTH - PinBallTableEdge.SIDE_WIDTH;
            float xOffset = -(table.DimensionX - effectiveTableWidth) / 2f;

            InitialX = sideMultiplier * 230f + xOffset;
            InitialY = -320f;
            InitialAngle = sideMultiplier * Convert.ToSingle(Math.PI) * 1.1f / 8f;

            Sprite = new PinBallFlipperMountSprite(table, side, InitialAngle);
        }
    }
}
