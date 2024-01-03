using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;


public class PlayerNamdCard : MonoBehaviourPun
{
    private TMP_Text nameText;

    void Start()
    {
        nameText = GetComponentInChildren<TextMeshProUGUI>();
        nameText.text = photonView.Owner.NickName; //PhotonNetwork.NickName;
        if (photonView.IsMine)
        {
            nameText.color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {
        nameText.transform.rotation = Camera.main.transform.rotation;
    }
}