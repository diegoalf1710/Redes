using Photon.Pun;
using UnityEditor.Rendering;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    public float speed = 5f;
    public float lifeTime = 5f;

    private Photon.Realtime.Player owner;

    public void Initialize (float bulletSpeed, Photon.Realtime.Player bulletOwner)
    {
        speed = bulletSpeed;
        
    }

    void Start()
    {
        //Destuir el projectil despues de un tiempo
        if(photonView.IsMine)
        {
            Destroy(gameObject, lifeTime);
        }
    }

    void Update()
    {
        //Mover el objeto
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(photonView.IsMine && other.CompareTag("Player"))
        {
            //Aqui puedes añadir logica para dañar el jugador
            Debug.Log("Jugador dañado");
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
