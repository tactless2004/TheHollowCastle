/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: Projectile.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2000/01/01 | Your Name | Created class
*
*
************************************************************/
 
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private AttackData attackData;
    private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction, float speed, AttackData data)
    {
        this.attackData = data;
        this.speed = speed;
        rb.linearVelocity = direction.normalized * speed;

        // projectiles might get stuck or behave weirdly and sometimes just need to die even if they hit nothing.
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(attackData);
        }

        Destroy(gameObject);
    }
}
