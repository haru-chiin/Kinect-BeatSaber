using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class BeatMapData
    {
        public List<Beat> beats;
    }

    [System.Serializable]
    public class Beat
    {
        public float time;
        public string type;
        public string spawnSide;
        public string direction;
    }

    public class BeatMapSementara 
    {
        public List <Beat> beatSementara;
    }

    public BeatMap beatMap;
    public GameObject redPrefabs;
    public GameObject bluePrefabs;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public AudioSource music;
    public TextAsset jsonFile;
    public List<Beat> beatRill;
    public MusicData musicData;
    public StartGame data;
    public float mTime;
    public float rTime;
    public float elapsedTime;
    private int currentBeatIndex = 0;

    private void Start()
    {
        musicData = data.songSelect;
        jsonFile = musicData.musicJson;
        if (jsonFile != null)
        {
            LoadBeatMapFromJson(jsonFile);
        }
        music.clip = musicData.musicClip;
        music.PlayDelayed(musicData.cooldownDelayUp);
        StartCoroutine(TimerCoroutine());
    }

    private void LoadBeatMapFromJson(TextAsset jsonFile)
    {
        try
        {
            Debug.Log("JSON Content: " + jsonFile.text);

            BeatMapData beatMapData = JsonUtility.FromJson<BeatMapData>(jsonFile.text);
            if (beatMap == null)
            {
                beatMap = ScriptableObject.CreateInstance<BeatMap>();
            }
            beatRill = beatMapData.beats;
            Debug.Log(beatMapData.beats[0].direction);

            
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to load BeatMap from JSON: " + ex.Message);
        }
    }

    private void Update()
    {
        if (!music.isPlaying && elapsedTime > 0)
        {
            SceneManager.LoadScene(0);  // Pindah ke scene index 0 saat lagu berakhir
        }


        Beat currentBeat = beatRill[currentBeatIndex];
        rTime = Time.time;
        mTime = music.time;
        
        if (elapsedTime >= currentBeat.time)
        {
            if (currentBeat.time  > 0) 
            {
                Debug.Log(currentBeat.time);
                SpawnBeat(currentBeat);
                currentBeatIndex++;
            }

        }
    }

    IEnumerator TimerCoroutine()
    {
        while (true)  // Loop yang terus berjalan
        {
            elapsedTime += Time.deltaTime;  // Tambahkan waktu setiap frame
            Debug.Log("Elapsed Time: " + elapsedTime + " seconds");
            yield return null;  // Tunggu satu frame dan lanjutkan
        }
    }
    private void SpawnBeat(Beat beat)
    {
        GameObject prefabToSpawn = null;
        Transform spawnPoint = null;
        float randPos = 0;
        switch (beat.type.ToLowerInvariant())
        {
            case "red":
                prefabToSpawn = redPrefabs;
                break;
            case "blue":
                prefabToSpawn = bluePrefabs;
                break;
            default:
                Debug.LogWarning("Unknown beat type: " + beat.type);
                return;
        }

        switch (beat.spawnSide.ToLowerInvariant())
        {
            case "left":
                spawnPoint = leftSpawnPoint;
                randPos = Random.Range(-0.2f,0);
                break;
            case "right":
                spawnPoint = rightSpawnPoint;
                randPos = Random.Range(0,0.2f);
                break;
            default:
                Debug.LogWarning("Unknown direction: " + beat.direction);
                return;
        }

        GameObject spawnedBeat = Instantiate(prefabToSpawn, new Vector3(spawnPoint.position.x + randPos,spawnPoint.position.y + Random.Range(-0.2f,0.2f),spawnPoint.position.z), Quaternion.identity);

        BeatMovement beatMovement = spawnedBeat.AddComponent<BeatMovement>();
        beatMovement.SetRotation(beat.direction);
        beatMovement.beatType = beat.type.ToLowerInvariant();
    }
}
