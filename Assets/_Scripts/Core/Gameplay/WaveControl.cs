using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveControl : MonoBehaviour
{
    private int wave;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private int _waveSpawnCount = 2;
    [SerializeField, Min(0.5f)] private float _waveRateTime = 0.5f;

    public void StartWaves(EnemyCreator enemyCreator) => StartCoroutine(SpawnWaves(enemyCreator));

    private IEnumerator SpawnWaves(EnemyCreator enemyCreator)
    {
        while(Pause.Instance.isPaused) yield return null;

        wave++;
        _waveText.text = wave.ToString();
        
        enemyCreator.SpawnEnemies(_waveSpawnCount);

        while(Registry<Enemy>.Count > 0) yield return null;

        int chance = Random.Range(0, 100);
        if(chance <= 20) _waveSpawnCount += 2;
        else _waveSpawnCount++;
        
        yield return new WaitForSeconds(Random.Range(_waveRateTime - 0.42f, _waveRateTime + 0.42f));
        StartCoroutine(SpawnWaves(enemyCreator));
    }
}
