// 
// Copyright (c) 2004-2006 Jaroslaw Kowalski <jaak@jkowalski.net>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

#if !NETCF

using System;
using System.Runtime.InteropServices;

using NLog.Config;
using NLog.Conditions;

namespace NLog.Win32.Targets
{
    /// <summary>
    /// The row-highlighting condition.
    /// </summary>
    public class ConsoleRowHighlightingRule
    {
        private ConditionExpression _condition = null;
        private ConsoleOutputColor _backgroundColor = ConsoleOutputColor.NoChange;
        private ConsoleOutputColor _foregroundColor = ConsoleOutputColor.NoChange;

        /// <summary>
        /// Default highlighting rule. Doesn't change the color.
        /// </summary>
        public static readonly ConsoleRowHighlightingRule Default = new ConsoleRowHighlightingRule(null, ConsoleOutputColor.NoChange, ConsoleOutputColor.NoChange);
        
        /// <summary>
        /// The condition that must be met in order to set the specified foreground and background color.
        /// </summary>
        [AcceptsCondition]
        [RequiredParameter]
        public string Condition
        {
            get 
            { 
                if (_condition == null)
                    return null;
                else
                    return _condition.ToString();
            }
            set 
            { 
                if (value != null)
                    _condition = ConditionParser.ParseExpression(value);
                else
                    _condition = null;
            }
        }

        /// <summary>
        /// The foreground color.
        /// </summary>
        [System.ComponentModel.DefaultValue("NoChange")]
        public ConsoleOutputColor ForegroundColor
        {
            get { return _foregroundColor; }
            set { _foregroundColor = value; }
        }

        /// <summary>
        /// The background color.
        /// </summary>
        [System.ComponentModel.DefaultValue("NoChange")]
        public ConsoleOutputColor BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        /// <summary>
        /// Creates a new instance of <see cref="ConsoleRowHighlightingRule"/>
        /// </summary>
        public ConsoleRowHighlightingRule()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="ConsoleRowHighlightingRule"/> and
        /// assigns Condition, ForegroundColor and BackgroundColor properties.
        /// </summary>
        public ConsoleRowHighlightingRule(string condition, ConsoleOutputColor foregroundColor, ConsoleOutputColor backgroundColor)
        {
            Condition = condition;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        /// <summary>
        /// Checks whether the specified log event matches the condition (if any)
        /// </summary>
        /// <param name="logEvent">log event</param>
        /// <returns><see langword="true"/> if the condition is not defined or 
        /// if it matches, <see langword="false"/> otherwise</returns>
        public bool CheckCondition(LogEventInfo logEvent)
        {
            if (_condition == null)
                return true;
            return true.Equals(_condition.Evaluate(logEvent));
        }
    }
}

#endif