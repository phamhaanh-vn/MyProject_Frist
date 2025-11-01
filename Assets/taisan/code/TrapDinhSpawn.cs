using System.Collections;
using UnityEngine;

public class TrapDinhSpawn : MonoBehaviour
{
    public GameObject Dan;
    public Transform PointA;
    public Transform PointB;
    public float TimeDelay = 1f;   // khoảng cách giữa A và B
    public float CycleTime = 2f;   // chu kỳ bắn lại của A
    void Start()
    {
        StartCoroutine(TrapSpawn());
    }
    private IEnumerator TrapSpawn()
    {
        while (true)
        {
            // A bắn
            yield return new WaitForSeconds(1f);
            Shoot(PointA);
            yield return new WaitForSeconds(TimeDelay);
            // B bắn
            Shoot(PointB);
            yield return new WaitForSeconds(CycleTime - TimeDelay);
        }
    }
    private void Shoot(Transform point)
    {
        Instantiate(Dan, point.position, point.rotation);
    }
}
