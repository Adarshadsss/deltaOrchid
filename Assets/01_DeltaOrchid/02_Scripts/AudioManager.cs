using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip flipSound;
    [SerializeField] AudioClip matchSound;
    [SerializeField] AudioClip mismatchSound;
    [SerializeField] AudioClip gameOverSound;

    void Awake()
    {
        Instance = this;
    }

    public void PlayFlip()
    {
        audioSource.PlayOneShot(flipSound);
    }

    public void PlayMatch()
    {
        audioSource.PlayOneShot(matchSound);
    }

    public void PlayMismatch()
    {
        audioSource.PlayOneShot(mismatchSound);
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOverSound);
    }
}