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
using System.ComponentModel;
using SoapBox.Core;

namespace SoapBox.Demo.PinBall
{
    public class PinBallLevelDisplaySprite : AbstractSprite 
    {
        public PinBallLevelDisplaySprite()
        {
            ZIndex = -1;
            Geometry = new RectangleGeometry(
                new Rect(-PinBallLevelDisplay.WIDTH / 2f, -PinBallLevelDisplay.HEIGHT / 2f, PinBallLevelDisplay.WIDTH, PinBallLevelDisplay.HEIGHT), 3, 3);
        }

        #region "Level"
        public int Level
        {
            get
            {
                return m_Level;
            }
            set
            {
                if(value < 0 || value > 10)
                {
                    throw new ArgumentOutOfRangeException(m_LevelName);
                }
                if (m_Level != value)
                {
                    m_Level = value;
                    NotifyPropertyChanged(m_LevelArgs);
                    NotifyPropertyChanged(m_Level1Args);
                    NotifyPropertyChanged(m_Level2Args);
                    NotifyPropertyChanged(m_Level3Args);
                    NotifyPropertyChanged(m_Level4Args);
                    NotifyPropertyChanged(m_Level5Args);
                    NotifyPropertyChanged(m_Level6Args);
                    NotifyPropertyChanged(m_Level7Args);
                    NotifyPropertyChanged(m_Level8Args);
                    NotifyPropertyChanged(m_Level9Args);
                    NotifyPropertyChanged(m_Level10Args);
                }
            }
        }
        private int m_Level = 0;
        static readonly PropertyChangedEventArgs m_LevelArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallLevelDisplaySprite>(o => o.Level);
        static readonly string m_LevelName =
            NotifyPropertyChangedHelper.GetPropertyName<PinBallLevelDisplaySprite>(o => o.Level);

        #endregion

        // This is tedious and kind of dumb, and I probably should have used
        // a value converter for this, but hey, it's a demo.
        #region "Level1"
        public bool Level1
        {
            get
            {
                return m_Level >= 1;
            }
        }
        static readonly PropertyChangedEventArgs m_Level1Args =
            NotifyPropertyChangedHelper.CreateArgs<PinBallLevelDisplaySprite>(o => o.Level1);
        #endregion
        #region "Level2"
        public bool Level2
        {
            get
            {
                return m_Level >= 2;
            }
        }
        static readonly PropertyChangedEventArgs m_Level2Args =
            NotifyPropertyChangedHelper.CreateArgs<PinBallLevelDisplaySprite>(o => o.Level2);
        #endregion
        #region "Level3"
        public bool Level3
        {
            get
            {
                return m_Level >= 3;
            }
        }
        static readonly PropertyChangedEventArgs m_Level3Args =
            NotifyPropertyChangedHelper.CreateArgs<PinBallLevelDisplaySprite>(o => o.Level3);
        #endregion
        #region "Level4"
        public bool Level4
        {
            get
            {
                return m_Level >= 4;
            }
        }
        static readonly PropertyChangedEventArgs m_Level4Args =
            NotifyPropertyChangedHelper.CreateArgs<PinBallLevelDisplaySprite>(o => o.Level4);
        #endregion
        #region "Level5"
        public bool Level5
        {
            get
            {
                return m_Level >= 5;
            }
        }
        static readonly PropertyChangedEventArgs m_Level5Args =
            NotifyPropertyChangedHelper.CreateArgs<PinBallLevelDisplaySprite>(o => o.Level5);
        #endregion
        #region "Level6"
        public bool Level6
        {
            get
            {
                return m_Level >= 6;
            }
        }
        static readonly PropertyChangedEventArgs m_Level6Args =
            NotifyPropertyChangedHelper.CreateArgs<PinBallLevelDisplaySprite>(o => o.Level6);
        #endregion
        #region "Level7"
        public bool Level7
        {
            get
            {
                return m_Level >= 7;
            }
        }
        static readonly PropertyChangedEventArgs m_Level7Args =
            NotifyPropertyChangedHelper.CreateArgs<PinBallLevelDisplaySprite>(o => o.Level7);
        #endregion
        #region "Level8"
        public bool Level8
        {
            get
            {
                return m_Level >= 8;
            }
        }
        static readonly PropertyChangedEventArgs m_Level8Args =
            NotifyPropertyChangedHelper.CreateArgs<PinBallLevelDisplaySprite>(o => o.Level8);
        #endregion
        #region "Level9"
        public bool Level9
        {
            get
            {
                return m_Level >= 9;
            }
        }
        static readonly PropertyChangedEventArgs m_Level9Args =
            NotifyPropertyChangedHelper.CreateArgs<PinBallLevelDisplaySprite>(o => o.Level9);
        #endregion
        #region "Level10"
        public bool Level10
        {
            get
            {
                return m_Level >= 10;
            }
        }
        static readonly PropertyChangedEventArgs m_Level10Args =
            NotifyPropertyChangedHelper.CreateArgs<PinBallLevelDisplaySprite>(o => o.Level10);
        #endregion

    }
}
