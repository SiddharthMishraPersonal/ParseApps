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
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace SoapBox.Demo.HighScores
{
    [Export(SoapBox.Core.ExtensionPoints.Workbench.Pads, typeof(IPad))]
    [Export(CompositionPoints.Workbench.Pads.HighScores, typeof(HighScores))]
    [Pad(Name = HighScores.PAD_NAME)]
    class HighScores : AbstractPad 
    {
        public const string PAD_NAME = "HighScores";

        private readonly string FILE_NAME = Path.Combine(AppEnvironment.UserFileDirectory(), "HighScores.dat");

        [ImportingConstructor]
        public HighScores([Import(SoapBox.Core.Services.Logging.LoggingService, typeof(ILoggingService))] ILoggingService l)
        {
            logger = l;

            Name = PAD_NAME;
            Title = Resources.Strings.Pads_HighScores_Title;

            Stream stream = null;
            try
            {
                // Read High Scores
                stream = new FileStream(FILE_NAME, System.IO.FileMode.Open, FileAccess.Read);
                BinaryFormatter b = new BinaryFormatter();
                lock (m_ScoresUnsorted_Lock)
                {
                    m_ScoresUnsorted = (List<HighScore>)b.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                logger.Error("Error reading high scores:", e);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            CreateObservable();
        }

        private ILoggingService logger { get; set; }

        public IEnumerable<HighScore> Scores
        {
            get
            {
                return m_Scores;
            }
        }
        private readonly object m_ScoresUnsorted_Lock = new Object();
        private List<HighScore> m_ScoresUnsorted = new List<HighScore>();
        private readonly ObservableCollection<HighScore> m_Scores = new ObservableCollection<HighScore>();

        public void LogNewHighScore(string name, int score, int level)
        {
            lock (m_ScoresUnsorted_Lock)
            {
                m_ScoresUnsorted.Add(new HighScore(name, score, level));
            }
            CreateObservable();

            ThreadPool.UnsafeQueueUserWorkItem(ignoredState =>
            {
                // Make a copy, and be sure to lock while accessing shared state
                List<HighScore> tempScores = new List<HighScore>();
                lock (m_ScoresUnsorted_Lock)
                {
                    foreach (HighScore highScore in m_ScoresUnsorted)
                    {
                        tempScores.Add(highScore);
                    }
                }

                Stream stream = null;
                try
                {
                    stream = new FileStream(FILE_NAME, System.IO.FileMode.Create, FileAccess.Write);
                    BinaryFormatter b = new BinaryFormatter();
                    b.Serialize(stream, tempScores); 
                }
                catch
                {
                    // Can't do much here.  The logger isn't thread safe (or not sure that it is)
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }
            }, null);

        }

        private void CreateObservable()
        {
            lock (m_ScoresUnsorted_Lock)
            {
                m_ScoresUnsorted.Sort();
                m_Scores.Clear();
                for (int i = m_ScoresUnsorted.Count - 1; i >= 0; i--)
                {
                    m_Scores.Add(m_ScoresUnsorted[i]);
                }
            }
        }

    }
}
