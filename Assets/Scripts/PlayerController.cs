using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private float ImpulseForce = 100f;
    private float limitsForce = 10f;
    private float upLimit = 13.94f;
    private AudioSource PlayerAudiosource;
    private AudioSource bombAudio;
    private AudioSource CamAudio;
    public AudioClip money;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        PlayerAudiosource = GetComponent<AudioSource>();
        CamAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();

    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) //al pulsar la tecla espacio
        {
            playerRigidbody.AddForce(Vector3.up * ImpulseForce, ForceMode.Impulse); //se le aplica una fuerza hacia arriba
            PlayerAudiosource.Play();
        }

        if (transform.position.y >= upLimit) //si superas el limite de la pantalla
        {
            //transform.position = new Vector3(transform.position.x, upLimit, transform.position.z);
            playerRigidbody.AddForce(Vector3.down * limitsForce, ForceMode.Impulse); //el juego te expulsa hacia abajo por chocarte con el limite
        }


    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("ground")) //al detectar que toca el suelo
        {
            Debug.Log("GAME OVER!!");
            Time.timeScale = 0; //el juego se acaba 
            CamAudio.Pause(); //se apaga la música también
        }

        if(otherCollider.gameObject.CompareTag("Bomb")) //al detectar que se choca con una bomba
        {
            Debug.Log("BOOM! GAME OVER!!");
            Time.timeScale = 0; //perdemos el juego
            //Destroy(otherCollider.gameObject);
            otherCollider.gameObject.GetComponent<AudioSource>().Play();
            CamAudio.Pause(); //se para la música
        }

        if (otherCollider.gameObject.CompareTag("Money")) //al recolectar dinero
        {
            Debug.Log("+1 Coin"); //se indica que se ha recolectado 1 moneda
            AudioSource collect = otherCollider.gameObject.GetComponent<AudioSource>();


            Destroy(otherCollider.gameObject);
        }
    }
}
