using UnityEngine;
using UnityEngine.UI;

public class PlayerFabStat : MonoBehaviour
{
    [Header("Counts")]
    public int StatBlockOneID;
    public int StatBlockTwoID;
    public int WinStat;
    public int DefeatStat;
    public int KillStat;
    public int DeathStat;
    public int ItemsStat;

    [Header("UI")]
    public Text StatBlockOne_Header;
    public Text StatBlockTwo_Header;
    public Text StatBlockOne_Count;
    public Text StatBlockTwo_Count;

    public void ChangeStatBlockOne(int StatID)
    {
        StatBlockOneID = StatID;

        if(StatID == 1)
        {
            StatBlockOne_Header.text = "Wins: ";
            StatBlockOne_Count.text = WinStat.ToString();
        }

        if(StatID == 2)
        {
            StatBlockOne_Header.text = "Defeats: ";
            StatBlockOne_Count.text = DefeatStat.ToString();
        }

        if(StatID == 3)
        {
            StatBlockOne_Header.text = "Kills: ";
            StatBlockOne_Count.text = KillStat.ToString();
        }

        if(StatID == 4)
        {
            StatBlockOne_Header.text = "Deaths: ";
            StatBlockOne_Count.text = DeathStat.ToString();
        }

        if(StatID == 5)
        {
            StatBlockOne_Header.text = "Items: ";
            StatBlockOne_Count.text = ItemsStat.ToString();
        }
    }

    public void ChangeStatBlockTwo(int StatID)
    {
        StatBlockTwoID = StatID;

        if(StatID == 1)
        {
            StatBlockTwo_Header.text = "Wins: ";
            StatBlockTwo_Count.text = WinStat.ToString();
        }

        if(StatID == 2)
        {
            StatBlockTwo_Header.text = "Defeats: ";
            StatBlockTwo_Count.text = DefeatStat.ToString();
        }

        if(StatID == 3)
        {
            StatBlockTwo_Header.text = "Kills: ";
            StatBlockTwo_Count.text = KillStat.ToString();
        }

        if(StatID == 4)
        {
            StatBlockTwo_Header.text = "Deaths: ";
            StatBlockTwo_Count.text = DeathStat.ToString();
        }

        if(StatID == 5)
        {
            StatBlockTwo_Header.text = "Items: ";
            StatBlockTwo_Count.text = ItemsStat.ToString();
        }
    }
}
