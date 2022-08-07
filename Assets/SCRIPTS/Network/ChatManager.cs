using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class ChatManager : MonoBehaviour
{
    public ConnectionSystem CS;

    public InputField ChatInput;
    public TextMeshProUGUI ChatContent;
    private PhotonView Photon;
    private List<string> messages = new List<string>();
    private float buildDelay = 0f;
    private int maxMessages = 14;
    public string PlayerNickname;

    private void Start()
    {
        Photon = GetComponent<PhotonView>();

        GetAccountInfo();
    }

    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            ChatContent.maxVisibleLines = maxMessages;

            if (messages.Count > maxMessages)
            {
                messages.RemoveAt(0);
            }
            if(buildDelay < Time.time)
            {
                BuildChatContents();
                buildDelay = Time.time + 0.25f;
            }

            if (CS.ChatPanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SubmitChat();
                    CS.ChatPanel.SetActive(false);
                }
            }
        }

        else if(messages.Count > 0)
        {
            messages.Clear();
            ChatContent.text = "";
        }
    }

    void DisableChatContent()
    {
        ChatContent.gameObject.SetActive(false);
    }

    [PunRPC]
    void RPC_NewMSG(string msg)
    {
        ChatContent.gameObject.SetActive(true);
        Invoke("DisableChatContent", 5f);
        messages.Add(msg);
    }

    public void SendChat(string msg)
    {
        string NewMessage = PlayerNickname + ": " + msg;
        CS.ChatPanel.SetActive(false);
        ChatContent.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Photon.RPC("RPC_NewMSG", RpcTarget.All, NewMessage);
    }

    public void SubmitChat()
    {
        string blankCheck = ChatInput.text;
        if(blankCheck == "")
        {
            CS.ChatPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return;
        }

        SendChat(ChatInput.text);
        ChatInput.ActivateInputField();
        ChatInput.text = "";
    }

    private void GetAccountInfo()
    {
        var request = new GetAccountInfoRequest{
            
        };
        PlayFabClientAPI.GetAccountInfo(request, TakePlayFabUsername, OnError);
    }

    private void TakePlayFabUsername(GetAccountInfoResult result)
    {
        PlayerNickname = result.AccountInfo.Username;
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    void BuildChatContents()
    {
        string NewContents = "";
        foreach(string s in messages)
        {
            NewContents += s + "\n";
        }
        ChatContent.text = NewContents;
    }
}
