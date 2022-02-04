using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private float ImpulseForce = 100f;
    private float limitsForce = 10f;

    private float upLimit = 13.94f;
    public bool gameOver = false;

    public ParticleSystem coinParticleSystem;
    public ParticleSystem explotionParticleSystem;

    private Vector3 offset = new Vector3(0, -1.5f, 0);

    private AudioSource PlayerAudiosource;
    private AudioSource CamAudio;

    public AudioClip money;
    public AudioClip bomb;
    public AudioClip salto;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        PlayerAudiosource = GetComponent<AudioSource>(); //audiosource del player
        CamAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>(); //audiosource de la camera

    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !gameOver) //al pulsar la tecla espacio
        {
            playerRigidbody.AddForce(Vector3.up * ImpulseForce, ForceMode.Impulse); //se le aplica una fuerza hacia arriba
            PlayerAudiosource.PlayOneShot(salto, 1f);
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
            gameOver = true;
            Time.timeScale = 0; //el juego se acaba 
            CamAudio.Pause(); //se apaga la música también
        }

        if(otherCollider.gameObject.CompareTag("Bomb")) //al detectar que se choca con una bomba
        {
            
            Debug.Log("BOOM! GAME OVER!!");
            gameOver = true;
            PlayerAudiosource.PlayOneShot(bomb,1f);
            CamAudio.Pause(); //se para la música
  
            Destroy(otherCollider.gameObject); //se destruye la bomba
            ParticleSystem explotionPs = Instantiate(explotionParticleSystem, transform.position + offset, transform.rotation);
            explotionPs.Play(); //aparecen particulas de explosion
        }

        if (otherCollider.gameObject.CompareTag("Money")) //al recolectar dinero
        {

            ParticleSystem CoinPs = Instantiate(coinParticleSystem, transform.position, transform.rotation);
            CoinPs.Play(); //aparecen particulas de explosion

            Debug.Log("+1 Coin"); //se indica que se ha recolectado 1 moneda
            //otherCollider.gameObject.GetComponent<AudioSource>().PlayOneShot(money, 1f);
            PlayerAudiosource.PlayOneShot(money, 1f);
            Destroy(otherCollider.gameObject);
        }
    }
}
