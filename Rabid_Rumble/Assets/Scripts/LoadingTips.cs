using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingTips : MonoBehaviour {

    private string[] loadingTips;
    [SerializeField] private TextAsset loadingTip;
    public Text textOutput;

    // Use this for initialization
    void Start()
    {
        string tip = loadingTip.text;
        loadingTips = splitStrings(tip);        

    }

    private string[] splitStrings(string stringToSplit)
    {
        string[] array = stringToSplit.Split('\n');
        return array;
    }

    public string GenerateTip()
    {
        string tips = null;
        tips = loadingTips[Random.Range(0, loadingTips.Length)];
        string tipOutput = tips;
        textOutput.text = tipOutput;
        return tipOutput;
    }
}
