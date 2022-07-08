using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Player_MAIN : MonoBehaviourPun, IPunObservable
{

    [Header("Scripts")]
    private PhotonView PV;
    public ConnectionSystem CS;
    public CharacterController controller;
    private Zipline ZL;
    public AudioSource MainAux;
    public AudioClip ZipLine_Sound;
    public AudioListener AUX;
    public KillFeed KF;

    [Header("Objects")]
    public GameObject LightBaseProtection;
    public GameObject DarkBaseProtection;
    public GameObject Hater;
    public GameObject ShopUI;
    public GameObject SpawnPoint;
    public GameObject ChatPanel;
    public GameObject ChatContent;
    public Text Use_Text;

    [Header("Values")]
    private Vector3 playerVelocity;
    private bool IsGrounded;
    public bool ChatOpen;
    public Skill.PlayerClass PlayerClassification;
    public bool CanZIP;
    public bool InShop;
    public bool SafeZone;
    public bool DisableMovement;
    public bool BossZone;
    public float MovementSpeed = 5f;
    public float PreviousMovement;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public Vector3 movementVector;

    [Header("NetworkMisc")]
    public string PlayFab_Nickname;

    private void Start()
    {
        controller = GetComponent<CharacterController>(); //����� ����������
        PV = GetComponent<PhotonView>(); //����� �����
        ZL = GetComponent<Zipline>(); //����� ������ ���������� �� ��������
        KF = FindObjectOfType<KillFeed>(); //����� ������ ���������� �� �������
        PreviousMovement = MovementSpeed;

        if (PV.IsMine)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GetComponent<Look>().cam.GetComponent<Camera>().enabled = true;
            GetComponent<Look>().cam.GetComponent<AudioListener>().enabled = true;
            GetComponent<Look>().GunCam.GetComponent<Camera>().enabled = true;
            GetComponent<InputManager>().enabled = true;
            AUX.enabled = true;
        }
    }

    private void Update()
    {
        IsGrounded = controller.isGrounded;
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CS.photonView.RPC("CountOfPlayer", RpcTarget.All, 1);
                PhotonNetwork.Disconnect();
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.Return) && !ChatOpen)
            {
                ChatPanel.SetActive(true);
                ChatContent.SetActive(true);
                ChatOpen = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                InShop = true;
            }

            if(!ChatPanel.activeSelf && ChatOpen)
            {
                InShop = false;
                ChatOpen = false;
            }

            if (gameObject.tag == "LightTeam")
            {
                LightBaseProtection.SetActive(false);
                DarkBaseProtection.SetActive(true);
            }

            if (gameObject.tag == "DarkTeam")
            {
                LightBaseProtection.SetActive(true);
                DarkBaseProtection.SetActive(false);
            }

            if (ChatPanel.activeSelf)
            {
                GetComponent<Look>().enabled = false;
                GetComponent<GunScript>().enabled = false;
            } else
            {
                GetComponent<Look>().enabled = true;
                GetComponent<GunScript>().enabled = true;
            }
        }
    }

    public void Move(Vector2 input)
    {
        if (!ZL.Ziplined)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            movementVector = moveDirection;
            if (!DisableMovement && !ChatOpen)
            {
                controller.Move(transform.TransformDirection(moveDirection) * MovementSpeed * Time.deltaTime);
            }
            playerVelocity.y += gravity * Time.deltaTime;

            if (IsGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }
            controller.Move(playerVelocity * Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.F) && CanZIP && photonView.IsMine)
        {
            GetComponent<Zipline>().StartCoroutine(GetComponent<Zipline>().StartZipLine());
            CanZIP = false;
            MainAux.PlayOneShot(ZipLine_Sound);
        }
    }

    public void Jump()
    {
        if (IsGrounded && !GetComponent<PlayerSTAT>().Dead)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);        
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "DarkBase")
        {
            SafeZone = true;
            GetComponent<PlayerSTAT>().SafeZone.gameObject.SetActive(true);
        }

        if(other.tag == "Shop")
        {
            if (PV.IsMine)
            {
                Use_Text.text = "Press 'f' to open shop";
                Use_Text.gameObject.SetActive(true);
                if (Input.GetKey(KeyCode.F))
                {
                    ShopUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    InShop = true;
                }
            }
        }

        if (other.tag == "LightBase")
        {
            SafeZone = true;
            GetComponent<PlayerSTAT>().SafeZone.gameObject.SetActive(true);
        }

        if(other.tag == "BossGround")
        {
            BossZone = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DamageGround")
        {
            Hater = FindObjectOfType<DarkBossSys>().gameObject;
            GetComponent<PlayerSTAT>().Debuffs.Add(BuffList.PoisonDeBuff);
            GetComponent<PlayerSTAT>().EnableDeBuff = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DarkBase")
        {
            SafeZone = false;
            GetComponent<PlayerSTAT>().SafeZone.gameObject.SetActive(false);
        }

        if(other.tag == "Shop")
        {
            if (PV.IsMine)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Use_Text.gameObject.SetActive(false);
                ShopUI.SetActive(false);
                InShop = false;
            }
        }

        if (other.tag == "LightBase")
        {
            SafeZone = false;
            GetComponent<PlayerSTAT>().SafeZone.gameObject.SetActive(false);
        }

        if(other.tag == "BossGround")
        {
            BossZone = false;
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(SafeZone);
        }
        else if (stream.IsReading)
        {
            SafeZone = (bool)stream.ReceiveNext();
        }
    }
}
