using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] int roomSize = 4;
    // Start is called before the first frame update
    void Start()
    {
        //Conectar al servidor de PUN
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        //Unirse a una sala aleatoria
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No se encontro una sala, creando una nueva...");
        // Crear una nueva sala si no hay ninguna disponible
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions{MaxPlayers = roomSize});
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Unirse a una sala");
        // Instanciar el jugador en la escena
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }
}
