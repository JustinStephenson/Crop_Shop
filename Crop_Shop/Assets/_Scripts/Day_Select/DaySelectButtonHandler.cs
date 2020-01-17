using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DaySelectButtonHandler : MonoBehaviour
{
    Image myImage;
    Color32 originalColor;
    Color32 pushDownColor = new Color32(0xC8, 0xC8, 0xC8, 0xFF);

    private void Start()
    {
        myImage = GetComponent<Image>();
        originalColor = myImage.color;
    }

    public void PushDown()
    {
        myImage.color = pushDownColor;
    }

    public void MoveAway()
    {
        myImage.color = originalColor;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Back()
    {
        SceneManager.LoadScene("Adventure");
    }
}
