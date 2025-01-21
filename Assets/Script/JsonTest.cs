using System.Collections.Generic;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    public TextAsset jsonFile;

    private void Start()
    {
        if (jsonFile != null)
        {
            Debug.Log("JSON Content: " + jsonFile.text);

            try
            {
                BeatMapData beatMapData = JsonUtility.FromJson<BeatMapData>(jsonFile.text);
                Debug.Log("Loaded Beats Count: " + beatMapData.beats.Count);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to parse JSON: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("JSON File is null!");
        }
    }

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
}