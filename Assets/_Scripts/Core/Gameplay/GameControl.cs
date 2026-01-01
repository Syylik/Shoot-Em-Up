using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameObject _loosePanel;
    [SerializeField] private GameObject _blackPanel;

    internal bool isLoosed = false;

    [SerializeField] private Bullet _bulletPrefab;
    public Pool<Bullet> bulletPool;

    public static GameControl Instance { get; private set; }

    public void Init()
    {
        if(Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        bulletPool = new Pool<Bullet>(_bulletPrefab, 32);
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