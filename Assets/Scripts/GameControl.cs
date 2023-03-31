using System.Collections;
using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameObject _loosePanel;

    [SerializeField] private EnemyCreator _enemyCreator;

    [SerializeField] private GameObject _blackPanel;

    internal int wave;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private int _waveSpawnCount;
    [SerializeField] private float _waveRateTime;

    internal int enemySpawned;
    internal int enemyLeft;

    internal bool isLoosed = false;

    public static GameControl Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start() => StartCoroutine(SpawnWaves());

    public void SetEnemyCount(int spawned)
    {
        enemySpawned = spawned;
        enemyLeft = spawned;
    }

    public void ReduceEnemyLeft() => enemyLeft--;

    private IEnumerator SpawnWaves()
    {
        wave++;
        _waveText.text = wave.ToString();
        var spawnCount = Random.Range(_waveSpawnCount - 1, _waveSpawnCount + 1);
        if(spawnCount <= 0) spawnCount = 1;
        _enemyCreator.SpawnEnemies(spawnCount);

        while(enemyLeft > 0) yield return null;

        int chance = Random.Range(0, 100);
        if(chance <= 45) _waveSpawnCount++;
        
        yield return new WaitForSeconds(Random.Range(_waveRateTime - 0.5f, _waveRateTime + 0.4f));
        StartCoroutine(SpawnWaves());
    }

    public void Loose()
    {
        isLoosed = true;
        Time.timeScale = 0f;
        SetBlackPanel(true);
        _loosePanel.SetActive(true);
    }

    public void SetBlackPanel(bool state) => _blackPanel.SetActive(state);

    private void OnDestroy() { if(Instance == this) Instance = null; }
}
