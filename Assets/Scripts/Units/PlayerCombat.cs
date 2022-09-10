using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField]
    private Weapon weapon;

    public float power;

    private Animator animator;
    private Animator weaponAnimation;

    public List<IDamagable> damagables;

    private Collider colider;

    public void Atack()
    {
        animator.SetTrigger("Atack");
        weapon.PlaySwingAudio();
        damagables.Clear();  
    }

    public void Hit()
    {
        weapon.GetComponent<Collider>().enabled = true;
    }

    public void ActivateWeapon()
    {
        //weapon.gameObject.SetActive(true);
        weaponAnimation.SetTrigger("On");
    }

    public void DesactivateWeapon()
    {
        //weapon.gameObject.SetActive(false);
        weaponAnimation.SetTrigger("Off");
        weapon.GetComponent<Collider>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //DesactivateWeapon();
        animator = GetComponent<Animator>();
        weaponAnimation = weapon.GetComponent<Animator>();

        damagables = new List<IDamagable>();
    }

}
