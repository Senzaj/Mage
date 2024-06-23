using System.Collections.Generic;
using Sources.Modules.Finder.Scripts;
using Sources.Modules.Wave.Scripts;
using Sources.Modules.Wave.Scripts.UI;
using Sources.Modules.Weapons.Scripts.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Scripts.Base
{
    public class Staff : MonoBehaviour
    {
        [SerializeField] private List<SpellCaster> _spellCasterPrefabs;
        [SerializeField] private ShootPoint _shootPoint;
        [SerializeField] private WaveGenerator _waveGenerator;
        [SerializeField] private WaveStartWaveUI _waveStartWaveUI;

        public int ActiveSpellsCount => _activeSpellCasters.Count;
        
        private List<SpellCaster> _spellCasters;
        private List<SpellCaster> _activeSpellCasters;
        private FinderCloseEnemy _finder;
        private ProjectilesPool _projectilesPool;

        private void OnEnable()
        {
            _waveStartWaveUI.NextWaveButtonPressed += StartShooting;
            _waveGenerator.WaveEnded += StopShooting;
        }

        private void OnDisable()
        {
            _waveStartWaveUI.NextWaveButtonPressed -= StartShooting;
            _waveGenerator.WaveEnded -= StopShooting;
        }

        public void Init(FinderCloseEnemy finderCloseEnemy, ProjectilesPool projectilesPool)
        {
            _spellCasters = new List<SpellCaster>();
            _activeSpellCasters = new List<SpellCaster>();
            
            _projectilesPool = projectilesPool;
            _finder = finderCloseEnemy;
            
            foreach (SpellCaster prefab in _spellCasterPrefabs)
            {
                SpellCaster spawned = Instantiate(prefab, transform.position, Quaternion.identity, transform);
                spawned.Init(_shootPoint, _finder, _projectilesPool);
                _spellCasters.Add(spawned);
            }
        }

        public void AddSpellCaster(SpellType spellType)
        {
            foreach (SpellCaster caster in _spellCasters)
            {
                if (_activeSpellCasters.Contains(caster) == false && caster.SpellType == spellType)
                {
                    _activeSpellCasters.Add(caster);
                    break;
                }
            }
        }

        public void RemoveSpellCaster(SpellType spellType)
        {
            foreach (SpellCaster caster in _activeSpellCasters)
            {
                if (caster.SpellType == spellType)
                {
                    _activeSpellCasters.Remove(caster);
                    break;
                }
            }
        }

        private void StartShooting()
        {
            foreach (SpellCaster spellCaster in _activeSpellCasters)
                spellCaster.StartCasting();
        }

        private void StopShooting()
        {
            foreach (SpellCaster spellCaster in _activeSpellCasters)
                spellCaster.StopCasting();
        }
    }
}
