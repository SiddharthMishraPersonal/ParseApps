﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoapBox.Demo.PinBall.CompositionPoints
{
    public static class PinBall
    {
        public const string PinBallTable = "CompositionPoints.PinBall.PinBallTable";
        public const string OptionsPad = "CompositionPoints.PinBall.OptionsPad";
    }

    public static class Workbench
    {
        public static class Pads
        {
            public const string Instructions = "CompositionPoints.Workbench.Pads.Instructions";
        }

        public static class StatusBar
        {
            public const string BallCounterHeading = "CompositionPoints.Workbench.StatusBar.BallCounterHeading";
            public const string BallCounterText = "CompositionPoints.Workbench.StatusBar.BallCounterText";
            public const string BallsScoreSeparator = "CompositionPoints.Workbench.StatusBar.BallsScoreSeparator";
            public const string ScoreDisplayHeading = "CompositionPoints.Workbench.StatusBar.ScoreDisplayHeading";
            public const string ScoreDisplayText = "CompositionPoints.Workbench.StatusBar.ScoreDisplayText";
        }
    }
}
