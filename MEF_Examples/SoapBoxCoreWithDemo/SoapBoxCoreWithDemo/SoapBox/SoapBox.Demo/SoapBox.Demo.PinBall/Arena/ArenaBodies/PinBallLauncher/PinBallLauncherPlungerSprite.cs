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

    public class PinBallLauncherPlungerSprite : AbstractSprite
    {
        public PinBallLauncherPlungerSprite(PinBallLauncherPlunger plunger)
        {
            float x1 = (PinBallLauncher.LAUNCHER_WIDTH - 2f*PinBallLauncherCasingSprite.WALL_THICKNESS) / 2f - 0.1f;
            float x2 = PinBallLauncher.PLUNGER_WIDTH / 2f - 0.1f;
            PathFigure pf = new PathFigure();
            pf.StartPoint = new Point(x1, -PinBallLauncherCasingSprite.WALL_THICKNESS*3f);
            pf.Segments.Add(new LineSegment(new Point(x1, 0),true));
            pf.Segments.Add(new LineSegment(new Point(x2, 0),true));
            pf.Segments.Add(new LineSegment(new Point(x2, PinBallLauncher.STROKE_LENGTH),true));
            pf.Segments.Add(new LineSegment(new Point(-x2, PinBallLauncher.STROKE_LENGTH),true));
            pf.Segments.Add(new LineSegment(new Point(-x2, 0),true));
            pf.Segments.Add(new LineSegment(new Point(-x1, 0),true));
            pf.Segments.Add(new LineSegment(new Point(-x1, -PinBallLauncherCasingSprite.WALL_THICKNESS*3f), true));
            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            Geometry = pg;
            m_plunger = plunger;
        }

        public PinBallLauncherPlunger Plunger
        {
            get
            {
                return m_plunger;
            }
        }
        private PinBallLauncherPlunger m_plunger = null;

    }
}
