using System;

namespace Sources.Modules.UI.Scripts
{
    [Serializable]
    public class VolumeData
    {
        public float SoundVolume;
        public float MusicVolume;
        public bool SoundEnable;
        public bool MusicEnable;
        public bool VolumeChanged;
    }
}
