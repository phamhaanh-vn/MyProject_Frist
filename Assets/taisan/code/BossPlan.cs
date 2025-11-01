using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossPlan : MonoBehaviour
{
    public GameObject Dan;
    public GameObject DanTrum;
    public Transform FirePoint;
    private Animator an;
    private float count = 0;
    private float TimeWaitTurn = 2.5f;
    void Start()
    {
        an = GetComponent<Animator>();
    }
    void Update()
    {
        
    }
     public IEnumerator Shoot()
    {
        while (true)
        {
            count++;
            if (count == 1)
            {
                an.SetTrigger("Attack");
                yield return new WaitForSeconds(0.25f);
                ShootOne();
                yield return new WaitForSeconds(TimeWaitTurn);
            }
            else if (count == 2)
            {
                an.SetTrigger("Attack");
                yield return new WaitForSeconds(0.25f);
                StartCoroutine(ShootTwo(0.5f));
                yield return new WaitForSeconds(TimeWaitTurn);
            }
            else if (count == 3)
            {
                an.SetTrigger("Attack");
                yield return new WaitForSeconds(0.25f);
                ShootThree();
                yield return new WaitForSeconds(TimeWaitTurn);
            }
            if (count > 3)
            {
                count = 0;
            }
        }
    }
    private void ShootOne()
    {
        Vector3[] positions = new Vector3[3];
        positions[0] = FirePoint.position + new Vector3(0, 2.85f, 0);
        positions[1] = FirePoint.position;
        positions[2] = FirePoint.position + new Vector3(0, -2.85f, 0);
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Dan, positions[i], Quaternion.identity);
        }
    }
    private IEnumerator ShootTwo(float Delay)
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(Dan, FirePoint.position, Quaternion.identity);
            yield return new WaitForSeconds(Delay);
        }
    }
    private void ShootThree()
    {
        Instantiate(DanTrum, FirePoint.position, DanTrum.transform.rotation);
    }
 
}
