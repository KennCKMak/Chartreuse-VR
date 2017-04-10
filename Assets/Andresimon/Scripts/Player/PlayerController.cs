using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void DeadEventHandler();

public class PlayerController : MonoBehaviour 
{
    #region Fields

  //  public bool TakingDamage { get; set;}

    [SerializeField] protected Stat healthStat;

    public bool IsDead
    {
        get
        {
            if ( healthStat.CurrentVal <= 0 ) OnDead();

            return healthStat.CurrentVal <= 0;
        }
    }

    #endregion

    public event DeadEventHandler Dead;

    public void OnDead()
    {
        if ( Dead != null )
        {
            Dead();
        }
    }

	public virtual void Start ()
    {
        healthStat.Initialize();
	}
	
    public void Death()
    {
        if ( IsDead )
        {
            FindObjectOfType<LifeManager>().TakeLife();
        }

        healthStat.CurrentVal = healthStat.MaxVal; 
    }

    public void TakeDamage(int damage)
    {
        healthStat.CurrentVal -= damage;

        if ( IsDead )
            Death();
    }

    public void heal(int amount)
    {
        healthStat.CurrentVal += amount;
    }

    void Update()
    {
		if ( Input.GetKey(KeyCode.KeypadPlus) || Input.GetKey(KeyCode.Alpha0))
        {
            heal(10);     
        }
		else if ( Input.GetKey(KeyCode.KeypadMinus) || Input.GetKey(KeyCode.Alpha9))
        {
            TakeDamage(10);

        }
    }
}
