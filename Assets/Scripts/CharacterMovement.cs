using Photon.Pun;
using UnityEngine;

public class CharacterMovement : MonoBehaviourPun
{
    CharacterController controller;
    [SerializeField] Camera mainCamera;

    [SerializeField] float pSpeed = 1;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform aimPoint;

    void Awake()
    {
        mainCamera = FindAnyObjectByType<Camera>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(photonView.IsMine)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");


            Vector3 dir = new Vector3(x, 0f, z);
            if(dir.magnitude > 0)
            {
                dir.Normalize();
                controller.Move(dir * pSpeed * Time.deltaTime);
            }

            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
            
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
    }
}
