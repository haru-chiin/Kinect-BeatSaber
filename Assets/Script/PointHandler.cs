using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointHandler : MonoBehaviour
{
    public static PointHandler instance;
    private void Awake()
    {
        instance = this;
    }
    
    public TextMeshProUGUI textMeshProUGUI;
    public int point;


    // Update is called once per frame
    void Update()
    {
        textMeshProUGUI.text = "score : " + point ;        
    }
}
