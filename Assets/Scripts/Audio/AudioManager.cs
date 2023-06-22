using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] menuMusic, gameMusic, menuSound, gameSound;
    [SerializeField] private AudioSource musicSource, soundSource;
    private int index = 0;
    private int prevIndex = 0;

    private float leftTime = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (PlayerPrefs.GetInt("muteM") == 1) MuteMusic();
        if (PlayerPrefs.GetInt("muteS") == 1) MuteSound();

        StartNewAudio();
    }

    private void Update()
    {
        leftTime -= Time.deltaTime;

        if (leftTime > 0) return;

        StartNewAudio();
    }

    private void StartNewAudio()
    {
        prevIndex = index;
        RollIndex();

        musicSource.clip = menuMusic[index];
        leftTime = musicSource.clip.length;

        musicSource.Play();
    }

    private void RollIndex()
    {
        index = Random.Range(0, menuMusic.Length);

        if (menuMusic.Length > 1 && index == prevIndex) RollIndex();
    }

    public void MuteMusic()
    { 
        musicSource.mute = !musicSource.mute;
        PlayerPrefs.SetInt("muteM", musicSource.mute ? 1 : 0);
    }
    public void MuteSound()
    {
        soundSource.mute = !soundSource.mute;
        PlayerPrefs.SetInt("muteS", soundSource.mute ? 1 : 0);
    }
}
