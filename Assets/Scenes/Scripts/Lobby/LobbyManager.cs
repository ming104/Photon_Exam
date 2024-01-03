using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "0.1";
    public TMP_Text conn_info_Text;
    public Button JoinButton;
    public TMP_InputField nickNameinp;

    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion; // 게임 버전
        PhotonNetwork.NickName = "Player" + UnityEngine.Random.Range(0, 1000); // 플레이어 닉네임
        //PhotonNetwork.AutomaticallySyncScene = true; // 자동으로 씬을 동기화 한다.
        PhotonNetwork.ConnectUsingSettings(); // 위의 파라미터들을 가지고 연결을 시도 함
        JoinButton.interactable = false;
        conn_info_Text.text = "마스터 서버 연결 중...";
    }

    public override void OnConnectedToMaster()
    {
        JoinButton.interactable = true;
        //PhotonNetwork.JoinLobby(TypedLobby.Default); // 로비의 타입은 디폴드
        conn_info_Text.text = "연결됨!";
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        JoinButton.interactable = false;
        conn_info_Text.text = "연결되지 않음! 재 접속 시도중...";
        Debug.Log("Disconnected message = " + cause);
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect_Button()
    {
        JoinButton.interactable = false;
        if (PhotonNetwork.IsConnected)
        {
            conn_info_Text.text = "방에 접속 중...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            conn_info_Text.text = "연결되지 않음! 재 접속 시도중...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        conn_info_Text.text = "빈 방이 없음, 새로운 방 생성 중...";
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName, new RoomOptions { IsVisible = true, IsOpen = true, MaxPlayers = 8 });
    }

    // public override void OnJoinedLobby() // 로비에 들어가면 실행됨
    // {
    //     Debug.Log("OnJoindLobby"); // 로비에 들어갔다고 알려줌
    //     RoomOptions ro = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 8 }; // 방설정을 하게 됨
    //     PhotonNetwork.JoinOrCreateRoom("NetTest", ro, TypedLobby.Default);
    //     //NetTest라는 방이 있는지 보고 없으면 위의 룸설정을 받아오고 만든다.
    // }

    public override void OnJoinedRoom()
    {
        conn_info_Text.text = "방 참가 성공";
        PhotonNetwork.NickName = nickNameinp.text;
        PhotonNetwork.LoadLevel("Playground");
        //Vector2 origPos = UnityEngine.Random.insideUnitCircle * 2.0f;

        //PhotonNetwork.Instantiate("PlayerArmature", new Vector3(origPos.x, 0, origPos.y), quaternion.identity);
        //이렇게 해야 만들어진다네요

        // GameObject player = GameObject.FindWithTag("Player");
        // if (player != null)
        // {
        //     player.transform.Find("NickNameCanvas/Text").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName;
        // }

    }
}
