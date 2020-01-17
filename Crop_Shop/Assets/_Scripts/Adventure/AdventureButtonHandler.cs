using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdventureButtonHandler : MonoBehaviour
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

    public void Continue()
    {
        SceneManager.LoadScene("Main");
    }

    public void DaySelect()
    {
        SceneManager.LoadScene("Day_Select");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
