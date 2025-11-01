using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Moveplayer;
    public GameObject Gate;
    private Vector3 follow = new Vector3(0.15075f, 2.23227f, -10f);
    public Transform Location_Boss_Plan;
    public Transform Location_Boss_Phu_Thuy;
    private Camera cam;
    public bool FollowBoss_Plan;
    public bool FollBoss_Phu_Thuy;
    public bool CloseGate;
    public bool Zoomcam = false;
    public float SpeedZoom;
    public float Doxa;
    public float t;
    void Start()
    {
        cam = Camera.main;
    }
    void LateUpdate()
    {
        ZoomToBoss_Plan();
        ZoomXa();
        ZoomToBoss_Phu_Thuy();
    }
    public void ZoomToBoss_Plan()
    {
        if(Location_Boss_Plan != null)
        {
            if (!FollowBoss_Plan)
            {
                transform.position = Moveplayer.transform.position + follow;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, Location_Boss_Plan.position, SpeedZoom * Time.deltaTime);
            }
        }
    }
    public void ZoomToBoss_Phu_Thuy()
    {
        if (Location_Boss_Phu_Thuy != null)
        {
            if (!CloseGate)
            {
                transform.position = Moveplayer.transform.position + follow;
            }
            // Chú ý khi dùng else if thì phải đặt điều kiện cụ thể hơn lên trên
            else if (CloseGate && FollBoss_Phu_Thuy)
            {
                transform.position = Vector3.Lerp(transform.position, Location_Boss_Phu_Thuy.position, SpeedZoom * Time.deltaTime);
            }
            else if (CloseGate)
            {
                Vector3 more = new Vector3(Gate.transform.position.x, Gate.transform.position.y, -10f);
                transform.position = Vector3.Lerp(transform.position, more, SpeedZoom * Time.deltaTime);
            }
        }
    }
    public void ZoomXa()
    {
        if (Zoomcam)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Doxa, t);// t càng nhỏ thì chuyển động càng từ từ 
        }
    }
}
