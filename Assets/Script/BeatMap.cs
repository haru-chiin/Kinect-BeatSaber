using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBeatMap", menuName = "BeatMap")]
public class BeatMap : ScriptableObject
{
    [System.Serializable]
    public class Beat
    {
        public float time;
        public string type;
        public string spawnSide;
        public string direction;
    }

    /*    public List<Beat> beats;*/

    public List<Beat> beats;
}
