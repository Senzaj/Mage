using System;
using Sources.Modules.Common;

namespace Sources.Modules.Player.Scripts
{
    public class PlayerSaver : Saver<PlayerData>
    {
        public event Action<PlayerData> RequestSave;
        public static PlayerSaver Instance { get; private set; }

        public override void Init(PlayerData data)
        {
            if (Instance == null)
            {
                Instance = this;
                base.Init(data);
            }
        }
        
        public override PlayerData GetData()
        {
#if UNITY_EDITOR
            return null;
#endif
            
            return base.GetData();
        }

        public override void SaveData(PlayerData data)
        {
            base.SaveData(data);
            
            RequestSave?.Invoke(MyData);
        }
    }
}