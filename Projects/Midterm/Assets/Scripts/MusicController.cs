using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] List<AudioData> sfxList;
    [SerializeField] AudioSource sfxPlayer;

    Dictionary<AudioId, AudioData> sfxLookup;

    public static MusicController i { get; private set; }

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        sfxLookup = sfxList.ToDictionary(x => x.id);
    }


    public void PlaySfx(AudioClip clip)
    {
        if (clip == null) return;

        sfxPlayer.PlayOneShot(clip);
    }

    public void PlaySfx(AudioId audioId)
    {
        if (sfxLookup.ContainsKey(audioId)) return;

        var audioData = sfxLookup[audioId];
        PlaySfx(audioData.clip);
    }
}

public enum AudioId { Dialogue, Grass, Ground }

[System.Serializable]

public class AudioData
{
    public AudioId id;
    public AudioClip clip;
}