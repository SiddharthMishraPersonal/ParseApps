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
using SoapBox.Core;
using System.ComponentModel;

namespace SoapBox.Demo.PinBall
{
    public class PinBallBonus : AbstractArenaDecorationBody 
    {
        public const float RADIUS = PinBall.PIN_BALL_RADIUS * 1.2f;

        public PinBallBonus(PinBallTable table, Point startingPoint)
        {
            InitialX = (float)startingPoint.X;
            InitialY = (float)startingPoint.Y;

            m_sensor = new PinBallCircularSensor(table, startingPoint, RADIUS);
            m_sensor.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(m_sensor_PropertyChanged);

            m_Sprite = new PinBallBonusSprite(RADIUS);
            Sprite = m_Sprite;
        }

        private PinBallBonusSprite m_Sprite = null;

        #region "IsActive"

        public bool IsActive
        {
            get
            {
                return m_Sprite.IsActive;
            }
            set
            {
                if (m_Sprite.IsActive != value)
                {
                    m_Sprite.IsActive = value;
                    NotifyPropertyChanged(m_IsActiveArgs);
                }
            }
        }
        static readonly PropertyChangedEventArgs m_IsActiveArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallBonus>(o => o.IsActive);

        void m_sensor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == m_ActiveName)
            {
                PinBallBonusSprite s = Sprite as PinBallBonusSprite;
                if (s != null)
                {
                    if (m_lastActive == false && m_sensor.Active == true)
                    {
                        // rising edge
                        s.IsActive = !s.IsActive; // toggle
                        if (s.IsActive)
                        {
                            SoundManager.PlayWav(Resources.Sounds.bonus_fast);
                        }
                        IsActive = s.IsActive;
                    }
                    m_lastActive = m_sensor.Active;
                }
            }
        }
        private bool m_lastActive = false;
        static readonly string m_ActiveName =
            NotifyPropertyChangedHelper.GetPropertyName<PinBallCircularSensor>(o => o.Active);

        #endregion

        private PinBallCircularSensor m_sensor = null;
    }
}
