using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMatch : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
