using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectAdd : MonoBehaviour
{
    public ScrollRect myScrollRect;
    public RectTransform contentPanel;
    public RectTransform[] childs;
    private RectTransform closestChild;

    private float distBetweenChildren;
    private bool draging = false;

    private void Start()
    {
        //find the distance between children and then divide by 2 to use to snap to the closet one to contentPanel
        distBetweenChildren = Mathf.Abs(childs[0].anchoredPosition.x - childs[1].anchoredPosition.x) / 2;
        closestChild = childs[0];
    }

    private void Update()
    {
        //Debug.Log(myScrollRect.viewport.anchoredPosition.x);
        //Debug.Log(Mathf.Abs(contentPanel.anchoredPosition.x + childs[3].anchoredPosition.x));
        if (draging)
        {
            float closestDist = Mathf.Abs(contentPanel.anchoredPosition.x + closestChild.anchoredPosition.x);
            for (int i = 0; i < childs.Length; i++)
            {
                float temp = Mathf.Abs(contentPanel.anchoredPosition.x + childs[i].anchoredPosition.x);
                if (temp < distBetweenChildren)
                {
                    closestChild = childs[i];
                }
            }
        }
        else
        {
            SnapTo(closestChild);
        }
    }

    private void SnapTo (RectTransform target)
    {
        Canvas.ForceUpdateCanvases();
        contentPanel.anchoredPosition = (Vector2)myScrollRect.transform.InverseTransformPoint(contentPanel.position) - (Vector2)myScrollRect.transform.InverseTransformPoint(target.position);
    }

    public void BeginDrag()
    {
        draging = true;
    }

    public void EndDrag()
    {
        draging = false;
    }
}
