using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Mini Scale Effect
/// </summary>
public class UGUIScaleEffect : MonoBehaviour
{
    public float scaleSpeedTime;
    public float maxScale;
    public float minScale;
    Vector3 maxVector3;
    Vector3 minVector3;
    // Start is called before the first frame update
    void Start()
    {
        maxVector3 = new Vector3(maxScale, maxScale, maxScale);
        minVector3 = new Vector3(minScale, minScale, minScale);
        ScaleMinStart();
    }
    private void ScaleMaxStart()
    {
        LeanTween.scale(gameObject, maxVector3, scaleSpeedTime).setEase(LeanTweenType.linear).setOnComplete(ScaleMinStart);
    }
    private void ScaleMinStart()
    {
        LeanTween.scale(gameObject, minVector3, scaleSpeedTime).setEase(LeanTweenType.linear).setOnComplete(ScaleMaxStart);
    }
}
