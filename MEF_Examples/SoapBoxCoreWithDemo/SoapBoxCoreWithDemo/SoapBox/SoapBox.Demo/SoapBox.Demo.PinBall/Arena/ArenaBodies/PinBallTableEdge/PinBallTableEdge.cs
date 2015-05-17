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
using System.Windows;
using System.Windows.Media;

namespace SoapBox.Demo.PinBall
{
    public enum PinBallTableSide
    {
        Top = 1,
        Right,
        Bottom,
        Left
    }

    public class PinBallTableEdge : AbstractArenaStationaryBody
    {
        public const int SIDE_WIDTH = 10;

        public PinBallTableEdge(PinBallTable table, 
            PinBallTableSide side)
        {
            Friction = PinBallTable.HARD_SURFACE_FRICTION;
            Restitution = 0.5f; // lower is a harder (less bouncy) surface
            ArenaPoint dimensions = new ArenaPoint();
            switch(side)
            {
                case PinBallTableSide.Top:
                    dimensions.X = table.DimensionX;
                    dimensions.Y = SIDE_WIDTH;
                    InitialY = (table.DimensionY + SIDE_WIDTH) / 2.0f;
                    break;
                case PinBallTableSide.Bottom:
                    dimensions.X = table.DimensionX - PinBallLauncher.LAUNCHER_WIDTH;
                    dimensions.Y = SIDE_WIDTH;
                    InitialY = -(table.DimensionY + SIDE_WIDTH) / 2.0f;
                    InitialX = -(PinBallLauncher.LAUNCHER_WIDTH / 2f);
                    break;
                case PinBallTableSide.Right:
                    dimensions.X = SIDE_WIDTH;
                    dimensions.Y = table.DimensionY + SIDE_WIDTH * 2.0f;
                    InitialX = (table.DimensionX + SIDE_WIDTH) / 2.0f;
                    break;
                case PinBallTableSide.Left:
                    dimensions.X = SIDE_WIDTH;
                    dimensions.Y = table.DimensionY + SIDE_WIDTH * 2.0f;
                    InitialX = -(table.DimensionX + SIDE_WIDTH) / 2.0f;
                    break;
            }
            Rect rect = new Rect(new Point(-dimensions.X / 2.0f, -dimensions.Y / 2.0f),
                new Point(dimensions.X / 2.0f, dimensions.Y / 2.0f));
            Sprite = new PinBallTableEdgeSprite(rect);
        }
    }
}
