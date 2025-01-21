using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefLoader : MonoBehaviour
{
    public List<MusicData> musicList;  // Drag and drop MusicData objects here
    public AudioSource audioSource;
    public Spawner spawner;

    private void Start()
    {
        int selectedSongIndex = PlayerPrefs.GetInt("SelectedSong", 1) - 1;
        if (selectedSongIndex >= 0 && selectedSongIndex < musicList.Count)
        {
            LoadMusic(selectedSongIndex);
            AssignMusicToSpawner(selectedSongIndex);
        }
        else
        {
            Debug.LogWarning("Selected song index out of range");
        }
    }

    void LoadMusic(int index)
    {
        MusicData selectedMusic = musicList[index];
        audioSource.clip = selectedMusic.musicClip;
    }

    void AssignMusicToSpawner(int index)
    {
        if (spawner != null && index >= 0 && index < musicList.Count)
        {
            spawner.musicData = musicList[index];
            Debug.Log("Music assigned to Spawner: " + musicList[index].musicClip.name);
        }
        else
        {
            Debug.LogWarning("Spawner or selected music is null");
        }
    }
}
