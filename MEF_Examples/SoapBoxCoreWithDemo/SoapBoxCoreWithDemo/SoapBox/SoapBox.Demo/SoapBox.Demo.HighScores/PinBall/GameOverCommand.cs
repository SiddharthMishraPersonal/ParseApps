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
using System.ComponentModel.Composition;
using SoapBox.Core;
using SoapBox.Demo.PinBall;

namespace SoapBox.Demo.HighScores
{
    /// <summary>
    /// This extends the basic pinball table by saving the score and
    /// level attained to a log when the game is over.
    /// </summary>
    [Export(SoapBox.Demo.PinBall.ExtensionPoints.PinBall.GameOverCommands, typeof(IExecutableCommand))]
    class GameOverCommand : AbstractExtension, IExecutableCommand
    {
        [Import(CompositionPoints.Workbench.Pads.HighScores, typeof(HighScores))]
        private Lazy<HighScores> highScores { get; set; }

        /// <summary>
        /// Registers the high scores with the HighScores ViewModel
        /// </summary>
        /// <param name="args"></param>
        public void Run(params object[] args)
        {
            // arg 0 = PinBallTable
            if (args.Length >= 1)
            {
                PinBallTable table = args[0] as PinBallTable;
                if (table != null)
                {
                    highScores.Value.LogNewHighScore(String.Empty, table.Score, table.Level);
                }
            }
        }
    }
}
