using System;
using Sources.Modules.Player.Scripts;
using Sources.Modules.Training.Scripts;
using Sources.Modules.UI.Scripts;
using Sources.Modules.Wallet.Scripts;
using Sources.Modules.Wave.Scripts;
using Sources.Modules.Workshop.Scripts;

namespace Sources.Modules.Initialization
{
    [Serializable]
    public class AllDates
    {
        public WaveData Wave;
        public SpellSlotDates WorkShop;
        public WalletData Wallet;
        public TrainingData Training;
        public PlayerData Player;
        public VolumeData Volume;
    }
}