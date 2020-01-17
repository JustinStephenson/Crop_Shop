using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
