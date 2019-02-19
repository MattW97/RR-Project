using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenChar : MonoBehaviour
{

    public GameObject p1ChosenCharacter;
    public GameObject p2ChosenCharacter;
    public GameObject p3ChosenCharacter;
    public GameObject p4ChosenCharacter;

    public Transform p1CharSpawn;
    public Transform p2CharSpawn;
    public Transform p3CharSpawn;
    public Transform p4CharSpawn;

    public int playerPickNum;
    public List<int> existingConNums;
    public List<GameObject> charOptions;
    public List<GameObject> currentSelectedChars;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public GameObject SelectedCharacter(int id)
    {
        if (id == 0)
        {
            return p1ChosenCharacter;
        }
        if (id == 1)
        {
            return p2ChosenCharacter;
        }
        if (id == 2)
        {
            return p3ChosenCharacter;
        }
        if (id == 3)
        {
            return p4ChosenCharacter;
        }
        else
        {
            return null;
        }
    }
}
