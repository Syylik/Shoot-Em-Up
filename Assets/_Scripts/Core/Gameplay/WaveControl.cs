using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveControl : MonoBehaviour
{
    private int wave;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private int _waveSpawnCount;
    [SerializeField] private float _waveRateTime;
    private int enemyCount;

    public void AddEnemyCount() => enemyCount++;     // On Enemy Spawn

    public void ReduceEnemyCount() => enemyCount--;  // On Enemy Death

    public void StartWaves(EnemyCreator enemyCreator)
    {
        enemyCreator.Init(AddEnemyCount, ReduceEnemyCount);
        StartCoroutine(SpawnWaves(enemyCreator));
    }

    private IEnumerator SpawnWaves(EnemyCreator enemyCreator)
    {
        while(Pause.Instance.isPaused) yield return null;

        wave++;
        _waveText.text = wave.ToString();
        var spawnCount = Random.Range(_waveSpawnCount - 1, _waveSpawnCount + 1);
        if(spawnCount <= 0) spawnCount = 1;
        enemyCreator.SpawnEnemies(spawnCount);

        while(enemyCount > 0) yield return null;

        int chance = Random.Range(0, 100);
        if(chance <= 15) _waveSpawnCount += 2;
        else if(chance <= 85) _waveSpawnCount++;
        
        yield return new WaitForSeconds(Random.Range(_waveRateTime - 0.5f, _waveRateTime + 0.4f));
        StartCoroutine(SpawnWaves(enemyCreator));
    }
}
