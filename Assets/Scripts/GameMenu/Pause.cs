using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    internal bool isPaused { get; private set; }

    public static Pause Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start() => InputSystem.input.UI.Pause.performed += context => SetPause();

    public void SetPause()
    {
        if(GameControl.Instance == null) return;

        if(GameControl.Instance.isLoosed) return;
        isPaused = !isPaused;

        if(isPaused) InputSystem.Instance.SwitchInputMap("UI");
        if(!isPaused) InputSystem.Instance.SwitchInputMap("Game");

        GameControl.Instance.SetBlackPanel(isPaused);
        _pausePanel.SetActive(isPaused);
    }
    
    private void OnDestroy() { if(Instance == this) Instance = null; }
}