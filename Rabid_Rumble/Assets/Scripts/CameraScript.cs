﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class CameraScript : MonoBehaviour {

    public Transform player1;
    public Transform player2;
    public Transform player3;
    public Transform player4;

    public GameObject midPoint;
    private GameObject playerPickerInfo;

    private Vector3 newCameraPos;
    private Vector3 middlePoint;
    private Vector3 vectorBetweenPlayers;
    private float distanceBetweenPlayers;
    private float maxCameraDistance;
    private float minCameraDistance;
    private float cameraYOffset;
    private float cameraZOffset;

    private float p1DistToMid;
    private float p2DistToMid;
    private float p3DistToMid;
    private float p4DistToMid;

    public GameObject playerSelection;

    private AudioSource audioSource;
    public AudioClip mainMenuMusic;
    public AudioClip combatMusic;
    public AudioClip victoryMusic;

    private void Awake()
    {
        playerPickerInfo = GameObject.Find("PlayerPickerInfo");
    }

    void Start()
    {
        newCameraPos = Camera.main.transform.position;
        maxCameraDistance = 35;
        minCameraDistance = 2;
        cameraYOffset = 0.0f;
        cameraZOffset = 0.0f;
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = combatMusic;
        audioSource.Play();
    }

    void LateUpdate()
    {
        p1DistToMid = Vector3.Distance(player1.position, midPoint.transform.position);
        p2DistToMid = Vector3.Distance(player2.position, midPoint.transform.position);
        p3DistToMid = Vector3.Distance(player3.position, midPoint.transform.position);
        p4DistToMid = Vector3.Distance(player4.position, midPoint.transform.position);


        switch (playerPickerInfo.GetComponent<ChosenChar>().existingConNums.Count)
        {
            case 1:
                //IF 1 PLAYER (Just uses same as 2 players for override)
                vectorBetweenPlayers = (player1.position / 2) + (player2.position / 2);
                distanceBetweenPlayers = (p1DistToMid + p2DistToMid) / 2;
                break;

            case 2:
                //IF 2 PLAYERS
                vectorBetweenPlayers = (player1.position / 2) + (player2.position / 2);
                distanceBetweenPlayers = (p1DistToMid + p2DistToMid) / 2;
                break;

            case 3:
                //IF 3 PLAYERS
                vectorBetweenPlayers = (player1.position / 3) + (player2.position / 3) + (player3.position / 3);
                distanceBetweenPlayers = (p1DistToMid + p2DistToMid + p3DistToMid) / 3;
                break;

            case 4:
                //IF 4 PLAYERS
                vectorBetweenPlayers = (player1.position / 4) + (player2.position / 4) + (player3.position / 4) + (player4.position / 4);
                distanceBetweenPlayers = (p1DistToMid + p2DistToMid + p3DistToMid + p4DistToMid) / 4;
                break;
        }

        middlePoint = vectorBetweenPlayers;

        newCameraPos.y = vectorBetweenPlayers.y + (midPoint.transform.position.y + ((distanceBetweenPlayers / maxCameraDistance) * (maxCameraDistance - minCameraDistance))) + minCameraDistance;

        newCameraPos.z = (midPoint.transform.position.z + (-1 * (newCameraPos.y * 1.0f)));

        Vector3 des = new Vector3(midPoint.transform.position.x, (newCameraPos.y + cameraYOffset), (newCameraPos.z + cameraZOffset));

        //Camera.main.transform.position = des;

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, des, Time.deltaTime * 2);
    }

    void FixedUpdate()
    {
        midPoint.transform.position = middlePoint + new Vector3(0, 1f, 0);
    }
}

