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
using SoapBox.Core;


namespace SoapBox.Demo.HighScores
{
    /// <summary>
    /// Implements a single high score (immutable)
    /// </summary>
    [Serializable]
    public class HighScore : IComparable
    {
        public HighScore(string name, int score, int level)
        {
            m_Name = name;
            m_Score = score;
            m_Level = level;
        }

        #region "Name"

        public string Name
        {
            get
            {
                return m_Name;
            }
        }
        private readonly string m_Name;

        #endregion

        #region "Score"

        public int Score
        {
            get
            {
                return m_Score;
            }
        }
        private readonly int m_Score;

        #endregion

        #region "Level"

        public int Level
        {
            get
            {
                return m_Level;
            }
        }
        private readonly int m_Level;

        #endregion

        #region " Implement IComparable "

        public int CompareTo(object obj)
        {
            if (obj is HighScore)
            {
                HighScore otherHighScore = (HighScore)obj;
                return this.Score.CompareTo(otherHighScore.Score);
            }
            else
            {
                throw new ArgumentException("Object is not a HighScore");
            }
        }

        #endregion

    }
}
