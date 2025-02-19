using UnityEngine;
using TMPro; // Añade esta línea para usar TextMeshPro
using Photon.Pun;

public class PlayerNameDisplay : MonoBehaviourPun
{
    public GameObject nameLabelPrefab; // Prefab del nombre de usuario (TextMeshPro)
    private GameObject nameLabel; // Instancia del nombre de usuario

    void Start()
    {
        if (photonView.IsMine)
        {
            // Solo el dueño del PhotonView instancia el nombre de usuario
            nameLabel = Instantiate(nameLabelPrefab, Vector3.zero, Quaternion.identity);
            nameLabel.transform.SetParent(GameObject.Find("Canvas").transform, false);

            // Asignar el nombre de usuario al texto
            TextMeshProUGUI nameText = nameLabel.GetComponent<TextMeshProUGUI>();
            nameText.text = photonView.Owner.NickName;

            // Asignar un color diferente para el jugador local
            nameText.color = Color.green; // Puedes cambiar el color según tus preferencias
        }
        else
        {
            // Para otros jugadores, instanciar el nombre de usuario
            nameLabel = Instantiate(nameLabelPrefab, Vector3.zero, Quaternion.identity);
            nameLabel.transform.SetParent(GameObject.Find("Canvas").transform, false);

            // Asignar el nombre de usuario al texto
            TextMeshProUGUI nameText = nameLabel.GetComponent<TextMeshProUGUI>();
            nameText.text = photonView.Owner.NickName;
        }
    }

    void Update()
    {
        if (nameLabel != null)
        {
            // Actualizar la posición del nombre de usuario en la pantalla
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f); // Ajusta la altura del texto
            nameLabel.transform.position = screenPosition;
        }
    }

    void OnDestroy()
    {
        // Destruir el nombre de usuario cuando el jugador se destruye
        if (nameLabel != null)
        {
            Destroy(nameLabel);
        }
    }
}