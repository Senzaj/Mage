using System;
using Sources.Modules.Common;

namespace Sources.Modules.Training.Scripts
{
    public class TrainingSaver : Saver<TrainingData>
    {
        public event Action<TrainingData> RequestSave;
        public static TrainingSaver Instance { get; private set; }
        
        public override void Init(TrainingData data)
        {
            if (Instance == null)
            {
                Instance = this;
                base.Init(data);
            }
        }

        public override TrainingData GetData()
        {
#if UNITY_EDITOR
            return null;
#endif
            
            return base.GetData();
        }
        
        public override void SaveData(TrainingData data)
        {
            base.SaveData(data);
            
            RequestSave?.Invoke(MyData);
        }
    }
}