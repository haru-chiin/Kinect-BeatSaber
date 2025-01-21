using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BeatMapData : MonoBehaviour
{
    public class Beat
    {
        public float time;
        public string type;
        public string spawnSide;
        public string direction;
    }
}
