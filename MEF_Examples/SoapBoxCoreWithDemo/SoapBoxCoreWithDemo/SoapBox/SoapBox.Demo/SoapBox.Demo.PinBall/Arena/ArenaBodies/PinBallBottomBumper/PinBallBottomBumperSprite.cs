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
    public class PinBallBottomBumperSprite : AbstractSprite
    {
        float m_sideMultiplier = 1f;

        public PinBallBottomBumperSprite(PinBallTable table,
            PinBallBottomBumperSide side)
        {
            // the left side is on the negative X side, right side is positive X
            if (side == PinBallBottomBumperSide.Left)
            {
                m_sideMultiplier = -1f;
            }

            float effectiveTableWidth = table.DimensionX - PinBallLauncher.LAUNCHER_WIDTH - PinBallTableEdge.SIDE_WIDTH;

            // The bottom bumpers aren't centered due to the launcher guide.  
            // Calculate the offset
            float xOffset = -(table.DimensionX - effectiveTableWidth) / 2f;

            GeometryGroup sprite = new GeometryGroup();

            // Calculate bottom right line x and y offset based on angle and corner radius
            float bottomRightAngle = (float)Math.Atan2(PinBallBottomBumper.Y_DIMENSION1, PinBallBottomBumper.X_DIMENSION);
            float bottomRightLineOffsetX = PinBallBottomBumper.CORNER_RADIUS * (float)Math.Sin(bottomRightAngle);
            float bottomRightLineOffsetY = -PinBallBottomBumper.CORNER_RADIUS * (float)Math.Cos(bottomRightAngle);

            // Calculate upper left line x and y offset based on angle and corner radius
            float topLeftAngle = (float)Math.Atan2(PinBallBottomBumper.Y_DIMENSION2, PinBallBottomBumper.X_DIMENSION);
            float topLeftLineOffsetX = -PinBallBottomBumper.CORNER_RADIUS * (float)Math.Sin(topLeftAngle);
            float topLeftLineOffsetY = PinBallBottomBumper.CORNER_RADIUS * (float)Math.Cos(topLeftAngle);

            int pointCount = 6;
            Point[] linePoints = new Point[pointCount + 1];

            // just define the right side points and flip them for the left side below

            linePoints[0].X = bottomRightLineOffsetX;
            linePoints[0].Y = bottomRightLineOffsetY;

            linePoints[1].X = PinBallBottomBumper.X_DIMENSION + bottomRightLineOffsetX;
            linePoints[1].Y = PinBallBottomBumper.Y_DIMENSION1 + bottomRightLineOffsetY;

            linePoints[2].X = PinBallBottomBumper.X_DIMENSION + PinBallBottomBumper.CORNER_RADIUS;
            linePoints[2].Y = PinBallBottomBumper.Y_DIMENSION1;

            linePoints[3].X = PinBallBottomBumper.X_DIMENSION + PinBallBottomBumper.CORNER_RADIUS;
            linePoints[3].Y = PinBallBottomBumper.Y_DIMENSION2;

            linePoints[4].X = PinBallBottomBumper.X_DIMENSION + topLeftLineOffsetX;
            linePoints[4].Y = PinBallBottomBumper.Y_DIMENSION2 + topLeftLineOffsetY;

            linePoints[5].X = topLeftLineOffsetX;
            linePoints[5].Y = topLeftLineOffsetY;

            linePoints[6].X = linePoints[0].X; //makes the match work a bit better to have the last point equal to the first
            linePoints[6].Y = linePoints[0].Y;

            PathFigure pf = new PathFigure();

            for (int i = 0; i < pointCount; i++)
            {
                // have to do the left side points in the reverse order
                int index = 0;
                if (side == PinBallBottomBumperSide.Left)
                {
                    index = pointCount - i - 1;
                }
                else
                {
                    index = i;
                }

                if (i == 0)
                {
                    pf.StartPoint = new Point(
                        m_sideMultiplier * linePoints[index].X, // x
                        linePoints[index].Y); // y
                }
                else
                {
                    pf.Segments.Add(new LineSegment(new Point(
                        m_sideMultiplier * linePoints[index].X, // x
                        linePoints[index].Y), // y
                        true));
                }

                // Draw the arcs to connect the corners
                if (i % 2 == 1)
                {
                    int iNext = (i + 1) % pointCount;
                    int indexNext = 0;
                    if (side == PinBallBottomBumperSide.Left)
                    {
                        indexNext = pointCount - iNext - 1;
                    }
                    else
                    {
                        indexNext = iNext;
                    }

                    pf.Segments.Add(new ArcSegment(new Point(
                        m_sideMultiplier * linePoints[indexNext].X,
                        linePoints[indexNext].Y),
                        new Size(PinBallBottomBumper.CORNER_RADIUS, PinBallBottomBumper.CORNER_RADIUS),
                        0, false, SweepDirection.Clockwise, true));
                }
            }

            pf.IsClosed = true;
            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            pg.FillRule = FillRule.Nonzero;
            sprite.Children.Add(pg);

            sprite.FillRule = FillRule.Nonzero;

            Geometry = sprite;
        }

        /// <summary>
        /// For binding (height and width of ellipse)
        /// </summary>
        public float CornerDiameter
        {
            get
            {
                return 1.3f * PinBallBottomBumper.CORNER_RADIUS * 2f;
            }
        }

        /// <summary>
        /// For binding (to offset ellipse this much)
        /// </summary>
        public float NegCornerRadius
        {
            get
            {
                return -CornerDiameter / 2f;
            }
        }

        /// <summary>
        /// For binding (height and width of ellipse)
        /// </summary>
        public float CornerStudDiameter
        {
            get
            {
                return 0.4f * PinBallBottomBumper.CORNER_RADIUS * 2f;
            }
        }

        /// <summary>
        /// For binding (to offset ellipse this much)
        /// </summary>
        public float NegCornerStudRadius
        {
            get
            {
                return -CornerStudDiameter / 2f;
            }
        }

        public ArenaPoint Corner1
        {
            get
            {
                return new ArenaPoint(0, 0);
            }
        }

        public ArenaPoint Corner2
        {
            get
            {
                return new ArenaPoint(m_sideMultiplier * PinBallBottomBumper.X_DIMENSION, PinBallBottomBumper.Y_DIMENSION1);
            }
        }

        public ArenaPoint Corner3
        {
            get
            {
                return new ArenaPoint(m_sideMultiplier * PinBallBottomBumper.X_DIMENSION, PinBallBottomBumper.Y_DIMENSION2);
            }
        }

    }
}
