using System.Collections;
using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameObject _loosePanel;

    [SerializeField] private EnemyCreator _enemyCreator;

    [SerializeField] private GameObject _blackPanel;

    internal int wave;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private int _waveSpawnCount;
    [SerializeField] private float _waveRateTime;

    internal int enemyCount;

    internal bool isLoosed = false;

    [SerializeField] private Bullet _bulletPrefab;
    public Pool<Bullet> bulletPool;

    public static GameControl Instance { get; private set; }

    public void Awake()
    {
        if(Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        bulletPool = new Pool<Bullet>(_bulletPrefab, 32);
    }

    private void Start() => StartCoroutine(SpawnWaves());

    public void AddEnemyCount() => enemyCount++;     // On Enemy Spawn

    public void ReduceEnemyCount()
    {
        enemyCount--;  // On Enemy Death
        Debug.Log(enemyCount);
    } 

    private IEnumerator SpawnWaves()
    {
        while(Pause.Instance.isPaused) yield return null;

        wave++;
        _waveText.text = wave.ToString();
        var spawnCount = Random.Range(_waveSpawnCount - 1, _waveSpawnCount + 1);
        if(spawnCount <= 0) spawnCount = 1;
        _enemyCreator.SpawnEnemies(spawnCount);

        while(enemyCount > 0) yield return null;

        int chance = Random.Range(0, 100);
        if(chance <= 15) _waveSpawnCount += 2;
        else if(chance <= 85) _waveSpawnCount++;
        // else if(chance <= 15 && _waveSpawnCount > 2) _waveSpawnCount -= 2;
        
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