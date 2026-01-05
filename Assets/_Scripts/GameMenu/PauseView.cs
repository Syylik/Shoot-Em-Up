using UnityEngine;

public class PauseView : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    private void Start() => InputSystem.Instance.input.UI.Pause.performed += context => TogglePause();

    public void TogglePause()
    {
        _pausePanel.SetActive(Pause.Instance.Toggle());
    }
}