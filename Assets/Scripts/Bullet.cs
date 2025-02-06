using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    public float bulletSpeed = 5f;
    public float lifeTime = 5f;

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
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
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
