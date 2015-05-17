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
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SoapBox.Core;
using System.ComponentModel.Composition;

namespace SoapBox.Demo.PinBall
{
    [Export(SoapBox.Core.ExtensionPoints.Workbench.Documents, typeof(IDocument))] // so the Workbench can save/restore
    [Export(CompositionPoints.PinBall.PinBallTable, typeof(PinBallTable))] // so the new View Menu item can refer to it
    [Document(Name = PinBallTable.DOCUMENT_NAME)]
    public class PinBallTable : AbstractArena, IPartImportsSatisfiedNotification
    {
        public const string DOCUMENT_NAME = "PinBallTable";

        public const float HARD_SURFACE_FRICTION = 0.00001f;
        public const float HARD_SURFACE_RESTITUTION = 0.2f;

        public const int TABLE_DIMENSION_X = 800;
        public const int TABLE_DIMENSION_Y = 1200;

        #region " DimensionX "
        public float DimensionX
        {
            get
            {
                return TABLE_DIMENSION_X;
            }
        }
        #endregion

        #region " DimensionY "
        public float DimensionY
        {
            get
            {
                return TABLE_DIMENSION_Y;
            }
        }
        #endregion

        [ImportingConstructor]
        public PinBallTable(
            [Import(CompositionPoints.Workbench.StatusBar.BallCounterHeading, typeof(BallCounterHeading))] BallCounterHeading ballCounterHeading,
            [Import(CompositionPoints.Workbench.StatusBar.BallCounterText, typeof(BallCounterText))] BallCounterText ballCounterText,
            [Import(CompositionPoints.Workbench.StatusBar.BallsScoreSeparator, typeof(BallsScoreSeparator))] BallsScoreSeparator ballsScoreSeparator,
            [Import(CompositionPoints.Workbench.StatusBar.ScoreDisplayHeading, typeof(ScoreDisplayHeading))] ScoreDisplayHeading scoreDisplayHeading,
            [Import(CompositionPoints.Workbench.StatusBar.ScoreDisplayText, typeof(ScoreDisplayText))] ScoreDisplayText scoreDisplayText)
        {

            // IDocument properties
            Name = DOCUMENT_NAME;
            Title = Resources.Strings.Arena_PinBallTable_Title;

            // Init the ball counter (status bar)
            m_ballCounterText = ballCounterText;
            m_ballCounterText.SetBallCount(Balls);
            m_scoreDisplayText = scoreDisplayText;
            m_scoreDisplayText.SetScore(0);
            // allow us to control visibility
            ballCounterHeading.VisibleCondition = m_IsRunningCondition;
            ballCounterText.VisibleCondition = m_IsRunningCondition;
            ballsScoreSeparator.VisibleCondition = m_IsRunningCondition;
            scoreDisplayHeading.VisibleCondition = m_IsRunningCondition;
            scoreDisplayText.VisibleCondition = m_IsRunningCondition;

            Gravity = new ArenaVector(0.0f, -800.0f);
            Scale = 0.5f; // screen elements per physics unit

            // Build the box around the table
            AddArenaBody(new PinBallTableEdge(this, PinBallTableSide.Bottom), 4f);
            AddArenaBody(new PinBallTableEdge(this, PinBallTableSide.Left), 4f);
            AddArenaBody(new PinBallTableEdge(this, PinBallTableSide.Right), 4f);
            AddArenaBody(new PinBallTableEdge(this, PinBallTableSide.Top), 4f);

            // Return ramp and launcher
            AddArenaBody(new PinBallReturnRamp(this), 3f);
            m_PinBallLauncher = new PinBallLauncher(this);
            foreach (IArenaBody bdy in m_PinBallLauncher.Bodies)
            {
                AddArenaBody(bdy);
            }
            AddArenaBody(new PinBallLauncherGuide(this), 4f);
            AddArenaBody(new PinBallLauncherFlipper(this));

            // Bottom ramps
            AddArenaBody(new PinBallBottomRamp(this, PinBallBottomRampSide.Left), 4f);
            AddArenaBody(new PinBallBottomRamp(this, PinBallBottomRampSide.Right), 4f);

            // Create 3 pinballs
            PinBalls.Add(new PinBall(this, new Point((DimensionX / 2.0f) - 250f, (-DimensionY / 2.0f) + 80f)));
            PinBalls.Add(new PinBall(this, new Point((DimensionX / 2.0f) - 350f, (-DimensionY / 2.0f) + 80f)));
            PinBalls.Add(new PinBall(this, new Point((DimensionX / 2.0f) - 450f, (-DimensionY / 2.0f) + 80f)));
            foreach (PinBall ball in PinBalls)
            {
                AddArenaBody(ball);
            }

            // Create the Pin Ball Stop
            m_PinBallStop = new PinBallStop((DimensionX / 2.0f) - (PinBallLauncher.LAUNCHER_WIDTH + 30f), (-DimensionY / 2.0f) + 47f, -(float)Math.PI / 4.0f);
            AddArenaBody(m_PinBallStop);
            AddArenaBody(new PinBallStopPin((DimensionX / 2.0f) - (PinBallLauncher.LAUNCHER_WIDTH + 30f), (-DimensionY / 2.0f) + 68f));

            // Create the Pin Ball Stop Sensor
            m_PinBallStopPresentSensor = new PinBallCircularSensor(this,
                new Point((DimensionX / 2.0f) - (PinBallLauncher.LAUNCHER_WIDTH + 10f), (-DimensionY / 2.0f) + 25f), PinBall.PIN_BALL_RADIUS);

            // Create the Launcher-Occupied Sensor
            m_LauncherOccupiedSensor = new PinBallRectangularSensor(this,
                new Point(DimensionX / 2f - PinBallLauncher.LAUNCHER_WIDTH, -DimensionY / 2f),
                new Point(DimensionX / 2f, DimensionY / 2f)
                );

            // Create the Ball In Play Sensor
            m_BallInPlaySensor = new PinBallRectangularSensor(this,
                new Point(-DimensionX / 2f, PinBallBottomRamp.BOTTOM_POSITION),
                new Point((DimensionX / 2f) - PinBallLauncher.LAUNCHER_WIDTH - PinBallTableEdge.SIDE_WIDTH, DimensionY / 2f)
                );

            // Create the controller to control the stop
            m_PinBallStopController = new PinBallStopController(this, m_PinBallStop, m_PinBallStopPresentSensor, m_LauncherOccupiedSensor, m_BallInPlaySensor);

            // Flippers
            float effectiveTableWidth = TABLE_DIMENSION_X - PinBallLauncher.LAUNCHER_WIDTH - PinBallTableEdge.SIDE_WIDTH;
            float xOffset = -(TABLE_DIMENSION_X - effectiveTableWidth) / 2f;
            float flipperX = 70f;
            float flipperY = -370f;

            m_RightFlipper = new PinBallFlipper(
                flipperX + xOffset, // x
                flipperY, // y
                Convert.ToSingle(Math.PI / 2f), // angle
                10f); // mass
            AddArenaBody(m_RightFlipper);
            AddArenaBody(new Pin((flipperX + 48f) + xOffset, flipperY - 19f, 2f));
            AddArenaBody(new Pin((flipperX + 62f) + xOffset, flipperY + 21f, 2f));

            m_LeftFlipper = new PinBallFlipper(
                -flipperX + xOffset, // x
                flipperY, // y
                -Convert.ToSingle(Math.PI / 2f), // angle
                10f); // mass
            AddArenaBody(m_LeftFlipper);
            AddArenaBody(new Pin(-(flipperX + 48f) + xOffset, flipperY - 19f, 2f));
            AddArenaBody(new Pin(-(flipperX + 62f) + xOffset, flipperY + 21f, 2f));

            // Flipper mounts (the things next to the flippers)
            AddArenaBody(new PinBallFlipperMount(this, PinBallFlipperMountSide.Left));
            AddArenaBody(new PinBallFlipperMount(this, PinBallFlipperMountSide.Right));

            // Bottom bumpers (triangular things with rounded corners above the flippers)
            PinBallBottomBumper leftBumper = new PinBallBottomBumper(this, PinBallBottomBumperSide.Left);
            AddArenaBody(leftBumper); 
            PinBallBottomBumper rightBumper = new PinBallBottomBumper(this, PinBallBottomBumperSide.Right);
            AddArenaBody(rightBumper); 
            
            // Partitions between the bumpers and the walls
            AddArenaBody(new PinBallPartition(
                new Point(TABLE_DIMENSION_X / 2f - PinBallLauncher.LAUNCHER_WIDTH - 60f - PinBallTableEdge.SIDE_WIDTH, -245f),
                new Point(TABLE_DIMENSION_X / 2f - PinBallLauncher.LAUNCHER_WIDTH - 60f - PinBallTableEdge.SIDE_WIDTH, -115f),
                10f));
            AddArenaBody(new PinBallPartition(
                new Point(-TABLE_DIMENSION_X / 2f + 60f, -245f),
                new Point(-TABLE_DIMENSION_X / 2f + 60f, -115f),
                10f));

            #region "Customizable"

            PinBallRoundedTop top = new PinBallRoundedTop(this);
            AddArenaBody(top, 6f); // use a bigger grid spacing to improve performance

            // Top kickers
            addKicker(new PinBallKicker(45f, -100f, 420f));
            addKicker(new PinBallKicker(45f, 5f, 300f));
            addKicker(new PinBallKicker(45f, 130f, 390f));

            // Right kicker
            addKicker(new PinBallKicker(45f, 300f, 140f));

            // Targets
            float interTargetDistance = PinBallTarget.TARGET_WIDTH + 1f;

            // Left side
            float leftTop = 267f;
            float xOffsetDelta = 4f;
            float leftAngle = -0.5f * (float)Math.PI - (float)Math.Atan2(xOffsetDelta * 4f, interTargetDistance * 4f);
            float leftX = -this.DimensionX / 2f + 3f;
            addTarget(new PinBallTarget(leftX + 3f * xOffsetDelta, leftTop - 0f * interTargetDistance, leftAngle, true));
            addTarget(new PinBallTarget(leftX + 2f * xOffsetDelta, leftTop - 1f * interTargetDistance, leftAngle, true));
            addTarget(new PinBallTarget(leftX + 1f * xOffsetDelta, leftTop - 2f * interTargetDistance, leftAngle, true));
            addTarget(new PinBallTarget(leftX + 0f * xOffsetDelta, leftTop - 3f * interTargetDistance, leftAngle, true));

            // Middle top side
            float middleTop = 267f;
            float xInterTarget = 0.707f * interTargetDistance;
            float yInterTarget = 0.707f * interTargetDistance;
            float topAngle = 0.75f * (float)Math.PI;
            float topX = -40f;
            addTarget(new PinBallTarget(topX + 2f * xInterTarget, middleTop - 3f * yInterTarget, topAngle, true));
            addTarget(new PinBallTarget(topX + 1f * xInterTarget, middleTop - 2f * yInterTarget, topAngle, true));
            addTarget(new PinBallTarget(topX + 0f * xInterTarget, middleTop - 1f * yInterTarget, topAngle, true));

            // Right side
            float rightTop = 130f;
            float xInterTargetRight = 0.707f * interTargetDistance;
            float yInterTargetRight = 0.707f * interTargetDistance;
            float rightAngle = 0.75f * (float)Math.PI;
            float rightX = 285f;
            addTarget(new PinBallTarget(rightX + 1f * xInterTargetRight, rightTop - 2f * yInterTargetRight, rightAngle, true));
            addTarget(new PinBallTarget(rightX + 0f * xInterTargetRight, rightTop - 1f * yInterTargetRight, rightAngle, true));

            // Bonus indicators
            addBonus(new PinBallBonus(this, new Point(253, -187)));
            addBonus(new PinBallBonus(this, new Point(-303, -187)));

            // Level trigger indicators
            addLevelTrigger(new PinBallLevelTrigger(this, new Point(320, -187)));
            addLevelTrigger(new PinBallLevelTrigger(this, new Point(-372, -187)));

            // Level display
            levelDisplay = new PinBallLevelDisplay(this, new Point(-(PinBallLauncher.LAUNCHER_WIDTH + PinBallTableEdge.SIDE_WIDTH) / 2f, -160f));

            #endregion

            // Detect when the game is over by the number of balls
            PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(PinBallTable_PropertyChanged);
        }

        [Import(SoapBox.Core.Services.Logging.LoggingService, typeof(ILoggingService))]
        private ILoggingService logger { get; set; }

        private BallCounterText m_ballCounterText = null;
        private ScoreDisplayText m_scoreDisplayText = null;

        [Import(SoapBox.Core.Services.Host.ExtensionService)]
        private IExtensionService extensionService { get; set; }

        [ImportMany(ExtensionPoints.PinBall.GameOverCommands, typeof(IExecutableCommand), AllowRecomposition=true)]
        private IEnumerable<IExecutableCommand> gameOverCommands { get; set; }

        private IList<IExecutableCommand> m_gameOverCommands = null;

        public void OnImportsSatisfied()
        {
            m_gameOverCommands = extensionService.Sort(gameOverCommands);
        }

        #region "Kickers"

        protected void addKicker(PinBallKicker kicker)
        {
            m_kickers.Add(kicker);
            AddArenaBody(kicker);
            kicker.Score += new EventHandler(kicker_Score);
        }
        private readonly Collection<PinBallKicker> m_kickers = new Collection<PinBallKicker>();

        #endregion

        #region "Targets"

        protected void addTarget(PinBallTarget target)
        {
            m_targets.Add(target);
            foreach (IArenaBody bdy in target.Bodies)
            {
                this.AddArenaBody(bdy);
            }
            target.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(target_PropertyChanged);
            target.Score += new EventHandler(target_Score);
        }
        private readonly Collection<PinBallTarget> m_targets = new Collection<PinBallTarget>();

        #endregion

        #region "Bonuses"

        protected void addBonus(PinBallBonus bonus)
        {
            m_bonuses.Add(bonus);
            AddArenaBody(bonus);
        }
        private readonly Collection<PinBallBonus> m_bonuses = new Collection<PinBallBonus>();

        #endregion

        #region "LevelTriggers"

        protected void addLevelTrigger(PinBallLevelTrigger levelTrigger)
        {
            m_levelTriggers.Add(levelTrigger);
            AddArenaBody(levelTrigger);
            levelTrigger.BallDetected += new EventHandler(levelTrigger_BallDetected);
        }
        private readonly Collection<PinBallLevelTrigger> m_levelTriggers = new Collection<PinBallLevelTrigger>();

        #endregion

        #region "LevelDisplay"

        protected PinBallLevelDisplay levelDisplay
        {
            get
            {
                return m_levelDisplay;
            }
            set
            {
                if (m_levelDisplay != null)
                {
                    throw new ArgumentException(m_levelDisplayName);
                }
                m_levelDisplay = value;
                AddArenaBody(value);
            }
        }
        private PinBallLevelDisplay m_levelDisplay = null;
        static readonly string m_levelDisplayName =
            NotifyPropertyChangedHelper.GetPropertyName<PinBallTable>(o => o.levelDisplay);

        #endregion

        #region "Scoring"

        private double levelMultiplier()
        {
            return 1.0 + (Level * 0.1);
        }

        private double bonusMultiplier()
        {
            double retVal = 1;
            foreach (PinBallBonus bonus in m_bonuses)
            {
                if (bonus.IsActive)
                {
                    retVal = retVal * 2;
                }
            }
            logger.InfoWithFormat("Bonus Multiplier: {0}", retVal);
            return retVal;
        }

        void kicker_Score(object sender, EventArgs e)
        {
            Score += Convert.ToInt32(3 * bonusMultiplier() * levelMultiplier());
        }

        void target_Score(object sender, EventArgs e)
        {
            Score += Convert.ToInt32(25 * bonusMultiplier() * levelMultiplier());
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
            private set
            {
                if (m_Score != value)
                {
                    m_Score = value;
                    m_scoreDisplayText.SetScore(value);
                    NotifyPropertyChanged(m_ScoreArgs);
                }
            }
        }
        private int m_Score = 0;
        static readonly PropertyChangedEventArgs m_ScoreArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallTable>(o => o.Score);

        #endregion

        #region "Levels"

        private bool allTargetsHit()
        {
            // See if all targets are hit
            bool foundNotHit = false;
            foreach (PinBallTarget target in m_targets)
            {
                if (!target.Hit)
                {
                    foundNotHit = true;
                    break;
                }
            }
            return !foundNotHit;
        }

        public int Level
        {
            get
            {
                return m_Level;
            }
            set
            {
                if (m_Level != value)
                {
                    m_Level = value;
                    m_levelDisplay.SetLevel(m_Level);
                    NotifyPropertyChanged(m_LevelArgs);
                }
            }
        }
        private int m_Level = 0;
        static readonly PropertyChangedEventArgs m_LevelArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallTable>(o => o.Level);


        void levelTrigger_BallDetected(object sender, EventArgs e)
        {
            if (allTargetsHit())
            {
                logger.Info("Advancing Level...");
                // Advance the level
                Level++;
                if (Level < 10)
                {
                    Balls++; // give them a free ball because they lose this one
                    ResetTriggersAndBonuses();
                }
                else
                {
                    // game over :)
                    Balls = 0;
                }
            }
        }

        private void ResetTriggersAndBonuses()
        {
            // Change everything back to initial conditions
            foreach (PinBallLevelTrigger levelTrigger in m_levelTriggers)
            {
                PinBallLevelTriggerSprite s = levelTrigger.Sprite as PinBallLevelTriggerSprite;
                if (s != null)
                {
                    s.IsActive = false;
                }
            }
            foreach (PinBallTarget target in m_targets)
            {
                target.Hit = false;
            }
            foreach (PinBallBonus bonus in m_bonuses)
            {
                bonus.IsActive = false;
            }
        }

        void target_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == m_HitName)
            {
                if (allTargetsHit())
                {
                    logger.Info("All Targets Hit...");
                    // Indicate to the player that they can advance a level
                    foreach (PinBallLevelTrigger levelTrigger in m_levelTriggers)
                    {
                        PinBallLevelTriggerSprite s = levelTrigger.Sprite as PinBallLevelTriggerSprite;
                        if (s != null)
                        {
                            s.IsActive = true;
                        }
                    }
                }
            }
        }
        static readonly string m_HitName =
            NotifyPropertyChangedHelper.GetPropertyName<PinBallTarget>(o => o.Hit);

        #endregion

        #region "Game Over"

        private void ExecuteGameOverCommands()
        {
            foreach (IExecutableCommand cmd in m_gameOverCommands)
            {
                try
                {
                    cmd.Run(this);
                }
                catch (Exception e)
                {
                    logger.Error("Exception while running command " + cmd.ID, e);
                }
            }
        }

        void PinBallTable_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == m_BallsName)
            {
                m_ballCounterText.SetBallCount(Balls);
                if (Balls <= 0)
                {
                    // game over
                    ResetTriggersAndBonuses();
                    ExecuteGameOverCommands();
                    MessageBox.Show(Resources.Strings.Arena_PinBallTable_GameOverMessage,
                        Resources.Strings.Arena_PinBallTable_GameOver, MessageBoxButton.OK);
                    Balls = 3;
                    Level = 0;
                    m_Score = 0;
                    m_scoreDisplayText.SetScore(0);
                }
            }
            else if (e.PropertyName == m_IsRunningName)
            {
                m_IsRunningCondition.SetCondition(IsRunning);
            }
        }
        static readonly string m_IsRunningName =
            NotifyPropertyChangedHelper.GetPropertyName<PinBallTable>(o => o.IsRunning);
        private ConcreteCondition m_IsRunningCondition = new ConcreteCondition(false);

        #endregion

        #region "Start and Stop"
        public void Play()
        {
            base.Start();
        }

        public void Pause()
        {
            base.Stop();
        }
        #endregion

        private PinBallStop m_PinBallStop = null;
        private PinBallCircularSensor m_PinBallStopPresentSensor = null;
        private PinBallRectangularSensor m_LauncherOccupiedSensor = null;
        private PinBallRectangularSensor m_BallInPlaySensor = null;
        private PinBallStopController m_PinBallStopController = null;

        private PinBallFlipper m_RightFlipper = null;
        private PinBallFlipper m_LeftFlipper = null;

        private PinBallLauncher m_PinBallLauncher = null;

        #region "Keyboard Control"

        public override void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.RightCtrl)
            {
                m_RightFlipper.ControlTorque = -8000000f;
            }
            else if (e.Key == System.Windows.Input.Key.LeftCtrl)
            {
                m_LeftFlipper.ControlTorque = 8000000f;
            }
            else if (e.Key == System.Windows.Input.Key.Space)
            {
                m_PinBallLauncher.PlungerPulled = true;
            }
        }

        public override void OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.RightCtrl)
            {
                m_RightFlipper.ControlTorque = 0;
            }
            else if (e.Key == System.Windows.Input.Key.LeftCtrl)
            {
                m_LeftFlipper.ControlTorque = 0;
            }
            else if (e.Key == System.Windows.Input.Key.Space)
            {
                m_PinBallLauncher.PlungerPulled = false;
            }
        }

        #endregion

        #region "PinBalls"
        public Collection<PinBall> PinBalls
        {
            get
            {
                return m_PinBalls;
            }
        }
        private readonly Collection<PinBall> m_PinBalls = new Collection<PinBall>();
        #endregion

        #region "Balls"
        /// <summary>
        /// Records the count of balls left in the game
        /// </summary>
        public int Balls
        {
            get
            {
                return m_Balls;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(m_BallsName);
                }
                if (m_Balls != value)
                {
                    m_Balls = value;
                    logger.InfoWithFormat("Balls: {0}", m_Balls);
                    NotifyPropertyChanged(m_BallsArgs);

                }
            }
        }
        private int m_Balls = 3;
        static readonly PropertyChangedEventArgs m_BallsArgs =
            NotifyPropertyChangedHelper.CreateArgs<PinBallTable>(o => o.Balls);
        static readonly string m_BallsName =
            NotifyPropertyChangedHelper.GetPropertyName<PinBallTable>(o => o.Balls);
        #endregion
    }
}
