using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void RandomizePitch(float pitchMin, float pitchMax) => _audioSource.pitch = Random.Range(pitchMin, pitchMax);

    public void SetVolume(float volume) => _audioSource.volume = volume;

    public void Play()
    {
        _audioSource.clip = _clips[Random.Range(0, _clips.Length)];
        _audioSource.Play();
    }

    public void PlayByIndex(int index)
    {
        _audioSource.clip = _clips[index];
        _audioSource.Play();
    }
}
