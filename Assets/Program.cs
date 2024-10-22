using JetBrains.Annotations;
using System;
using System.Diagnostics;

public class HealthSystem
{
    // Variables
    public int health;
    public int maxhealth = 100;
    public string healthStatus;
    public int shield;
    public int maxshield = 50;
    public int lives;
    public int maxlives = 99;

    // Optional XP system variables
    public int xp;
    public int level;

    public HealthSystem()
    {
        ResetGame();
    }

    public string ShowHUD()
    {
        // Implement HUD display
        UpdateHealthStatus();
        return $"Health: {healthStatus}, Shield: {shield}/{maxshield} , Lives {lives}, XP {xp} Level {level} ";
    }

    public void TakeDamage(int damage)
    {
        damage = Math.Max(0, damage);
        health -= damage;
        // Implement damage logic
        if (shield > 0)
        {
            int shieldDamage = Math.Min(shield, damage);
            shield -= shieldDamage;
            damage -= shieldDamage;
        
        }
        
        if (health <= 0)
        {
            health = 0;
            lives--;
            if (lives > 0)
            {
                Revive();
            }
            else  {
                Console.WriteLine("dead");

            }
        }

    }
        public void Heal(int hp)
        {
            // Implement healing logic
            health += hp;
            if (health > maxhealth)
            { health = maxhealth; }
           
        }

        public void RegenerateShield(int hp)
        {
        // Implement shield regeneration logic
        shield += hp;
            if (shield > maxshield)
            { shield = maxshield; }

        }

        public void Revive()
        {
            // Implement revive logic
            health = maxhealth;
            shield = maxshield;
            lives--;
        }

        public void ResetGame()
        {
            // Reset all variables to default values
            health = maxhealth;
            shield = maxshield;
            lives = 3;
             level = 1;
             xp = 0;
         }

    private void UpdateHealthStatus()
        {
            if (health <= 10)
            {
                healthStatus = "Imminent Danger";
            }
            else if (health <= 50)
            {
                healthStatus = "Badly Hurt";
            }
            else if (health <= 75)
            {
                healthStatus = "Hurt";
            }
            else if (health <= 90)
        {
            healthStatus = "Healthy";
           
        }
        else
            {
                healthStatus = "Perfect Health";
            }
        }

    // Optional XP system methods
    public void IncreaseXP(int exp)
    {
        xp += exp;
        if (xp >= 100)
        {
            Levelup();
        }
    }
       public void Levelup()
    {
        level++;
        xp = 0;

    }

    public void Update()
    {
        RegenerateShield(1);
        Heal(1);

    }
    



    public void Test_TakeDamage_OnlyShield()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;
        system.lives = 3;

        system.TakeDamage(10);

        Debug.Assert(90 == system.shield);
        Debug.Assert(100 == system.health);
        Debug.Assert(3 == system.lives);
    }

    public void Test_TakeDamage_ShieldAndHealth()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 20;
        system.health = 100;
        system.lives = 3;

        system.TakeDamage(50);

        Debug.Assert(0 == system.shield);
        Debug.Assert(80 == system.health);
        Debug.Assert(3 == system.lives);
    }

    public void Test_TakeDamage_HealthOnly()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 0;
        system.health = 100;

        system.TakeDamage(50);

        Debug.Assert(50 == system.health);
        Debug.Assert(0 == system.shield);
    }

    public void Test_TakeDamage_HealthToZero()
    {
        HealthSystem system = new HealthSystem();
        system.health = 100;
        system.lives = 3;

        system.TakeDamage(150);

        Debug.Assert(0 == system.health);
        Debug.Assert(2 == system.lives);
    }

    public void Test_TakeDamage_ShieldDepletedAndHealthToZero()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 30;
        system.health = 30;

        system.TakeDamage(70);

        Debug.Assert(0 == system.shield);
        Debug.Assert(0 == system.health);
        Debug.Assert(2 == system.lives); 
    }

    public void Test_TakeDamage_NegativeDamage()
    {
        HealthSystem system = new HealthSystem();
        system.health = 100;
        system.shield = 50;

        system.TakeDamage(-10);

        Debug.Assert(100 == system.health);
        Debug.Assert(50 == system.shield);
    }

    public void Test_Heal_NormalHealing()
    {
        HealthSystem system = new HealthSystem();
        system.health = 50;

        system.Heal(30);

        Debug.Assert(80 == system.health);
    }

    public void Test_Heal_MaxHealth()
    {
        HealthSystem system = new HealthSystem();
        system.health = 90;

        system.Heal(20);

        Debug.Assert(100 == system.health); 
    }

    public void Test_Heal_NegativeHealing()
    {
        HealthSystem system = new HealthSystem();
        system.health = 100;

        system.Heal(-10);

        Debug.Assert(100 == system.health); 
    }

    public void Test_RegenerateShield_NormalRegeneration()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 20;

        system.RegenerateShield(20);

        Debug.Assert(40 == system.shield);
    }

    public void Test_RegenerateShield_MaxShield()
    {
        HealthSystem system = new HealthSystem();

        system.RegenerateShield(20);

        Debug.Assert(50 == system.shield);
    }

    public void Test_RegenerateShield_NegativeRegeneration()
    {
        HealthSystem system = new HealthSystem();

        system.RegenerateShield(-10);

        Debug.Assert(50 == system.shield); 
    }

    public void Test_Revive_ResetsHealthAndShield()
    {
        HealthSystem system = new HealthSystem();
        system.health = 50;
        system.shield = 20;

        system.Revive();

        Debug.Assert(100 == system.health);
        Debug.Assert(50 == system.shield);
        Debug.Assert(2 == system.lives); 
    }

    public void Test_ResetGame_ResetsAllVariables()
    {
        HealthSystem system = new HealthSystem();
        system.health = 50;
        system.shield = 20;
        system.lives = 1;

        system.ResetGame();

        Debug.Assert(100 == system.health);
        Debug.Assert(50 == system.shield);
        Debug.Assert(3 == system.lives);
        Debug.Assert(1 == system.level);
        Debug.Assert(0 == system.xp);
    }
}


