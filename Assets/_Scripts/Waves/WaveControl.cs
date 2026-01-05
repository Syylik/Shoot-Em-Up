using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveControl : MonoBehaviour
{
    [SerializeField] private ClassicWaveStrategy _classicWaveStrategy;
    [SerializeField] private MinibossSpawnStrategy _minibossSpawnStrategy;
    [SerializeField] private BossSpawnStrategy _bossSpawnStrategy;

    private int wave = 1;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private int _waveSpawnCount = 2;
    [SerializeField, Min(0.5f)] private float _waveRateTime = 0.5f;

    public void StartWaves(EnemyCreator enemyCreator)
    {
        wave = 1;
        StartCoroutine(SpawnWaves(enemyCreator));
    } 

    private IEnumerator SpawnWaves(EnemyCreator enemyCreator)
    {
        while(Pause.Instance.isPaused) yield return null;

        SelectStrategyByWave(enemyCreator);

        enemyCreator.SpawnWave(_waveSpawnCount);

        while(Registry<Enemy>.Count > 0) yield return null;

        int chance = Random.Range(0, 100);
        if(chance <= 20) _waveSpawnCount += 2;
        else _waveSpawnCount++;
        
        yield return new WaitForSeconds(Random.Range(_waveRateTime - 0.42f, _waveRateTime + 0.42f));
        
        wave++;
        _waveText.text = wave.ToString();

        StartCoroutine(SpawnWaves(enemyCreator));
    }

    private void SelectStrategyByWave(EnemyCreator enemyCreator)
    {
        enemyCreator.SetSpawnStrategy(_classicWaveStrategy);
        if(wave % 5 == 0) enemyCreator.SetSpawnStrategy(_minibossSpawnStrategy);
        if(wave % 10 == 0) enemyCreator.SetSpawnStrategy(_bossSpawnStrategy);
    }
}