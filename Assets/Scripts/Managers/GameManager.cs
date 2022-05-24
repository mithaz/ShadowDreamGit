using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public AudioClip levelAudio;
    private AudioSource audioSource;
    [SerializeField] private GameObject[] lifePickups;
    [SerializeField] private GameObject[] hurtPickups;
    [SerializeField] private GameObject[] hiddenWalls;

    [SerializeField] private GameObject[] riftPortals;
    public GameObject LevelExitDoor;
    public PlayerMovementController playerStatus;

    private bool playerState;

    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = levelAudio;
        audioSource.volume = 0.5f;
        audioSource.Play();
        playerState = playerStatus.IsLiving;
    }

    void Update()
    {
        playerState = playerStatus.IsLiving;

        lifePickups = GameObject.FindGameObjectsWithTag("Life");
        hurtPickups = GameObject.FindGameObjectsWithTag("Hurt");
        riftPortals = GameObject.FindGameObjectsWithTag("Rift");
        hiddenWalls = GameObject.FindGameObjectsWithTag("HiddenWall");


        if (playerState)
        {
            HideHiddenWalls();
            ShowExitDoor();
            HideLifeBox();
            ShowHurtBox();
            ShowRift();
        }
        else if(!playerState)
        {
            HideHiddenWalls();
            ShowExitDoor();
            ShowLifeBox();
            HideHurtBox();
            ShowRift();
        }
    }

    private void ShowExitDoor()
    {
        bool isDoorAlive = LevelExitDoor.gameObject.GetComponent<NextLevelDoor>().IsDoorAliveRealm;
        if(isDoorAlive && playerState)
        {
            LevelExitDoor.gameObject.GetComponent<MeshRenderer>().enabled = true;
            LevelExitDoor.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else if (!isDoorAlive && !playerState)
        {
            LevelExitDoor.gameObject.GetComponent<MeshRenderer>().enabled = true;
            LevelExitDoor.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            LevelExitDoor.gameObject.GetComponent<MeshRenderer>().enabled = false;
            LevelExitDoor.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void HideHiddenWalls()
    {
        bool isHiddenWhenAliveOrDeath;
        for(int i = 0; i < hiddenWalls.Length; i++)
        {
            isHiddenWhenAliveOrDeath = hiddenWalls[i].GetComponent<HiddenWallScript>().IsHiddenAlivePlane;

            if (isHiddenWhenAliveOrDeath && playerState)
            {
                hiddenWalls[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
                hiddenWalls[i].gameObject.GetComponent<MeshCollider>().enabled = false;
            }
            else if(!isHiddenWhenAliveOrDeath && !playerState)
            {
                hiddenWalls[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
                hiddenWalls[i].gameObject.GetComponent<MeshCollider>().enabled = false;
            }
            else
            {
                hiddenWalls[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
                hiddenWalls[i].gameObject.GetComponent<MeshCollider>().enabled = true;
            }
        }
    }

    private void ShowRift()
    {
        for(int i = 0; i < riftPortals.Length; i++)
        {
            riftPortals[i].gameObject.GetComponent<MeshRenderer>().enabled = !playerState;
            riftPortals[i].gameObject.GetComponent<BoxCollider>().enabled = !playerState;
        }
    }

    private void HideHurtBox()
    {
        for(int i = 0; i < hurtPickups.Length; i++)
        {
            hurtPickups[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
            hurtPickups[i].gameObject.GetComponent<BoxCollider>().enabled = false;
            hurtPickups[i].gameObject.GetComponentInChildren<Light>().enabled = false;
        }
    }

    private void ShowHurtBox()
    {
        for (int i = 0; i < hurtPickups.Length; i++)
        {
            hurtPickups[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
            hurtPickups[i].gameObject.GetComponent<BoxCollider>().enabled = true;
            hurtPickups[i].gameObject.GetComponentInChildren<Light>().enabled = true;
        }
    }

    private void HideLifeBox()
    {
        for (int i = 0; i < lifePickups.Length; i++)
        {
            lifePickups[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
            lifePickups[i].gameObject.GetComponent<BoxCollider>().enabled = false;
            lifePickups[i].gameObject.GetComponentInChildren<Light>().enabled = false;
        }
    }

    private void ShowLifeBox()
    {
        for (int i = 0; i < lifePickups.Length; i++)
        {
            lifePickups[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
            lifePickups[i].gameObject.GetComponent<BoxCollider>().enabled = true;
            lifePickups[i].gameObject.GetComponentInChildren<Light>().enabled = true;
        }
    }
}