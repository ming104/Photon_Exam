using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public static ChatManager instance = null;

    private PhotonView PV;
    public TMP_Text chat;
    public TMP_InputField inpt; // 내가 입력한 내용
    public Scrollbar scrollbar;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
    }

    [PunRPC]
    void ChangeText(string textRPC)
    {
        chat.text = textRPC;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !inpt.isFocused)
        {
            inpt.ActivateInputField();
        }
    }

    public void CallRPC()
    {
        if (!string.IsNullOrEmpty(inpt.text))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PV = player.GetComponent<PhotonView>();
            Debug.Log("Nick name = " + PV.Owner.NickName);

            string msg = "\n" + "<color=lightblue>" + PV.Owner.NickName + " : " + "</color>" + inpt.text;
            photonView.RPC("ChangeText", RpcTarget.All, chat.text + msg);
            inpt.text = null;
            scrollbar.value = 0;
            inpt.DeactivateInputField();
        }
    }
}
