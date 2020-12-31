using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int i_maxDamage;
    [SerializeField] int i_minDamage;
    [SerializeField] float f_fireTime;
    [SerializeField] float f_forceScailar;
    [SerializeField] GameObject go_bulletRef;
    [SerializeField] Transform t_shootPoint;
    bool b_fired;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0 && !b_fired)
        {
            GameObject bull = PoolManager.x.SpawnObject(go_bulletRef);
            bull.transform.position = t_shootPoint.position;
            bull.GetComponent<Bullet>().Damage = Random.Range(i_minDamage, i_maxDamage);
            bull.GetComponent<Rigidbody>().AddForce(transform.forward * f_forceScailar, ForceMode.Impulse);
            b_fired = true;
            StartCoroutine(FireDelay(f_fireTime));
        }
    }

    private IEnumerator FireDelay(float fireTime)
    {
        yield return new WaitForSeconds(fireTime);
        b_fired = false;
    }
}
