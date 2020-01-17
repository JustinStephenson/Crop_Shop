using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Tool")]
public class Tool : ScriptableObject {

    public Sprite sprite;
    public ToolSet toolset;

    public float watercanAmountOfWater;
    public int weedcutterAmountOfCut;

    public enum ToolSet
    {
        plow,
        watercan,
        weedcutter,
        bugspray
    }

    public float GetWaterAmount()
    {
        if (toolset == ToolSet.watercan)
        {
            return watercanAmountOfWater;
        }
        return 0.0f;
    }

    public int GetCutAmount()
    {
        if (toolset == ToolSet.weedcutter)
        {
            return weedcutterAmountOfCut;
        }
        return 0;
    }

    public Sprite GetToolSprite()
    {
        return sprite;
    }
}
