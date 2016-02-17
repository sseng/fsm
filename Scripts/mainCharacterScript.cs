using UnityEngine;
using System.Collections;

public class mainCharacterScript : MonoBehaviour 
{
	
	
	public float runSpeed = 6.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;
	CharacterController controller1;
	
	public float hp = 100.0f; 	  //health need to be a float to make (hp/max_health) in adjustHealth() work
	public float max_health=100.0f;
	public GUIStyle _style; //choose style of GUI you want, choose character, under style
	public float len_bar_default;
	public float len_health_bar;
	
	public GameObject bullet;
	public int max_bullet=100;
	public int curr_bullet=100;
	public int check;
	public float check_bullet;
	Vector3 firePos;
	
	// Use this for initialization
	void Start () 
	{
		controller1 = GetComponent<CharacterController>();
		len_bar_default=Screen.width/4;
		
	}
	
	// Update is called once per frames
	void Update () 
	{
		if (controller1.isGrounded)
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
			//moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= runSpeed;
			
			if(Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller1.Move(moveDirection * Time.deltaTime);
		adjustHealth();
		if (hp < 1)
			Destroy (this.gameObject);
		
		
	}
	void OnGUI()
	{
		
		
	}
	void adjustHealth()
	{
		len_health_bar=len_bar_default*(float)(hp/max_health);
	}
	
	public void ApplyDamage(float damage)
	{
		hp -= damage;
		Debug.Log (hp);
	}
	
	public float getHP()
	{	 return hp; 	}
	
	public void setHP(float health)
	{	this.hp = health;
	}
		
}
