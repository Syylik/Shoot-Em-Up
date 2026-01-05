using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyCreator : MonoBehaviour
{
    public Transform bossSpawnPos;
    public List<Transform> spawnPoses;

    private Pool<Minion> _minionPool;

    private ISpawnStrategy _spawnStrategy;

    public void SetSpawnStrategy(ISpawnStrategy spawnStrategy) => _spawnStrategy = spawnStrategy;

    public void SpawnWave(int num) => StartCoroutine(_spawnStrategy.Spawn(num, this));

    public void CreateMinion(Enemy enemyPrefab, Transform afterSpawn)
    {
        if(_minionPool == null && enemyPrefab is Minion minionPrefab) _minionPool = new Pool<Minion>(minionPrefab, spawnPoses.Count);
        
        var pos = new Vector2
            (afterSpawn.position.x + Random.Range(-0.5f, 0.5f),
            afterSpawn.position.y + Random.Range(6.5f, 10f));
            
        var minion = _minionPool.Spawn(pos, enemyPrefab.transform.rotation);
        minion.Init(afterSpawn, _minionPool);
    }

    public void CreateMiniBoss(Enemy miniBossPrefab)
    {
        var spawnPos = (Vector2)bossSpawnPos.position + new Vector2(0f, 10f);
        var miniBoss = Instantiate(miniBossPrefab, spawnPos, miniBossPrefab.transform.rotation);
        miniBoss.Init(bossSpawnPos);
    }
}