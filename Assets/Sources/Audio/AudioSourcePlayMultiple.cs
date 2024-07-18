using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourcePlayMultiple : MonoBehaviour
{
    public AudioSource Source => _source;

    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private PlayBehaviour _behaviour;

    private AudioSource _source;
    private int _lastPlayed;

    public void PlayOneShot()
    {
        var next = GetNextPlayIndex();
        _source.PlayOneShot(_clips[next]);
        _lastPlayed = next;
    }

    private int GetNextPlayIndex()
    {
        var clipsCount = _clips.Length;
        switch (_behaviour)
        {
            case PlayBehaviour.Sequence:
                return (_lastPlayed + 1) % clipsCount;
            case PlayBehaviour.Random:
                return Random.Range(0, clipsCount);
            case PlayBehaviour.RandomNoRepeat:
                var random = Random.Range(0, clipsCount - 1);
                return random >= _lastPlayed ? random + 1 : random;
            default:
                return 0;
        }

    }

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _lastPlayed = _clips.Length;
    }

    public enum PlayBehaviour { Sequence, Random, RandomNoRepeat }
}
