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
    public class PinBallTargetCasingSprite : AbstractSprite
    {
        public PinBallTargetCasingSprite()
        {
            ZIndex = 1;

            float outsideX = PinBallTarget.TARGET_WIDTH / 2f;
            float insideX = outsideX - PinBallTarget.WALL_THICKNESS;
            float topY = 3f * PinBallTarget.WALL_THICKNESS + PinBallTarget.MOVEMENT_DISTANCE;
            float insideTopY = topY - PinBallTarget.WALL_THICKNESS;
            float insideBottomY = PinBallTarget.WALL_THICKNESS;

            PathFigure pf = new PathFigure();
            pf.StartPoint = new Point(0, 0);
            pf.Segments.Add(new LineSegment(new Point(outsideX, 0), true));
            pf.Segments.Add(new LineSegment(new Point(outsideX, topY), true));
            pf.Segments.Add(new LineSegment(new Point(PinBallTarget.WALL_THICKNESS, topY), true));
            pf.Segments.Add(new LineSegment(new Point(PinBallTarget.WALL_THICKNESS, insideTopY), true));
            pf.Segments.Add(new LineSegment(new Point(insideX, insideTopY), true));
            pf.Segments.Add(new LineSegment(new Point(insideX, insideBottomY), true));
            pf.Segments.Add(new LineSegment(new Point(-insideX, insideBottomY), true));
            pf.Segments.Add(new LineSegment(new Point(-insideX, insideTopY), true));
            pf.Segments.Add(new LineSegment(new Point(-PinBallTarget.WALL_THICKNESS, insideTopY), true));
            pf.Segments.Add(new LineSegment(new Point(-PinBallTarget.WALL_THICKNESS, topY), true));
            pf.Segments.Add(new LineSegment(new Point(-outsideX, topY), true));
            pf.Segments.Add(new LineSegment(new Point(-outsideX, 0), true));
            pf.Segments.Add(new LineSegment(new Point(0, 0), true));
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
                return 3f * PinBallTarget.WALL_THICKNESS + 2f * PinBallTarget.MOVEMENT_DISTANCE;
            }
        }
    }
}
