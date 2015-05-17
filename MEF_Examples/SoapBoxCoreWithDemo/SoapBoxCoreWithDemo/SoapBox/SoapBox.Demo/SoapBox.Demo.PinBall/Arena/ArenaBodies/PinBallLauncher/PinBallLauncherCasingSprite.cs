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

    public class PinBallLauncherCasingSprite : AbstractSprite
    {
        public const float WALL_THICKNESS = 5f;
        public const float BALL_DEPRESSION_DEPTH = 10f;

        public PinBallLauncherCasingSprite(PinBallTable table)
        {
            float topCasingWidth = (PinBallLauncher.LAUNCHER_WIDTH-PinBallLauncher.PLUNGER_WIDTH)/2f;

            PathFigure pf = new PathFigure();
            pf.StartPoint = new Point(table.DimensionX / 2f, -table.DimensionY / 2f -1f);
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X - topCasingWidth, pf.StartPoint.Y - BALL_DEPRESSION_DEPTH), true));
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X - topCasingWidth, pf.StartPoint.Y - PinBallLauncher.STROKE_LENGTH), true));
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X - WALL_THICKNESS, pf.StartPoint.Y - PinBallLauncher.STROKE_LENGTH), true));
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X - WALL_THICKNESS, pf.StartPoint.Y - (PinBallLauncher.STROKE_LENGTH * 2f)), true));
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X - PinBallLauncher.LAUNCHER_WIDTH + WALL_THICKNESS, pf.StartPoint.Y - (PinBallLauncher.STROKE_LENGTH * 2f)), true));
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X - PinBallLauncher.LAUNCHER_WIDTH + WALL_THICKNESS, pf.StartPoint.Y - PinBallLauncher.STROKE_LENGTH), true));
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X - PinBallLauncher.LAUNCHER_WIDTH + topCasingWidth, pf.StartPoint.Y - PinBallLauncher.STROKE_LENGTH), true));
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X - PinBallLauncher.LAUNCHER_WIDTH + topCasingWidth, pf.StartPoint.Y - BALL_DEPRESSION_DEPTH), true));
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X - PinBallLauncher.LAUNCHER_WIDTH, pf.StartPoint.Y), true));
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X - PinBallLauncher.LAUNCHER_WIDTH, pf.StartPoint.Y - (PinBallLauncher.STROKE_LENGTH * 2f) - WALL_THICKNESS), true));
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X, pf.StartPoint.Y - (PinBallLauncher.STROKE_LENGTH * 2f) - WALL_THICKNESS),true));
            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            Geometry = pg;
        }
    }

}
