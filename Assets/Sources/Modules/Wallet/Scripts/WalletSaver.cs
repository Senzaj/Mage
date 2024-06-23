using System;
using Sources.Modules.Common;

namespace Sources.Modules.Wallet.Scripts
{
    public class WalletSaver : Saver<WalletData>
    {
        public event Action<WalletData> RequestSave;
        public static WalletSaver Instance { get; private set; }
        
        public override void Init(WalletData data)
        {
            if (Instance == null)
            {
                Instance = this;
                base.Init(data);
            }
        }
        
        public override WalletData GetData()
        {
#if UNITY_EDITOR
            return null;
#endif
            
            return base.GetData();
        }
        
        public override void SaveData(WalletData data)
        {
            base.SaveData(data);
            
            RequestSave?.Invoke(MyData);
        }
    }
}