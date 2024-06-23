using System.Collections.Generic;

namespace Sources.Modules.Enemy
{
    public class EnemyWaveConfig
    {
        private readonly List<EnemyType> _enemyTypes;
        public int SpawnCount { get; private set; }

        public EnemyWaveConfig(List<EnemyType> enemyTypes)
        {
            _enemyTypes = enemyTypes;
        }

        public void Init(int spawnCount)
        {
            SpawnCount = spawnCount;
        }

        public List<EnemyType> GetEnemyTypes() => _enemyTypes.GetRange(0, _enemyTypes.Count);
    }
}