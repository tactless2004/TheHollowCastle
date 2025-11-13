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
* 2025/11/12 | Leyton McKinney | Destroy self, if distance traveled exceeds weapon range.
* 2025/11/12 | Leyton McKinney | Collision detection makes sure target is off correct tag, preventing "friendly fire".
*
************************************************************/
 
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private WeaponData weaponData;
    private Vector3 initialPosition;
    private bool launched = false;
    private string targetTag;

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
    public void Launch(Vector3 direction, float speed, WeaponData weaponData, string targetTag)
    {
        this.weaponData = weaponData;
        this.targetTag = targetTag;

        rb.linearVelocity = direction.normalized * speed;

        // projectiles might get stuck or behave weirdly and sometimes just need to die even if they hit nothing.
        Destroy(gameObject, 5f);

        // Set initial position, for travel checking
        initialPosition = transform.position;
        launched = true;
    }

    public void FixedUpdate()
    {
        // The projectile has a natural timeout, but if the projectile's travel exceeds the range of the Ranged Weapon,
        // then destroy the projectile.
        if (launched)
        {
            if (Vector3.Distance(transform.position, initialPosition) >= weaponData.range)
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(
            collision.collider.TryGetComponent(out CombatEntity target) && // hit entity is a CombatEntity
            collision.collider.CompareTag(targetTag) // hit entity is of the target type
        )
        {
            target.TakeDamage(weaponData);
        }

        Destroy(gameObject);
    }
}
