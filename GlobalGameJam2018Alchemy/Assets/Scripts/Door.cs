using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [Tooltip("Health of the door at creation / Max health.")]
    public int startingHealth = 100;

    public Slider slider;

    private int health;
    public int Health
    {
        get { return health; }
        set
        {
            if (slider != null) { slider.value = health / (float)startingHealth; }
            health = value;
        }
    }

    public void Start()
    {
        Health = startingHealth;
        slider.value = 1f;
    }

    public void Attack(int damage)
    {
        Health = Mathf.Max(0, Health - damage);
        if (Health <= 0) { FindObjectOfType<GameController>()?.TriggerGameOver(false); }
    }

    public void Heal(int heal) => Health = Mathf.Min(startingHealth, Health + heal);
}
