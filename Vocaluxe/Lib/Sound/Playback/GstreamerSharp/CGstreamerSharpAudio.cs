﻿#region license
// This file is part of Vocaluxe.
// 
// Vocaluxe is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Vocaluxe is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Vocaluxe. If not, see <http://www.gnu.org/licenses/>.
#endregion

using Gst;

namespace Vocaluxe.Lib.Sound.Playback.GstreamerSharp
{
    public class CGstreamerSharpAudio : CPlaybackBase
    {
        public override bool Init()
        {
            if (_Initialized)
                return false;
#if ARCH_X86
            const string path = ".\\x86\\gstreamer";
#endif
#if ARCH_X64
            const string path = ".\\x64\\gstreamer";
#endif
            //SetDllDirectory(path);
            Application.Init();
            Registry reg = Registry.Get();
            reg.ScanPath(path);

            _Initialized = Application.IsInitialized;
            return _Initialized;
        }

        public override void Close()
        {
            if (!_Initialized)
                return;
            base.Close();
            Application.Deinit();
        }

        protected override IAudioStream _CreateStream(int id, string media, bool loop)
        {
            return new CGstreamerSharpAudioStream(id, media, loop);
        }
    }
}