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
    public class PinBallPartitionSprite : AbstractSprite 
    {
        public PinBallPartitionSprite(Point p1, Point p2, float width)
        {
            // length
            float length = (float)Math.Sqrt(Math.Pow(p2.X - p1.X,2) + Math.Pow(p2.Y - p1.Y,2)) + width;

            // center
            Point center = new Point((p1.X + p2.X) / 2f, (p1.Y + p2.Y) / 2f);

            // Calculate bottom right line x and y offset based on angle and corner radius
            float lineAngle = (float)Math.Atan2((p2.Y - p1.Y), (p2.X - p1.X));

            // upper left
            Point upperLeft = new Point(center.X - length / 2f, center.Y + width / 2f);
            Point bottomRight = new Point(center.X + length / 2f, center.Y - width / 2f);

            Rect r = new Rect(upperLeft, bottomRight);

            RectangleGeometry rg = new RectangleGeometry(r, width / 2f, width / 2f,
                new RotateTransform(-(lineAngle*180f/Math.PI),center.X, center.Y));

            Geometry = rg;
        }
    }
}
