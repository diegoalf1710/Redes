using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerShooting : MonoBehaviourPun
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform aimPoint;
    [SerializeField] float bSpeed;


    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Fire");
                photonView.RPC("Shoot", RpcTarget.All);
            }
        }
    }

    [PunRPC]

    void Shoot()
    {
        GameObject bullet = PhotonNetwork.Instantiate("Bullet", aimPoint.position, aimPoint.rotation);
        bullet.GetComponent<Bullet>().photonView.TransferOwnership(photonView.Owner);
        bullet.GetComponent<Bullet>().Initialize(bSpeed, photonView.Owner);
    }
}
