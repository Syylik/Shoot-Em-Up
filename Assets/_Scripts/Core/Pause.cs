using UnityEngine;

public class Pause
{
    internal bool isPaused { get; private set; }

    private static Pause _instance;
    public static Pause Instance
    {
        get
        {
            if(_instance == null) _instance = new Pause();
            return _instance;
        }
    }

    public bool TogglePause()
    {
        if(GameControl.Instance == null || GameControl.Instance.isLoosed) return false;

        isPaused = !isPaused;

        if(isPaused) InputSystem.Instance.SwitchInputMap("UI");
        if(!isPaused) InputSystem.Instance.SwitchInputMap("Game");

        GameControl.Instance.SetBlackPanel(isPaused);
        return isPaused;
    }
}