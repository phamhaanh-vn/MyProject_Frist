using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootXoay : MonoBehaviour
{
    public float SpeedXoay;
    public float Speed;
    public bool kiemtra;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (kiemtra == false)
        {
            transform.Translate(-Vector3.right * Speed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.World);
        }
        transform.Rotate(0, 0, 360 * SpeedXoay * Time.deltaTime);
    }
}
