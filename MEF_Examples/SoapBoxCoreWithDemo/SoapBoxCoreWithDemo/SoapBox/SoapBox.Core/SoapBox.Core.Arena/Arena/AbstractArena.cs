#region "SoapBox.Core License"
/// <header module="SoapBox.Core"> 
/// Copyright (C) 2009 SoapBox Automation Inc., All Rights Reserved.
/// Contact: SoapBox Automation Licencing (license@soapboxautomation.com)
/// 
/// This file is part of SoapBox Core.
/// 
/// Commercial Usage
/// Licensees holding valid SoapBox Automation Commercial licenses may use  
/// this file in accordance with the SoapBox Automation Commercial License
/// Agreement provided with the Software or, alternatively, in accordance 
/// with the terms contained in a written agreement between you and
/// SoapBox Automation Inc.
/// 
/// GNU Lesser General Public License Usage
/// SoapBox Core is free software: you can redistribute it and/or modify 
/// it under the terms of the GNU Lesser General Public License
/// as published by the Free Software Foundation, either version 3 of the
/// License, or (at your option) any later version.
/// 
/// SoapBox Core is distributed in the hope that it will be useful, 
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Lesser General Public License for more details.
/// 
/// You should have received a copy of the GNU Lesser General Public License 
/// along with SoapBox Core. If not, see <http://www.gnu.org/licenses/>.
/// </header>
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

using AdvanceMath;
using Physics2DDotNet;
using Physics2DDotNet.Detectors;
using Physics2DDotNet.Solvers;
using Physics2DDotNet.PhysicsLogics;
using Physics2DDotNet.Shapes;
using SoapBox.Core;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using Physics2DDotNet.Joints;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Input;
using System.ComponentModel;

namespace SoapBox.Core.Arena
{
    public abstract class AbstractArena : AbstractDocument, IArena
    {
        #region "Gravity"
        /// <summary>
        /// Represents the gravity field in the Arena.
        /// Defaults to no gravity.
        /// Can include left/right(X) and up/down(Y) components.
        /// Positive X is to the right, Positive Y is up.
        /// </summary>
        public ArenaVector Gravity
        {
            get
            {
                return m_Gravity;
            }
            set
            {
                if (m_Gravity.X != value.X || m_Gravity.Y != value.Y)
                {
                    m_Gravity = value;
                    m_GravityLogic.Lifetime.IsExpired = true;
                    m_GravityLogic = (PhysicsLogic)new GravityField(new Vector2D(m_Gravity.X, m_Gravity.Y), new Lifespan());
                    m_engine.AddLogic(m_GravityLogic);
                    NotifyPropertyChanged(m_GravityArgs);
                }
            }
        }
        private ArenaVector m_Gravity = new ArenaVector(); // default zero
        static readonly PropertyChangedEventArgs m_GravityArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArena>(o => o.Gravity);
        private PhysicsLogic m_GravityLogic = (PhysicsLogic)new GravityField(new Vector2D(0f, 0f), new Lifespan());
        #endregion

        #region "Scale"
        /// <summary>
        /// Represents the number of screen elements (pixels?)
        /// per each physics unit (meters?).
        /// </summary>
        public float Scale
        {
            get
            {
                return m_Scale;
            }
            set
            {
                if (m_Scale != value)
                {
                    m_Scale = value;
                    NotifyPropertyChanged(m_ScaleArgs);
                }
            }
        }
        private float m_Scale = 1.0f;
        static readonly PropertyChangedEventArgs m_ScaleArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArena>(o => o.Scale);
        #endregion

        #region "TargetInterval"
        /// <summary>
        /// Represents the period (seconds) between running
        /// the physics calculations.
        /// </summary>
        public float TargetInterval
        {
            get
            {
                return m_timer.TargetInterval;
            }
            set
            {
                if (m_timer.TargetInterval != value)
                {
                    m_timer.TargetInterval = value;
                    NotifyPropertyChanged(m_TargetIntervalArgs);
                }
            }
        }
        static readonly PropertyChangedEventArgs m_TargetIntervalArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArena>(o => o.TargetInterval);
        #endregion

        #region "Bodies"
        /// <summary>
        /// A collection of bodies in the arena.
        /// Use AddArenaBody and RemoveArenaBody
        /// to manipulate this collection from the
        /// derived class.
        /// </summary>
        public IEnumerable<IArenaBody> Bodies
        {
            get
            {
                return from obj in m_Bodies orderby obj.Sprite.ZIndex select obj;
            }
        }
        private readonly ObservableCollection<IArenaBody> m_Bodies = 
            new ObservableCollection<IArenaBody>();
        
        // Each item will have a "body" that represents it in the
        // physics engine, so keep track of that mapping
        private readonly Dictionary<IArenaBody, Body> m_BodiesLookup =
            new Dictionary<IArenaBody, Body>();
        private readonly Dictionary<Body, IArenaBody> m_BodiesReverseLookup =
            new Dictionary<Body, IArenaBody>();


        /// <summary>
        /// Add a body to the arena.  
        /// Use AbstractArenaFreeBody or AbstractArenaStationaryBody 
        /// as a base.
        /// </summary>
        /// <param name="item"></param>
        protected void AddArenaBody(IArenaBody obj)
        {
            AddArenaBody(obj, 2f);
        }

        /// <summary>
        /// Default grid spacing is 2.  Override it with a higher number
        /// for less accuracy, but better performance.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="gridSpacing"></param>
        protected void AddArenaBody(IArenaBody obj, float gridSpacing)
        {
            if (!m_Bodies.Contains(obj))
            {
                m_Bodies.Add(obj);

                // Copy the initial properties to the state
                obj.State.Position.X = obj.InitialX;
                obj.State.Position.Y = obj.InitialY;
                obj.State.Angle = obj.InitialAngle;

                // Add it to the physics engine
                PhysicsState state = new PhysicsState(new ALVector2D(obj.InitialAngle, obj.InitialX, obj.InitialY));
                IShape shape = null;
                Coefficients coff = new Coefficients(obj.Restitution, obj.Friction, obj.Friction);
                Lifespan life = new Lifespan();

                // Special case - circles.  They won't roll right unless we define them as CircleShapes.
                if (obj.Sprite.Geometry is EllipseGeometry)
                {
                    EllipseGeometry eg = (EllipseGeometry)obj.Sprite.Geometry;
                    if (eg.RadiusX == eg.RadiusY)
                    {
                        shape = new CircleShape((float)eg.RadiusX, Convert.ToInt32(eg.RadiusX) * 4);
                    }
                }

                if (shape == null)
                {
                    // Have to convert the geometry into a polygon (points)
                    Collection<Point> points = new Collection<Point>();
                    PathGeometry pg = obj.Sprite.Geometry.GetFlattenedPathGeometry(1f, ToleranceType.Absolute);
                    foreach (PathFigure pf in pg.Figures)
                    {
                        // a figure is a list of segments, and a starting point
                        points.Add(pf.StartPoint);
                        foreach (PathSegment ps in pf.Segments)
                        {
                            // Path segments can be made up of LineSegments or
                            // PolyLineSegments
                            if (ps is LineSegment)
                            {
                                Point p = ((LineSegment)ps).Point;
                                if (!points.Contains(p))
                                {
                                    points.Add(p);
                                }
                            }
                            else if (ps is PolyLineSegment)
                            {
                                foreach (Point p in ((PolyLineSegment)ps).Points)
                                {
                                    if (!points.Contains(p))
                                    {
                                        points.Add(p);
                                    }
                                }
                            }
                            else
                            {
                                // ignore?
                            }
                        }
                    }

                    // convert to an array of Vector2D objects so Physics2D.Net understands it
                    Vector2D[] vectorPoints = new Vector2D[points.Count];
                    for (int i = 0; i < points.Count; i++)
                    {
                        vectorPoints[i] = new Vector2D(
                            Convert.ToSingle(points[i].X),
                            Convert.ToSingle(points[i].Y));
                    }

                    if (vectorPoints.Length < 2)
                    {
                        shape = new CircleShape(float.Epsilon, 4);
                    }
                    else
                    {
                        shape = new PolygonShape(VertexHelper.Subdivide(vectorPoints, 5), gridSpacing);
                    }
                }

                Body bdy;
                if (obj is IArenaPivotingBody)
                {
                    IArenaPivotingBody pivotObj = (IArenaPivotingBody)obj;

                    // Have to calculate the position of the joint, accounting for the initial angle
                    // theta1 is the initial angle of the pivot point relative to the center of mass
                    double theta1 = Math.Atan2(pivotObj.PivotPoint.Y, pivotObj.PivotPoint.X);
                    double newTheta = theta1 + pivotObj.InitialAngle;
                    double magnitude = Math.Sqrt(pivotObj.PivotPoint.X * pivotObj.PivotPoint.X + 
                        pivotObj.PivotPoint.Y * pivotObj.PivotPoint.Y);
                    float jointX = Convert.ToSingle(magnitude * Math.Cos(newTheta));
                    float jointY = Convert.ToSingle(magnitude * Math.Sin(newTheta));

                    bdy = new Body(state, shape, ((IArenaPivotingBody)obj).Mass, coff, life);
                    bdy.AngularDamping = 1.0f - ((IArenaPivotingBody)obj).PivotFriction;
                    bdy.IgnoresGravity = ((IArenaPivotingBody)obj).IgnoresGravity;
                    m_engine.AddBody(bdy);
                    FixedHingeJoint jnt = new FixedHingeJoint(bdy, 
                        new Vector2D(obj.InitialX + jointX, obj.InitialY + jointY), 
                        life);
                    jnt.Softness = 0;
                    jnt.DistanceTolerance = 200f;
                    m_engine.AddJoint(jnt);
                }
                else if (obj is IArenaDynamicBody)
                {
                    bdy = new Body(state, shape, ((IArenaDynamicBody)obj).Mass, coff, life);
                    bdy.IgnoresGravity = ((IArenaDynamicBody)obj).IgnoresGravity;
                    m_engine.AddBody(bdy);
                }
                else if (obj is IArenaDecorationBody)
                {
                    bdy = new Body(state, shape, float.PositiveInfinity, coff, life);
                    bdy.IgnoresGravity = true;
                    bdy.IgnoresCollisionResponse = true;
                    m_engine.AddBody(bdy);
                }
                else if (obj is IArenaStationaryBody)
                {
                    bdy = new Body(state, shape, float.PositiveInfinity, coff, life);
                    bdy.IgnoresGravity = true;
                    m_engine.AddBody(bdy);
                }
                else
                {
                    bdy = new Body(state, shape, 0f, coff, life);
                    m_engine.AddBody(bdy);
                }

                // hook up the collided event
                bdy.Collided += new EventHandler<CollisionEventArgs>(Body_Collided);
                
                // hook up the sprite ZIndex changed event handler
                obj.Sprite.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Sprite_PropertyChanged);

                m_BodiesLookup.Add(obj, bdy);
                m_BodiesReverseLookup.Add(bdy, obj);
            }
        }

        /// <summary>
        /// Remove a body from the arena.
        /// </summary>
        /// <param name="item"></param>
        protected void RemoveArenaBody(IArenaBody obj)
        {
            if (m_Bodies.Contains(obj))
            {
                m_Bodies.Remove(obj);

                // Remove it from the physics engine
                m_BodiesLookup[obj].Lifetime.IsExpired = true;
                m_BodiesReverseLookup.Remove(m_BodiesLookup[obj]);
                m_BodiesLookup.Remove(obj);
            }
        }
        #endregion
        #region "Body Collisions"
        private void Body_Collided(object sender, CollisionEventArgs e)
        {
            Body bdy = sender as Body;
            if (bdy != null)
            {
                Body otherBody = null;
                if (e.Contact.Body1 == bdy) // reference equality
                {
                    otherBody = e.Contact.Body2;
                }
                else
                {
                    otherBody = e.Contact.Body1;
                }
                if (otherBody != null)
                {
                    lock (m_bodyCollisionQueue_Lock)
                    {
                        m_bodyCollisionQueue.Enqueue(new CollisionBodies(bdy, otherBody));
                    }
                }
            }
        }

        private readonly Queue<CollisionBodies> m_bodyCollisionQueue = new Queue<CollisionBodies>();
        private object m_bodyCollisionQueue_Lock = new object();

        private class CollisionBodies
        {
            public Body Body1 { get; private set; }
            public Body Body2 { get; private set; }

            public CollisionBodies(Body body1, Body body2)
            {
                Body1 = body1;
                Body2 = body2;
            }
        }
        #endregion
        #region "Sprite PropertyChanged"

        void Sprite_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == m_ZIndexName)
            {
                NotifyPropertyChanged(m_BodiesArgs);
            }
        }
        static readonly PropertyChangedEventArgs m_BodiesArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArena>(o => o.Bodies);
        static readonly string m_ZIndexName =
            NotifyPropertyChangedHelper.GetPropertyName<ISprite>(o => o.ZIndex);
        #endregion

        #region "ViewPortWidth"
        /// <summary>
        /// Represents the number of screen elements (pixels?)
        /// per each physics unit (meters?).
        /// </summary>
        public float ViewPortWidth
        {
            get
            {
                return m_ViewPortWidth;
            }
            set
            {
                if (m_ViewPortWidth != value)
                {
                    if (value < 0f)
                    {
                        throw new ArgumentOutOfRangeException(m_ViewPortWidthName);
                    }
                    m_ViewPortWidth = value;
                    NotifyPropertyChanged(m_ViewPortWidthArgs);
                    NotifyPropertyChanged(m_ViewPortOffsetXArgs);
                }
            }
        }
        private float m_ViewPortWidth;
        static readonly PropertyChangedEventArgs m_ViewPortWidthArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArena>(o => o.ViewPortWidth);
        static readonly string m_ViewPortWidthName =
            NotifyPropertyChangedHelper.GetPropertyName<AbstractArena>(o => o.ViewPortWidth);
        #endregion

        #region "ViewPortHeight"
        /// <summary>
        /// Represents the number of screen elements (pixels?)
        /// per each physics unit (meters?).
        /// </summary>
        public float ViewPortHeight
        {
            get
            {
                return m_ViewPortHeight;
            }
            set
            {
                if (m_ViewPortHeight != value)
                {
                    if (value < 0f)
                    {
                        throw new ArgumentOutOfRangeException(m_ViewPortHeightName);
                    }
                    m_ViewPortHeight = value;
                    NotifyPropertyChanged(m_ViewPortHeightArgs);
                    NotifyPropertyChanged(m_ViewPortOffsetYArgs);
                }
            }
        }
        private float m_ViewPortHeight;
        static readonly PropertyChangedEventArgs m_ViewPortHeightArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArena>(o => o.ViewPortHeight);
        static readonly string m_ViewPortHeightName =
            NotifyPropertyChangedHelper.GetPropertyName<AbstractArena>(o => o.ViewPortHeight);
        #endregion

        #region "ViewPortOffsetX"
        /// <summary>
        /// Represents the number of screen elements (pixels?)
        /// per each physics unit (meters?).
        /// </summary>
        public float ViewPortOffsetX
        {
            get
            {
                return m_ViewPortWidth / 2;
            }
        }
        static readonly PropertyChangedEventArgs m_ViewPortOffsetXArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArena>(o => o.ViewPortOffsetX);
        #endregion

        #region "ViewPortOffsetY"
        /// <summary>
        /// Represents the number of screen elements (pixels?)
        /// per each physics unit (meters?).
        /// </summary>
        public float ViewPortOffsetY
        {
            get
            {
                return m_ViewPortHeight / 2;
            }
        }
        static readonly PropertyChangedEventArgs m_ViewPortOffsetYArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArena>(o => o.ViewPortOffsetY);
        #endregion

        Object m_engineLock = new object();
        PhysicsEngine m_engine;
        PhysicsTimer m_timer;

        public AbstractArena()
        {
            // Setup the Physics Engine
            m_engine = new PhysicsEngine();
            m_engine.BroadPhase = (BroadPhaseCollisionDetector)new SweepAndPruneDetector();
            m_engine.Solver = (CollisionSolver)new SequentialImpulsesSolver();

            // Pulled these values from their demo.  They are "popular" choices.
            SequentialImpulsesSolver phsSolver = (SequentialImpulsesSolver)m_engine.Solver;
            phsSolver.Iterations = 12;
            phsSolver.SplitImpulse = true;
            phsSolver.BiasFactor = 0.7f;
            phsSolver.AllowedPenetration = 0.1f;

            // Determines how fast we recalculate the simulation
            // Can be controlled with the TargetInterval property
            m_timer = new PhysicsTimer(timer_Callback, 0.01f);

            // Default gravity field is nothing
            m_engine.AddLogic(m_GravityLogic);

        }

        void timer_Callback(float dt, float trueDt)
        {
            // Be careful here, this runs on another thread
            lock (m_engineLock)
            {
                m_engine.Update(dt, trueDt);
            }

            // We're using this as a bit of a hack.  By telling the View that BodyCheck
            // has changed (which it binds to), it forces that thread
            // to call the Get method, which updates the bodies safely.
            NotifyPropertyChanged(m_BodyCheckArgs);
        }

        public bool BodyCheck
        {
            get
            {
                updateBodies();
                return true;
            }
        }
        static readonly PropertyChangedEventArgs m_BodyCheckArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArena>(o => o.BodyCheck);

        private void updateBodies()
        {
            lock (m_engineLock)
            {
                foreach (Body bdy in m_BodiesReverseLookup.Keys)
                {
                    IArenaBody obj = m_BodiesReverseLookup[bdy];

                    // Update physics quantities
                    obj.State.Position.X = bdy.State.Position.X;
                    obj.State.Position.Y = bdy.State.Position.Y;
                    obj.State.Angle = bdy.State.Position.Angular;
                    obj.State.Velocity.X = bdy.State.Velocity.X;
                    obj.State.Velocity.Y = bdy.State.Velocity.Y;

                    // Update screen co-ordinates
                    obj.State.ScreenX = (bdy.State.Position.X * Scale) + ViewPortOffsetX;
                    obj.State.ScreenY = -(bdy.State.Position.Y * Scale) + ViewPortOffsetY;
                    obj.State.ScreenAngle = -obj.State.Angle * 180.0f / Convert.ToSingle(Math.PI);
                    obj.State.Scale = Scale;

                    // allows each item to respond to the new state
                    obj.OnUpdate(); 

                    if (obj is IArenaDynamicBody)
                    {
                        IArenaDynamicBody arenaBody = (IArenaDynamicBody)obj;
                        bdy.ApplyTorque(arenaBody.Torque);
                        bdy.ApplyForce(
                            new Vector2D(arenaBody.Force.X, arenaBody.Force.Y),
                            new Vector2D(arenaBody.ForcePosition.X, arenaBody.ForcePosition.Y));
                        bdy.ApplyImpulse(
                            new Vector2D(arenaBody.Impulse.X, arenaBody.Impulse.Y),
                            new Vector2D(arenaBody.ImpulsePosition.X, arenaBody.ImpulsePosition.Y));
                    }
                }
            }
            lock (m_bodyCollisionQueue_Lock)
            {
                while (m_bodyCollisionQueue.Count > 0)
                {
                    CollisionBodies bodies = m_bodyCollisionQueue.Dequeue();
                    IArenaBody obj = m_BodiesReverseLookup[bodies.Body1];
                    IArenaBody otherObj = m_BodiesReverseLookup[bodies.Body2];
                    obj.OnCollision(otherObj);
                }
            }
        }

        #region "Control the Simulation"

        protected void Start()
        {
            m_timer.IsRunning = true;
            NotifyPropertyChanged(m_IsRunningArgs);
        }

        protected void Stop()
        {
            m_timer.IsRunning = false;
            NotifyPropertyChanged(m_IsRunningArgs);
        }

        /// <summary>
        /// Readonly property that returns true if the 
        /// simulation is running, false otherwise.
        /// Control this with the Start/Stop methods.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return m_timer.IsRunning;
            }
        }
        static readonly PropertyChangedEventArgs m_IsRunningArgs =
            NotifyPropertyChangedHelper.CreateArgs<AbstractArena>(o => o.IsRunning);

        #endregion

        #region "Keyboard Input"
        public virtual void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e) { }
        public virtual void OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e) { }
        #endregion

        #region "Mouse Input"
        public virtual void OnMouseDown(object sender, MouseButtonEventArgs e) { }
        public virtual void OnMouseUp(object sender, MouseButtonEventArgs e) { }
        public virtual void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) { }
        public virtual void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) { }
        public virtual void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e) { }
        public virtual void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e) { }
        public virtual void OnMouseMove(object sender, MouseEventArgs e) { }
        public virtual void OnMouseWheel(object sender, MouseEventArgs e) { }
        public virtual void OnMouseEnter(object sender, MouseEventArgs e) { }
        public virtual void OnMouseLeave(object sender, MouseEventArgs e) { }
        #endregion

        #region "Focus"
        // The layout manager can give us focus (by the user clicking on the document tab)
        // without us ever getting focus directly.  Therefore we have to handle the 
        // OnGotFocus method and raise our own event, and let our View make sure it grabs
        // the focus appropriately.  This is only because the Arena is a weird beast.  It
        // has a bunch of visual controls (shapes, etc.) that don't normally get keyboard
        // focus, and yet we want to react to keyboard input.
        public event RoutedEventHandler GotFocus;
        public override void OnGotFocus(object sender, RoutedEventArgs e)
        {
            base.OnGotFocus(sender, e);
            GotFocus(this, e);
        }
        #endregion
    }
}
