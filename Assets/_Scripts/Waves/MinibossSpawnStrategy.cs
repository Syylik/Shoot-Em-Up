using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName="new MiniBossWaveStrategy", menuName="SO/SpawnStrategy/MiniBossWave")]
public class MinibossSpawnStrategy : ScriptableObject, ISpawnStrategy
{
    [SerializeField] private Enemy _miniBossPrefab;

    [SerializeField] private ClassicWaveStrategy _classicWaveStrategy;
    
    public IEnumerator Spawn(int num, EnemyCreator enemyCreator)
    {
        yield return _classicWaveStrategy.Spawn(num, enemyCreator);

        enemyCreator.CreateMiniBoss(_miniBossPrefab);
    }
}