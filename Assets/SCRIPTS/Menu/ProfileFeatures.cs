using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class ProfileFeatures : MonoBehaviour
{
    [Header("Scripts")]
    public Network NT;

    [Header("MainUIHandlers")]
    public Text UserName;
    public Text ProfileLevel_Text;
    public GameObject ProfileStat_One;
    public GameObject ProfileStat_Two;
    public GameObject FriendsList;
    public GameObject MatchList;
    public Image MenuUserAvatar;
    public Image ProfileUserAvatar;
    public Image NewRank;
    public Image PreviousRank;
    public Image ProfileLevel;

    [Header("AvatarsBase")]
    public int CurrentAvatarID;
    public Sprite[] PlayerAvatar;
    
    [Header("NewRankBase")]
    public int CurrentRankID;
    public float CurrentRankProgress;
    public float CurrentNextRankProgress;
    public Image CurrentRankProgressBar;
    public Sprite[] RankIMG;

    void Start()
    {
        MenuUserAvatar.sprite = ProfileUserAvatar.sprite;
    }

    public void ChangeAvatar(int AvatarID)
    {
        CurrentAvatarID = AvatarID;
        GetPlayerAvatar(AvatarID);
        NT.SaveAccountData();
    }

    public void GetPlayerAvatar(int AvatarID)
    {
        ProfileUserAvatar.sprite = PlayerAvatar[AvatarID];
        MenuUserAvatar.sprite = ProfileUserAvatar.sprite;
    }

    public void GetPlayerNewRank(int RankID)
    {
        //NewRank.sprite = RankIMG[RankID];
        CurrentRankProgressBar.fillAmount = CurrentRankProgress / CurrentNextRankProgress;
    }

}
