using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Array de prefabs que querremos spawnear, recolectables y obstáculos
    public GameObject[] objectprefabs; 

    private Vector3 spawnPosition;
    private int girar = 180;
    private int zero = 0;
    private int two = 2;

    //Limites donde queremos que spawnee el prefab
    private float uplim = 14.1f;
    private float lowlim = 1.5f;
    private float Xlim = -16.29f;
    private float zLim = 0f;

    //tiempo para que se spawnee el primer objeto
    private float initialtime = 2f;
    //tiempo entre cada spawn de un prefab
    private float repetitiontime = 2f;

    //comunicación con el script PlayerController
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); //localizamos el script playercontroller
        InvokeRepeating("SpawnPrefab", initialtime, repetitiontime); //invocamos a la función SpawnPrefab cada cierto tiempo automáticamente
    }
    public Vector3 RandomSpawnPosition() //función para generar una posición aleatoria dentro de los limites que hemos marcado
    {
        float randomY = Random.Range(lowlim, uplim);
        return new Vector3(Xlim, randomY, zLim);
    }

    public void SpawnPrefab() //función para spawnear un prefab aleatorio del array que hemos creado
    {
        if (!playerControllerScript.gameOver) //solo spawneará si no hemos perdido el juego
        {
            int orientacion = Random.Range(zero, two); //la decisión de si va a aparecer por la izquieda (0) o la derecha (1).
            int objecto = Random.Range(zero, objectprefabs.Length); //con esta linea se obtiene que elemento del array vamos a spawnear de forma aleatoria
            spawnPosition = RandomSpawnPosition(); //obtenemos nuestra posición random
            if (orientacion == zero)
            {
                Instantiate(objectprefabs[objecto], spawnPosition, objectprefabs[objecto].transform.rotation); //instanciamos un prefab aleatorio de la lista de prefabs, en la spawnposition.
            }
            else
            {
                spawnPosition.x = spawnPosition.x * -1;
                Quaternion prefabrotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + girar, transform.rotation.z);
                Instantiate(objectprefabs[objecto], spawnPosition, prefabrotation); //instanciamos un prefab aleatorio de la lista de prefabs, en la spawnposition.
            }

        }
    }

}
