using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody; //donde guardaremos el componente rigidbody del player, para poder acceder a él
    private float ImpulseForce = 100f; //fuerza par impulsar al player al saltar
    private float limitsForce = 10f; //fuerza que empujará al player si intenta salir fuera de la pantalla.

    private float upLimit = 13.94f; //limite superior de la pantalla
    public bool gameOver = false; //el boolean que nos indicará si hemos perdido el juego

    //particulas que utilizaremos durante la partida
    public ParticleSystem coinParticleSystem;
    public ParticleSystem explotionParticleSystem;

    //Los componentes Audiosource que querremos acceder para reproducir los sonidos
    private AudioSource PlayerAudiosource;
    private AudioSource CamAudio;

    //clips que reproduciremos al realizar diferentes acciones en el juego
    public AudioClip money;
    public AudioClip bomb;
    public AudioClip salto;

    //counter
    private int counter;

    void Start()
    {
        counter = 0;

        playerRigidbody = GetComponent<Rigidbody>(); //guardamos la componente rigidbody de nuestro Player
        PlayerAudiosource = GetComponent<AudioSource>(); //audiosource del player
        CamAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>(); //audiosource de la camera

    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !gameOver) //al pulsar la tecla espacio y aun no haber perdido el juego chocando con una bomba o el suelo
        {
            playerRigidbody.AddForce(Vector3.up * ImpulseForce, ForceMode.Impulse); //se le aplica una fuerza hacia arriba en modo Impulse, para ser toda la fuerza de golpe y no repartida
            PlayerAudiosource.PlayOneShot(salto, 1f); //al saltar reproducimos el sonido de salto, 1 vez por salto.
        }

        if (transform.position.y >= upLimit) //si superas el limite de la pantalla
        {
            playerRigidbody.AddForce(Vector3.down * limitsForce, ForceMode.Impulse); //el juego te expulsa hacia abajo por chocarte con el limite
        }


    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("ground")) //al detectar que toca el suelo
        {
            Debug.Log("GAME OVER!!");
            gameOver = true; //perdemos el juego
            //Time.timeScale = 0; //el juego se acaba 
            CamAudio.Pause(); //se apaga la música también
            Debug.Log($"Score: {counter}");
        }

        if(otherCollider.gameObject.CompareTag("Bomb")) //al detectar que se choca con una bomba
        {
            
            Debug.Log("BOOM!");
            gameOver = true;
            PlayerAudiosource.PlayOneShot(bomb,1f); //se reproduce el sonido de explosión
            CamAudio.Pause(); //se para la música
  
            Destroy(otherCollider.gameObject); //se destruye la bomba
            ParticleSystem explotionPs = Instantiate(explotionParticleSystem, transform.position, transform.rotation);
            explotionPs.Play(); //aparecen particulas de explosion

        }

        if (otherCollider.gameObject.CompareTag("Money")) //al recolectar dinero
        {

            ParticleSystem CoinPs = Instantiate(coinParticleSystem, transform.position, transform.rotation);
            CoinPs.Play(); //aparecen particulas de explosion
            
            Debug.Log("+1 Coin"); //se indica que se ha recolectado 1 moneda
            counter++;

            PlayerAudiosource.PlayOneShot(money, 1f);
            Destroy(otherCollider.gameObject);
        }
    }
}
