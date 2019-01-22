using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUIPosition : MonoBehaviour {

    public Text mapName;
    public Image mapImage;
    [HideInInspector] public bool mapAssigned;
    [HideInInspector] public int voteNumber;
    public string sceneToLoad;

}
