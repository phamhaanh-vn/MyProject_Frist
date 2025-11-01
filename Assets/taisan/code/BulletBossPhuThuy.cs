using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletBossPhuThuy : MonoBehaviour
{
    public float Speed;
    private Vector2 dir;
    public float Gocquay;
    void Start()
    {
       
    }

    void Update()
    {
        
    }
    public IEnumerator Shoot(float TimeWaitShoot)
    {
        yield return new WaitForSeconds(TimeWaitShoot);
        // Hướng từ Đạn đến Player
        dir = ((Vector2)Moveplayer.Mo.transform.position - (Vector2)transform.position).normalized;
        // Tính toán góc xoay đến Player, mặc định Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg góc xoay sẽ theo trục x: 0 độ
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // Dùng để xoay theo lần lượt là Quaternion.Euler(x, y, z(angle - 90))
        // angle -90f: để xoay theo hướng đầu đạn mong muốn vì angle mặc định là 0 độ
        transform.rotation = Quaternion.Euler(0, 0, angle - Gocquay);
        // Vị trí đạn đến Player bằng cách += vị trí mới liên tục   
        while (true)
        {
            transform.position += (Vector3)(dir * Speed * Time.deltaTime);
            yield return null;// phải có cái này để chờ đến Frame tiếp theo không sẽ bị lag
        }
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("vatcan"))
        {
            Destroy(gameObject);
        }
    }
}
