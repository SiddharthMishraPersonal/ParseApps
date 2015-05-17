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
using System.Windows.Media;

namespace SoapBox.Core.Arena
{
    public interface IArenaBody : IViewModel 
    {
        // Initial State
        float InitialX { get; }
        float InitialY { get; }
        float InitialAngle { get; }

        // Properties
        float Restitution { get; } //bounciness
        float Friction { get; } //stickiness

        // Ongoing State (the physics engine updates this)
        ArenaBodyState State { get; }

        // Use a DataTemplate to apply a View to it.  This will
        // represent the item visually in the arena.
        ISprite Sprite { get; }

        // The arena calls this after every step of the simulation
        // so the object can respond to its new state.
        void OnUpdate();

        // The arena will call this method when this body
        // collides with another body in the arena.
        void OnCollision(IArenaBody otherBody);
    }
}
