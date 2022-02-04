using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Right : MonoBehaviour
{
    private int movespeed = 5; //velocidad a la que se desplazar� a la derecha
    private float xLim = 16.5f; //el limite de la pantalla apartir del cual desaparecer� el gameobject

    //comunicaci�n con el script PlayerController
    private PlayerController playerControllerScript;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>(); //localizamos el script playercontroller
    }

    void Update()
    {
        if (!playerControllerScript.gameOver) //los objetos se mover�n solo si no aun no hemos perdido
        {
            transform.Translate(Vector3.right * Time.deltaTime * movespeed); //el gameobject se trasladar� a la derecha a una velocidad de movespeed autom�ticamente
            if (transform.position.x > xLim || transform.position.x < -xLim) //al salir de la pantalla
            {
                Destroy(gameObject); //se destruye el gameobject
            }
        }
    }
}
