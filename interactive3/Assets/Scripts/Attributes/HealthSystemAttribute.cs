using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Attributes/Health System")]
public class HealthSystemAttribute : MonoBehaviour
{
	public int health = 3;

	private UIScript ui;
	private int maxHealth;

	// Will be set to 0 or 1 depending on how the GameObject is tagged
	// it's -1 if the object is not a player
	private int playerNumber;
    public ParticleSystem deathEffect;
    public GameObject hiddenItem;
    public bool hiddenItemBool = false;




    private void Start()
	{
        Debug.Log("start");
        if(hiddenItemBool)
            hiddenItem.SetActive(false);
        // Find the UI in the scene and store a reference for later use
        ui = GameObject.FindObjectOfType<UIScript>();
        Debug.Log("28");
        // Set the player number based on the GameObject tag
        switch (gameObject.tag)
		{
			case "Player":
				playerNumber = 0;
				break;
			case "Player2":
				playerNumber = 1;
				break;
			default:
				playerNumber = -1;
				break;
		}
        Debug.Log("42");
        // Notify the UI so it will show the right initial amount
        if (ui != null&& playerNumber != -1)
		{
			ui.SetHealth(health, playerNumber);
		}

		maxHealth = health; //note down the maximum health to avoid going over it when the player gets healed
        Debug.Log("maxHealth" + maxHealth);
	}


	// changes the energy from the player
	// also notifies the UI (if present)
	public void ModifyHealth(int amount)
	{
        Debug.Log("AddAmount" + amount);
        //avoid going over the maximum health by forcin
        if (health + amount > maxHealth)
		{
			amount = maxHealth - health;
            Debug.Log("ture: " + maxHealth + "health: " + health);
		}
			
		health += amount;
        Debug.Log("Health" + amount);
        // Notify the UI so it will change the number in the corner
        if (ui != null
			&& playerNumber != -1)
		{
			ui.ChangeHealth(amount, playerNumber);
		}

		//DEAD
		if(health <= 0)
		{
            deathEffect.transform.position = this.transform.position;
            deathEffect = Instantiate(deathEffect);
            deathEffect.Play();

            if (hiddenItemBool)
            {
                hiddenItem.transform.position = this.transform.position;
                hiddenItem.SetActive(true);
            }

            Destroy(gameObject);
		}
	}
}
