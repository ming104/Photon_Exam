using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
        //PhotonNetwork.SendRate = 30;
        //PhotonNetwork.SerializationRate = 30;
        PhotonNetwork.Instantiate("PlayerArmature", transform.position, transform.rotation);
        PhotonNetwork.AutomaticallySyncScene = true;
    }
}
