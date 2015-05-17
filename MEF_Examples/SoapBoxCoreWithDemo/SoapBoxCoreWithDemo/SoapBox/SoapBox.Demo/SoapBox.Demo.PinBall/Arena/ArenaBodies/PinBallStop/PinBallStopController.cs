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
using System.ComponentModel;
using SoapBox.Core;

namespace SoapBox.Demo.PinBall
{
    class PinBallStopController
    {
        public PinBallStopController(
            PinBallTable table,
            PinBallStop pinBallStop,
            PinBallCircularSensor ballPresentSensor,
            PinBallRectangularSensor launcherOccupiedSensor,
            PinBallRectangularSensor ballInPlaySensor)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            if (pinBallStop == null)
            {
                throw new ArgumentNullException("pinBallStop");
            }
            if (ballPresentSensor == null)
            {
                throw new ArgumentNullException("ballPresentSensor");
            }
            if (launcherOccupiedSensor == null)
            {
                throw new ArgumentNullException("launcherOccupiedSensor");
            }
            if (ballInPlaySensor == null)
            {
                throw new ArgumentNullException("ballInPlaySensor");
            }
            m_table = table;
            m_pinBallStop = pinBallStop;
            m_ballPresentSensor = ballPresentSensor;
            m_launcherOccupiedSensor = launcherOccupiedSensor;
            m_ballInPlaySensor = ballInPlaySensor;

            ballPresentSensor.PropertyChanged += 
                new PropertyChangedEventHandler(sensors_PropertyChanged);
            launcherOccupiedSensor.PropertyChanged +=
                new PropertyChangedEventHandler(sensors_PropertyChanged);
            ballInPlaySensor.PropertyChanged +=
                new PropertyChangedEventHandler(sensors_PropertyChanged);

        }
        private PinBallTable m_table = null;
        private PinBallStop m_pinBallStop = null;
        private PinBallCircularSensor m_ballPresentSensor = null;
        private PinBallRectangularSensor m_launcherOccupiedSensor = null;
        private PinBallRectangularSensor m_ballInPlaySensor = null;

        void sensors_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((sender is PinBallRectangularSensor && e.PropertyName == m_RectangularSensor_ActiveName) &&
                (m_lastBallInPlayActive && !m_ballInPlaySensor.Active))
            {
                // falling edge of ball in play sensor, so decrement the ball count
                m_table.Balls--;
            }
            m_lastBallInPlayActive = m_ballInPlaySensor.Active;

            if ((sender is PinBallRectangularSensor && e.PropertyName == m_RectangularSensor_ActiveName) ||
                (sender is PinBallCircularSensor && e.PropertyName == m_CircularSensor_ActiveName) ||
                (sender is PinBallTable && e.PropertyName == m_IPinBallTable_BallsName))
            {
                m_pinBallStop.Release = 
                    m_ballPresentSensor.Active 
                    && !m_launcherOccupiedSensor.Active
                    && !m_ballInPlaySensor.Active
                    && (m_table.Balls > 0);
            }
        }
        private bool m_lastBallInPlayActive = false;
        static readonly string m_RectangularSensor_ActiveName =
            NotifyPropertyChangedHelper.GetPropertyName<PinBallRectangularSensor>(o => o.Active);
        static readonly string m_CircularSensor_ActiveName =
            NotifyPropertyChangedHelper.GetPropertyName<PinBallCircularSensor>(o => o.Active);
        static readonly string m_IPinBallTable_BallsName =
            NotifyPropertyChangedHelper.GetPropertyName<PinBallTable>(o => o.Balls);
    }
}
