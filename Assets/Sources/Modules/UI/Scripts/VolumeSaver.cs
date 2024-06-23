using System;
using Sources.Modules.Common;

namespace Sources.Modules.UI.Scripts
{
    public class VolumeSaver : Saver<VolumeData>
    {
        public event Action<VolumeData> RequestSave; 
        public static VolumeSaver Instance { get; private set; }

        public override void Init(VolumeData data)
        {
            if (Instance == null)
            {
                Instance = this;
                base.Init(data);
            }
        }
        
        public override VolumeData GetData()
        {
#if UNITY_EDITOR
            return null;
#endif

            return new VolumeData()
            {
                MusicEnable = MyData.MusicEnable,
                SoundEnable = MyData.SoundEnable,
                MusicVolume = MyData.MusicVolume,
                SoundVolume = MyData.SoundVolume,
                VolumeChanged = MyData.VolumeChanged
            };
        }

        public override void SaveData(VolumeData data)
        {
            MyData = new VolumeData()
            {
                MusicEnable = data.MusicEnable,
                SoundEnable = data.SoundEnable,
                MusicVolume = data.MusicVolume,
                SoundVolume = data.SoundVolume,
                VolumeChanged = data.VolumeChanged
            };

            RequestSave?.Invoke(MyData);
        }
    }
}
