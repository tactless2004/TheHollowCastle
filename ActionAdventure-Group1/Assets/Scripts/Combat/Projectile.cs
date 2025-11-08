/************************************************************
* COPYRIGHT:  Year
* PROJECT: Name of Project or Assignment
* FILE NAME: Projectile.cs
* DESCRIPTION: Short Description of script.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/11/04 | Leyton McKinney | Init
* 2025/11/07 | Leyton McKinney | Switch from AttackData to WeaponData system.
*
************************************************************/
 
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private WeaponData weaponData;
    private float speed;

    private void Awake()
    {
        if(TryGetComponent(out Rigidbody rb))
        {
            this.rb = rb;
        }

        else
        {
            Debug.LogError("Projectile does not have a Rigidbody.");
        }
    }
    public void Launch(Vector3 direction, float speed, WeaponData data)
    {
        this.weaponData = data;
        this.speed = speed;
        rb.linearVelocity = direction.normalized * speed;

        // projectiles might get stuck or behave weirdly and sometimes just need to die even if they hit nothing.
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out CombatEntity target))
        {
            target.TakeDamage(weaponData);
        }

        Destroy(gameObject);
    }
}
