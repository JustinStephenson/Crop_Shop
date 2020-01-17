using UnityEngine;

public class TrashType : MonoBehaviour {

    public void Interact(Tool tool, Crop crop, PlayerInteractions player)
    {
        player.SetCrop(null);
        player.SetGrownOrDead(false);
    }
}
