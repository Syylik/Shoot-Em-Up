using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName="new BossSpawnStrategy", menuName="SO/SpawnStrategy/BossWave")]
public class BossSpawnStrategy : ScriptableObject, ISpawnStrategy
{
    [SerializeField] private Enemy _bossPrefab;

    public IEnumerator Spawn(int num, EnemyCreator enemyCreator)
    {
        enemyCreator.CreateMiniBoss(_bossPrefab);
        yield return null;
    }
}
