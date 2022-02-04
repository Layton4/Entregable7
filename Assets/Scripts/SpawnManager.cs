using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectprefabs;
    private Vector3 spawnPosition;
    private float uplim = 16f;
    private float lowlim = 0.8f;
    private float Xlim = -16.29f;
    private float initialtime = 2f;
    private float repetitiontime = 3f;
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); //localizamos el script playercontroller
        InvokeRepeating("SpawnPrefab", initialtime, repetitiontime);
    }
    public Vector3 RandomSpawnPosition()
    {
        float randomY = Random.Range(lowlim, uplim);
        return new Vector3(Xlim, randomY, 0f);

    }

    public void SpawnPrefab()
    {
        if (!playerControllerScript.gameOver)
        {
            int objecto = Random.Range(0, objectprefabs.Length);
            spawnPosition = RandomSpawnPosition();
            Instantiate(objectprefabs[objecto], spawnPosition, objectprefabs[objecto].transform.rotation);
        }
    }

}
