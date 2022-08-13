using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class ListingPrefab : MonoBehaviour
{
    public ProfileFeatures PF;
    public Network NT;

    public Text PlayerUsername;
    public string PlayerID;
    public Image AvatarHolder;
    public int AvatarID;

    void Start()
    {
        PF.GetPlayerAvatar(AvatarHolder, AvatarID);
    }

    public void DeleteMe()
    {
        NT.RemoveFriend(PlayerID);
        Destroy(gameObject);
        NT.GetFriends();
    }

    public void OnFriendDataRecieved(GetUserDataResult result)
    {
        AvatarID = int.Parse(result.Data["Avatar"].Value);
    }
}
