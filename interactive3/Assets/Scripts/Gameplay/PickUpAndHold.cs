using UnityEngine;

[AddComponentMenu("Playground/Gameplay/Pick Up And Hold")]
public class PickUpAndHold : MonoBehaviour
{
    Move moveScript;

	//pickup key and drop key could be the same
	public KeyCode pickupKey = KeyCode.A;
	public KeyCode dropKey = KeyCode.B;

	public float pickUpDistance = 3f; // An object need to closer than that distance to be picked up.
	//public float hitToDropObject = Mathf.Infinity; //if the character hits anything with a force stronger than this value, the pickup is dropped

	private Transform carriedObject = null;

    public float dirX = 20.0f;
    public float dirY = 1.0f;

    public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys;
    private float moveHorizontal;
    private bool faceToDirection = true;

    private Animator anime;
    private void Start()
    {
        anime = GetComponent<Animator>();
        anime.SetBool("MoveSpeed", false);
    }

    private void Update()
	{
		bool justPickedUpSomething = false;

		if(Input.GetKeyDown(pickupKey)
			&& carriedObject == null)
		{
			//Nothing in hand, we check if something is around and pick it up.
			justPickedUpSomething = PickUp();
			//Debug.Log("Pickup");
		}

		if(Input.GetKeyDown(dropKey)
			&& carriedObject != null
			&& !justPickedUpSomething)
		{
			//We're holding something already, we drop
			Drop();
			//Debug.Log("Drop");
		}
        if (typeOfControl == Enums.KeyGroups.ArrowKeys)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            if (moveHorizontal > 0.01f)
            {
                faceToDirection = true;
                //anime.SetBool("MoveSpeed", true);
            }

            if (moveHorizontal < -0.01f)
            {
                faceToDirection = false;
                //anime.SetBool("MoveSpeed", false);
            }
        }
    }




    public void Drop()
	{
		Rigidbody2D rb2d = carriedObject.GetComponent<Rigidbody2D>();
		if(rb2d != null)
		{
			rb2d.bodyType = RigidbodyType2D.Dynamic;
            if (faceToDirection == true)
            {
                rb2d.AddForce(new Vector2(dirX, dirY), ForceMode2D.Impulse);
            }

            if (faceToDirection == false)
            {
                rb2d.AddForce(new Vector2(-dirX, dirY), ForceMode2D.Impulse);

            }

            //rb2d.velocity = Vector2.zero;
		}
		//unparenting
		carriedObject.parent = null;
		//hands are free again
		carriedObject = null;
	}

	public bool PickUp()
	{
		//Collect every Pickup around
		GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

		// Find the closest
		float dist = pickUpDistance;
		for(int i = 0; i < pickups.Length; i++)
		{
			float newDist = (transform.position - pickups[i].transform.position).sqrMagnitude;
			if(newDist  < dist)
			{
				carriedObject = pickups[i].transform;
				dist = newDist;

                Vector2 pos = carriedObject.position;
                pos.x = transform.position.x;
                pos.y = transform.position.y + 1.7f;
                carriedObject.position = pos;

            }
        }

		// Check if we found something
		if(carriedObject != null)
		{
			//check if another player had it, in this case, steal it
			Transform pickupParent = carriedObject.parent;
			if(pickupParent != null)
			{
				PickUpAndHold pickupScript = pickupParent.GetComponent<PickUpAndHold>();
				if(pickupScript != null)
				{
					pickupScript.Drop();
				}
			}

			carriedObject.parent = gameObject.transform;
			// Set to Kinematic so it will move with the Player
			Rigidbody2D rb2d = carriedObject.GetComponent<Rigidbody2D>();
			if(rb2d != null)
			{
				rb2d.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
			}
			return true;
		}
		else
		{
			return false;
		}
	}
}