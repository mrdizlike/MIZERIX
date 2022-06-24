using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Sprite[] ItemIcon;
    public Sprite[] ItemQualityIcon;
    public AudioClip[] Audio;  //0 = Отсутствие компонента
    public GameObject[] UIEffect; //0 = Отсутствие компонента
    public GameObject[] PlayerEffect; //0 = Отсутствие компонента
}
