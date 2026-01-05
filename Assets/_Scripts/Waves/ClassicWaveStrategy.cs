using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName="new ClassicWaveStrategy", menuName="SO/SpawnStrategy/ClassicWave")]
public class ClassicWaveStrategy : ScriptableObject, ISpawnStrategy
{
    [SerializeField] private Minion _minionPrefab;
    [SerializeField] private float _timeBtwSpawn = 0.25f;

    public IEnumerator Spawn(int num, EnemyCreator enemyCreator)
    {
        var spawnPoses = enemyCreator.spawnPoses.GetRange(0, enemyCreator.spawnPoses.Count);
        if(num > spawnPoses.Count) num = spawnPoses.Count;
        for(int i = 0; i < num; i++)
        {
            var spawn = spawnPoses[Random.Range(0, spawnPoses.Count)];
            enemyCreator.CreateMinion(_minionPrefab, spawn);
            spawnPoses.Remove(spawn);
            yield return new WaitForSeconds(_timeBtwSpawn);
        }
    }
}