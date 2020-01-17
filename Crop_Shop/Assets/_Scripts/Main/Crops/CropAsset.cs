using UnityEngine;

[CreateAssetMenu(fileName = "New Crop", menuName = "Crop")]
public class CropAsset : ScriptableObject {

    public int ID;

    public Sprite seedSprite;
    public Sprite growing1Sprite;
    public Sprite growing2Sprite;
    public Sprite growing3Sprite;
    public Sprite grownSprite;
    public Sprite deadSprite;

    public float growthSpeed = 5f;
    public float dehydrateSpeed = 5f;
    public float weedGrowthSpeed = 5f;
}
