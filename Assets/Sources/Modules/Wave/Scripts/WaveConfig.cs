using System.Collections.Generic;
using Sources.Modules.Enemy;

namespace Sources.Modules.Wave.Scripts
{
    public class WaveConfig
    {
        private List<EnemyType> _enemyTypes;

        public WaveConfig(List<EnemyType> enemyTypes)
        {
            _enemyTypes = new List<EnemyType>();
            
            AddEnemyTypes(enemyTypes);
        }

        public List<EnemyType> GetEnemyTypes()
        {
            return _enemyTypes.GetRange(0, _enemyTypes.Count);
        }
        
        public void AddEnemyTypes(List<EnemyType> enemyTypes)
        {
            _enemyTypes.AddRange(enemyTypes);
        }
    }
}