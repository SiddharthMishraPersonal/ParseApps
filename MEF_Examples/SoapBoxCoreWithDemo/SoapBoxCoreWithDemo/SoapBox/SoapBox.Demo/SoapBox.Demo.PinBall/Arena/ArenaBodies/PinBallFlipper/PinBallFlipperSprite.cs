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
using System.Windows.Media;
using System.Windows;
using System.Collections.ObjectModel;
using System.Media;

namespace SoapBox.Demo.PinBall
{
    class PinBallFlipperSprite : AbstractSprite
    {

        public PinBallFlipperSprite()
        {

            GeometryGroup sprite = new GeometryGroup();
            PathFigure pf = new PathFigure();
            pf.StartPoint = new Point(-(PinBallFlipper.WIDE_WIDTH / 2.0f), -(PinBallFlipper.CENTER_TO_CENTER / 2.0f));
            pf.Segments.Add(new ArcSegment(new Point((PinBallFlipper.WIDE_WIDTH / 2.0f), -(PinBallFlipper.CENTER_TO_CENTER / 2.0f)),
                new Size(PinBallFlipper.WIDE_WIDTH / 2.0f, PinBallFlipper.WIDE_WIDTH / 2.0f), 0, false, SweepDirection.Clockwise, true));
            pf.Segments.Add(new LineSegment(new Point((PinBallFlipper.NARROW_WIDTH / 2.0f), (PinBallFlipper.CENTER_TO_CENTER / 2.0f)), true));
            pf.Segments.Add(new ArcSegment(new Point(-(PinBallFlipper.NARROW_WIDTH / 2.0f), (PinBallFlipper.CENTER_TO_CENTER / 2.0f)),
                new Size(PinBallFlipper.NARROW_WIDTH / 2.0f, PinBallFlipper.NARROW_WIDTH / 2.0f), 0, false, SweepDirection.Clockwise, true));
            pf.Segments.Add(new LineSegment(new Point(-(PinBallFlipper.WIDE_WIDTH / 2.0f), -(PinBallFlipper.CENTER_TO_CENTER / 2.0f)), true));
            pf.IsClosed = true;
            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            pg.FillRule = FillRule.Nonzero;
            sprite.Children.Add(pg);
            sprite.FillRule = FillRule.Nonzero;

            Geometry = sprite;
        }
    }
}
