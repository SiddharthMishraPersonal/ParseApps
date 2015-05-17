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

namespace SoapBox.Demo.PinBall
{
    public class PinBallReturnRampSprite : AbstractSprite
    {
        public PinBallReturnRampSprite(PinBallTable table)
        {
            PathFigure pf = new PathFigure();
            // start in the bottom left corner
            pf.StartPoint = new Point(-table.DimensionX / 2f, -table.DimensionY / 2f);
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X, pf.StartPoint.Y + PinBallReturnRamp.HEIGHT +0f), true));
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X + table.DimensionX - PinBallLauncher.LAUNCHER_WIDTH - 3f, pf.StartPoint.Y + 1f), true));
            pf.Segments.Add(new LineSegment(new Point(pf.StartPoint.X + table.DimensionX - PinBallLauncher.LAUNCHER_WIDTH, pf.StartPoint.Y + 0f), true));
            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            Geometry = pg;
        }
    }
}
