using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapGrow : MonoBehaviour
{
    public float TimeGrow;
    private BoxCollider2D box;
    private Vector3 TargetScale = new Vector3(1.35f, -13.7f, 1f);
    public Vector3 LocalscaleStart;
    public bool isGrow = false;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        LocalscaleStart = transform.localScale;
    }
    void Update()
    {

    }
    public IEnumerator Grow()
    {
        float TimeElapsed = 0f;
        isGrow = true;
        while(TimeElapsed < TimeGrow)   
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / TimeGrow;
            transform.localScale = Vector3.Lerp(LocalscaleStart, TargetScale, t);
            yield return null;
        }
        transform.localScale = TargetScale;
    }
}
