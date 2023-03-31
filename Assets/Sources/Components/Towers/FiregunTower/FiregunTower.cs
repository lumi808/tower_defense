using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiregunTower : BaseTower, IUpgradable
{
    [SerializeField] private ParticleSystem _fireEffectLevel1;
    [SerializeField] private ParticleSystem _fireEffectLevel2;
    [SerializeField] private ParticleSystem _fireEffectLevel3;

    private ParticleSystem _currentFireEffects;

    private void Start()
    {
        SetCurrentFireEffect();
    }

    public void Upgrade()
    {
        Level++;
        UpdateStats();
        ChangeLevelModel();

        SetCurrentFireEffect();
    }

    private void SetCurrentFireEffect()
    {
        if (Level == 0)
            _currentFireEffects = _fireEffectLevel1;
        else if (Level == 1)
            _currentFireEffects = _fireEffectLevel2;
        else
            _currentFireEffects = _fireEffectLevel3;
    }

    private void Update()
    {
        if (AvailableEnemies.Count == 0 || !IsActive)
        {
            SetFireParticles(false);
            return;
        }

        BaseEnemy target = AvailableEnemies[0];

        Vector3 toEnemyVector = target.transform.position - transform.position;
        toEnemyVector.y = 0;
        _rotateElement.forward = toEnemyVector;

        Attack(target);
    }

    private void Attack(BaseEnemy target)
    {
        SetFireParticles(true);

        target.GetDamage(BaseDamage * Time.deltaTime);
    }

    private void SetFireParticles(bool isPlaying)
    {
        if (isPlaying)
        {
            if (!_currentFireEffects.isPlaying)
                _currentFireEffects.Play();
        }
        else
        {
            _currentFireEffects.Stop();
        }
    }
}
