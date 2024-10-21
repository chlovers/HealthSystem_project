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
            Debug.Assert(90 == shield);
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
}