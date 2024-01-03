using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ConnManager : MonoBehaviourPunCallbacks
{
    public TMP_Text userNickName;

    void Start()
    {
        PhotonNetwork.GameVersion = "0.1"; // 게임 버전
        int num = UnityEngine.Random.Range(0, 1000); // 랜덤숫자
        PhotonNetwork.NickName = "Player" + num; // 플레이어 닉네임
        PhotonNetwork.AutomaticallySyncScene = true; // 자동으로 씬을 동기화 한다.
        PhotonNetwork.ConnectUsingSettings(); // 위의 파라미터들을 가지고 연결을 시도 함
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default); // 로비의 타입은 디폴드
    }

    public override void OnJoinedLobby() // 로비에 들어가면 실행됨
    {
        Debug.Log("OnJoindLobby"); // 로비에 들어갔다고 알려줌
        RoomOptions ro = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 8 }; // 방설정을 하게 됨
        PhotonNetwork.JoinOrCreateRoom("NetTest", ro, TypedLobby.Default);
        //NetTest라는 방이 있는지 보고 없으면 위의 룸설정을 받아오고 만든다.
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoindRoom");
        Vector2 origPos = UnityEngine.Random.insideUnitCircle * 2.0f;

        PhotonNetwork.Instantiate("PlayerArmature", new Vector3(origPos.x, 0, origPos.y), quaternion.identity);
        //이렇게 해야 만들어진다네요

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.Find("NickNameCanvas/Text").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName;
        }

    }
}
