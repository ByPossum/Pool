using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] float f_lifetime;
    [SerializeField] private int i_damage;
    public int Damage { set { i_damage = value; } }
    public void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(DeathTime(f_lifetime));
    }

    public void Die()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        PoolManager.x.ReturnObjectToPool(gameObject);
        StopAllCoroutines();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    private IEnumerator DeathTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Die();
    }
}
