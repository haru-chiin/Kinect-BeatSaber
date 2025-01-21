using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "musik", menuName = "MusikList")]
public class MusicData : ScriptableObject
{
    public AudioClip musicClip;
    public float cooldownDelayUp;
    public float cooldownDelayDown;
    public TextAsset musicJson;
}
