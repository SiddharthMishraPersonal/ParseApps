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
using System.Windows;
using System.Windows.Media;

namespace SoapBox.Demo.PinBall
{
    public class PinBallTargetPistonSprite : AbstractSprite
    {
        public PinBallTargetPistonSprite()
        {
            float delta = 0.1f;
            float outsideX = PinBallTarget.TARGET_WIDTH / 2f - delta;
            float insideX = outsideX - PinBallTarget.WALL_THICKNESS;
            float shaftX = PinBallTarget.WALL_THICKNESS - delta;
            float targetTopY = 4f * PinBallTarget.WALL_THICKNESS + 2f * PinBallTarget.MOVEMENT_DISTANCE;
            float targetBottomY = targetTopY - PinBallTarget.WALL_THICKNESS;
            float pistonTopY = 2f * PinBallTarget.WALL_THICKNESS + PinBallTarget.MOVEMENT_DISTANCE;
            float pistonBottomY = PinBallTarget.WALL_THICKNESS + PinBallTarget.MOVEMENT_DISTANCE;

            PathFigure pf = new PathFigure();
            pf.StartPoint = new Point(0, pistonBottomY);
            pf.Segments.Add(new LineSegment(new Point(insideX, pistonBottomY), true));
            pf.Segments.Add(new LineSegment(new Point(insideX, pistonTopY), true));
            pf.Segments.Add(new LineSegment(new Point(shaftX, pistonTopY), true));
            pf.Segments.Add(new LineSegment(new Point(shaftX, targetBottomY), true));
            pf.Segments.Add(new LineSegment(new Point(outsideX, targetBottomY), true));
            pf.Segments.Add(new LineSegment(new Point(outsideX, targetTopY), true));
            pf.Segments.Add(new LineSegment(new Point(-outsideX, targetTopY), true));
            pf.Segments.Add(new LineSegment(new Point(-outsideX, targetBottomY), true));
            pf.Segments.Add(new LineSegment(new Point(-shaftX, targetBottomY), true));
            pf.Segments.Add(new LineSegment(new Point(-shaftX, pistonTopY), true));
            pf.Segments.Add(new LineSegment(new Point(-insideX, pistonTopY), true));
            pf.Segments.Add(new LineSegment(new Point(-insideX, pistonBottomY), true));
            pf.Segments.Add(new LineSegment(new Point(0, pistonBottomY), true));
            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            Geometry = pg;
        }

        public float Width
        {
            get
            {
                return PinBallTarget.TARGET_WIDTH;
            }
        }

        public float XOffset
        {
            get
            {
                return -Width / 2f;
            }
        }

        public float Height
        {
            get
            {
                return PinBallTarget.WALL_THICKNESS;
            }
        }

        public float YOffset
        {
            get
            {
                return 3f * PinBallTarget.WALL_THICKNESS + 2f * PinBallTarget.MOVEMENT_DISTANCE;
            }
        }

    }
}
