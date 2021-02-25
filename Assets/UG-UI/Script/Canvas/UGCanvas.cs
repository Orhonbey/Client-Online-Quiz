using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGCanvas : MonoBehaviour
{
    public bool isEditMode;
    public Canvas myCanvas;
    public static Vector2 canvasSize;
    private void Awake()
    {
        myCanvas = GetComponent<Canvas>();
        RectTransform r = myCanvas.GetComponent<RectTransform>();
        canvasSize = new Vector2(r.rect.width, r.rect.height);
    }
}
