using System;
using Sources.Modules.Common;

namespace Sources.Modules.Wave.Scripts
{
    public class WaveSaver : Saver<WaveData>
    {
        public event Action<WaveData> RequestSave; 
        public static WaveSaver Instance { get; private set; }
        
        public override void Init(WaveData waveData)
        {
            if (Instance == null)
            {
                Instance = this;
                base.Init(waveData);
            }
        }

        public override WaveData GetData()
        {
#if UNITY_EDITOR
            return null;
#endif
            
            return base.GetData();
        }
        
        public override void SaveData(WaveData data)
        {
            base.SaveData(data);
            
            RequestSave?.Invoke(MyData);
        }
    }
}