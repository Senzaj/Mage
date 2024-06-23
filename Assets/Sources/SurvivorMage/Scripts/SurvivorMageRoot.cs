using Sources.Modules.EnemyFactory.Scripts;
using Sources.Modules.EnemyFactory.Scripts.Pool;
using Sources.Modules.Finder.Scripts;
using Sources.Modules.Particles.Scripts;
using Sources.Modules.Player.Scripts;
using Sources.Modules.Player.Scripts.MVP;
using Sources.Modules.Sound.Scripts;
using Sources.Modules.Wallet.Scripts.MVP;
using Sources.Modules.Weapons.Scripts;
using Sources.Modules.Weapons.Scripts.Base;
using Sources.Modules.Workshop.Scripts.UI;
using UnityEngine;

namespace Sources.SurvivorMage.Scripts
{
    [RequireComponent(typeof(FinderCloseEnemy))]
    internal class SurvivorMageRoot : MonoBehaviour
    {
        [SerializeField] private Mage _mage;
        [SerializeField] private Staff _staff;
        [SerializeField] private ProjectilesPool _projectilesPool;
        [SerializeField] private ParticleSpawner _particleSpawner;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PlayerSetup _playerSetup;
        [SerializeField] private WalletSetup _walletSetup;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private SpellsShop _spellsShop;
        [SerializeField] private PlayerSound _playerSound;
        [SerializeField] private SoundContainer _soundContainer;
        [SerializeField] private AudioSource _audioSourcePrefab;

        private FinderCloseEnemy _finderCloseEnemy;

        private void Awake()
        {
            _finderCloseEnemy = GetComponent<FinderCloseEnemy>();
            _playerSound.Init(_soundContainer, _audioSourcePrefab);
            _mage.Init(_playerSound);
            _finderCloseEnemy.Init(_mage);
            _enemyPool.Init(_particleSpawner);
            _enemySpawner.Init(_enemyPool);
            _projectilesPool.Init(_particleSpawner);
            _playerSetup.Init(_mage);
            _staff.Init(_finderCloseEnemy, _projectilesPool);
            _spellsShop.Init(_staff);
            _walletSetup.Init(_playerView, _spellsShop);
            
        }
    }
}
