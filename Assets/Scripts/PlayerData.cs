using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public event EventHandler LevelUpEvent;

    private int level;
    [SerializeField] private float healthMax;
    private float health;
    [SerializeField] private float manaMax;
    private float mana;
    [SerializeField] private float progressMax;
    private float progress;
    [SerializeField] private float impactForce;
    private Vector3 respawnPoint;

    private void Awake()
    {
        Instance = this;
        respawnPoint = transform.position;
        health = healthMax;
        mana = manaMax;
    }


    private void Start()
    {
    }

    public void TakeDamage(int amountDamage)
    {
        DecreaseHealth(amountDamage);
        PlayerUI.Instance.SetHealth();
    }

    public float GetHeath()
    {
        return health;
    }
    public float GetHeathMax()
    {
        return healthMax;
    }

    public void AddHealth(int health)
    {
        if (this.health + health < healthMax)
        {
            this.health += health;
        }
        else
        {
            this.health = healthMax;
        }
    }

    public void DecreaseHealth(int health)
    {
        if (this.health - health > 0)
        {
            this.health -= health;
        }
        else
        {
            this.health = 0;
            Player.Instance.Die();
        }
    }
    public float GetMana()
    {
        return mana;
    }
    public float GetManaMax()
    {
        return manaMax;
    }

    public void AddMana(int mana)
    {
        if (this.mana + mana < manaMax)
        {
            this.mana += mana;
        }
        else
        {
            this.mana = manaMax;
        }
    }

    public void DecreaseMana(int mana)
    {
        if (this.mana - mana > 0)
        {
            this.mana -= mana;
        }
        else
        {
            this.mana = 0;

        }
    }

    public float GetProgress()
    {
        return progress;
    }

    public int GetLevel()
    {
        return level;
    }
    public float GetProgressMax()
    {
        return progressMax;
    }

    public void AddProgress(int progress)
    {
        if (this.progress + progress < progressMax)
        {
            this.progress += progress;
        }
        else
        {
            this.progress = progressMax;
            LevelUp();
        }
        PlayerUI.Instance.SetProgress();
    }

    public void LevelUp()
    {
        LevelUpEvent?.Invoke(this, EventArgs.Empty);
        level++;
        progressMax = (float)Math.Round(progressMax * (1.6));
        PlayerUI.Instance.SetLevel();
        
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
    }

    public float GetImpactForce()
    {
        return impactForce;
    }

}
