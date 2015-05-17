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
using System.Windows.Media;
using System.Windows;

namespace SoapBox.Demo.PinBall
{
    public class PinBallBottomRampSprite : AbstractSprite
    {
        public PinBallBottomRampSprite(PinBallTable table,
            PinBallBottomRampSide side)
        {
            // the left side is on the negative X side, right side is positive X
            float sideMultiplier = 1f;
            if (side == PinBallBottomRampSide.Left)
            {
                sideMultiplier = -1f;
            }

            float effectiveTableWidth = table.DimensionX - PinBallLauncher.LAUNCHER_WIDTH - PinBallTableEdge.SIDE_WIDTH;

            // The bottom ramps aren't centered due to the launcher guide.  
            // Calculate the offset
            float xOffset = -(table.DimensionX - effectiveTableWidth) / 2f;

            PathFigure pf = new PathFigure();
            pf.StartPoint = new Point(
                sideMultiplier * (PinBallBottomRamp.MIDDLE_GAP / 2f) + xOffset, // x
                PinBallBottomRamp.BOTTOM_POSITION); // y

            pf.Segments.Add(new LineSegment(new Point(
                sideMultiplier * (effectiveTableWidth / 2f) + xOffset, // x
                PinBallBottomRamp.BOTTOM_POSITION // y
                ), true));

            pf.Segments.Add(new LineSegment(new Point(
                sideMultiplier * (effectiveTableWidth / 2f) + xOffset , // x
                PinBallBottomRamp.BOTTOM_POSITION + PinBallBottomRamp.RAMP_HEIGHT // y
                ), true));

            pf.Segments.Add(new LineSegment(new Point(
                sideMultiplier * (PinBallBottomRamp.MIDDLE_GAP / 2f) + xOffset, // x
                PinBallBottomRamp.BOTTOM_POSITION)
                , true));

            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            Geometry = pg;
        }
    }
}
