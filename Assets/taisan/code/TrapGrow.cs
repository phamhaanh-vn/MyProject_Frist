using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapGrow : MonoBehaviour
{
    public float TimeGrow;
    private float TimeElapsed = 0f;
    private BoxCollider2D box;
    private Vector3 TargetScale = new Vector3(1.35f, -13.7f, 1f);
    private Vector3 LocalscaleStart;
    private Vector2 Sizebox;
    public bool isGrow = false;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        LocalscaleStart = transform.localScale;
        Sizebox = box.size;
    }
    void Update()
    {

    }
    public IEnumerator Grow()
    {
        isGrow = true;
        Vector2 SizeboxStart = box.size;
        Vector2 TargetBox = new Vector2(Sizebox.x, TargetScale.y);
        while(TimeElapsed < TimeGrow)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / TimeGrow;
            transform.localScale = Vector3.Lerp(LocalscaleStart, TargetScale, t);
            box.size = Vector2.Lerp(SizeboxStart, TargetBox, t);
            yield return null;
        }
        transform.localScale = TargetScale;
        box.size = TargetBox;
    }
}
