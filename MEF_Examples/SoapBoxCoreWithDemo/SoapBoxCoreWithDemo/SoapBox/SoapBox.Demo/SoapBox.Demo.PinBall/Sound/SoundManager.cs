using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace SoapBox.Demo.PinBall
{
    public sealed class SoundManager
    {
        #region " SINGLETON DESIGN PATTERN "
        //this design pattern pulled from here: http://www.yoda.arachsys.com/csharp/singleton.html
        SoundManager()
        {
        }

        public static SoundManager Instance
        {
            get
            {
                return Nested.instance;
            }
        }
        
        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly SoundManager instance = new SoundManager();
        }
        #endregion

        public static void PlayWav(Stream wav)
        {
            if (Properties.Settings.Default.Sound)
            {
                ThreadPool.UnsafeQueueUserWorkItem(ignoredState =>
                {
                    System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer(wav);
                    soundPlayer.Play();
                }, null);
            }
        }
    }
}
