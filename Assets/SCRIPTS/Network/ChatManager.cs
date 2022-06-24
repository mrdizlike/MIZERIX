using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public ConnectionSystem CS;

    public InputField ChatInput;
    public TextMeshProUGUI ChatContent;
    private PhotonView Photon;
    private List<string> messages = new List<string>();
    private float buildDelay = 0f;
    private int maxMessages = 14;

    private void Start()
    {
        Photon = GetComponent<PhotonView>();

        PhotonNetwork.NickName = "Player"; //Заменить на ник из PlayFab
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
        string NewMessage = PhotonNetwork.NickName + ": " + msg;
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
