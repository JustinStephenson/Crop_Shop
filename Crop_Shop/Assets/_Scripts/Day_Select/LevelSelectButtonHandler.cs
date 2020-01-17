using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButtonHandler : MonoBehaviour
{
    [SerializeField]
    private bool unlocked;
    [SerializeField]
    private int myNumber;

    private void Start()
    {
        unlocked = GameControl.control.LevelUnlock[myNumber - 1];
        if (!unlocked)
        {
            //change sprite to look like level is locked.
            GetComponent<Image>().color = new Color32(155, 155, 155, 255);
        }
    }

    public void Clicked()
    {
        if (unlocked)
        {
            GameControl.control.currentLevel = myNumber;
            SceneManager.LoadScene("Main");
        }
    }
}
