using UnityEngine;
using System.Collections;
using UnityEngine.AI;
//simple "platformer enemy" AI
[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(DealDamage))]
public class EnemyAI : MonoBehaviour 
{
	public float acceleration = 35f;					//acceleration of enemy movement
	public float deceleration = 8f;						//deceleration of enemy movement
	public float rotateSpeed = 0.7f;					//how fast enemy can rotate
	public float speedLimit = 10f;						//how fast enemy can move
	public Vector3 bounceForce = new Vector3(0, 13, 0);	//force to apply to player when player jumps on enemies head
	public AudioClip bounceSound;						//sound when you bounce on enemies
	public float pushForce = 10f;						//how far away to push the player when they are attacked by the enemy
	public float pushHeight = 7f;						//how high to push the player when they are attacked by the enemy
	public int attackDmg = 1;							//how much damage to deal to the player when theyre attacked by this enem
	public bool chase = true;							//should this enemy chase objects inside its sight?
	public bool ignoreY = true;							//ignore Y axis when chasing? (this would be false for say.. a flying enemy)
	public float chaseStopDistance = 0.7f;				//stop this far away from object when chasing it
	public GameObject sightBounds;						//trigger for sight bounds
	public GameObject attackBounds;                     //trigger for attack bounds (player is hurt when they enter these bounds)
	public GameObject shootBounds;
	public Animator animatorController;					//object which holds the animator for this enem	
	public MoveToPoints moveToPointsScript;             //if you've attached this script, drag the component here
	public float shootTime = 0.5f;
	public int howManyShoots = 3;
	public float timeAfterAllShoots = 5;

	private TriggerParent sightTrigger;
	private TriggerParent attackTrigger;
	private TriggerParent shootTrigger;
	private PlayerMove playerMove;
	private CharacterMotor characterMotor;
	private DealDamage dealDamage;
	private Shoot shooter;
	private int shooting_number;
	private float shootTimeCounter;

	private Transform destination;
	private NavMeshAgent navigation_agent;

	public bool haveShoot;

	//setup
	void Awake()
	{
		navigation_agent = GetComponent<NavMeshAgent>();
		destination = GameObject.FindGameObjectWithTag("Player").transform;

		shooting_number = 0;
		shootTimeCounter = 0;
		
		characterMotor = GetComponent<CharacterMotor>();
		dealDamage = GetComponent<DealDamage>();
		//avoid setup errors
		if(tag != "Enemy")
		{
			tag = "Enemy";
			Debug.LogWarning("'EnemyAI' script attached to object without 'Enemy' tag, it has been assign automatically", transform);
		}
		
		if(sightBounds)
		{
			sightTrigger = sightBounds.GetComponent<TriggerParent>();
			if(!sightTrigger)
				Debug.LogError("'TriggerParent' script needs attaching to enemy 'SightBounds'", sightBounds);
		}
		if(!sightBounds)
			Debug.LogWarning("Assign a trigger with 'TriggerParent' script attached, to 'SightBounds' or enemy will not be able to see", transform);
		
		if(attackBounds)
		{
			attackTrigger = attackBounds.GetComponent<TriggerParent>();
			if(!attackTrigger)
				Debug.LogError("'TriggerParent' script needs attaching to enemy 'attackBounds'", attackBounds);
		}
		else
			Debug.LogWarning("Assign a trigger with 'TriggerParent' script attached, to 'AttackBounds' or enemy will not be able to attack", transform);

		if(haveShoot)
        {
			if (shootBounds)
				shootTrigger = shootBounds.GetComponent<TriggerParent>();
			shooter = GetComponent<Shoot>();
		}
	}
	
	void Update()
	{
		//chase
		if (navigation_agent)
		{ 
			navigation_agent.destination = destination.position;
			bool ismoving = navigation_agent.velocity.x > 0 || navigation_agent.velocity.y > 0 || navigation_agent.velocity.z > 0;
			if (animatorController)
				animatorController.SetBool("Moving", ismoving);
		}
		else if (sightTrigger && sightTrigger.colliding && chase && sightTrigger.hitObjects != null && sightTrigger.hitObjects.Count > 0 && sightTrigger.hitObjects[0].activeInHierarchy)
		{
			characterMotor.MoveTo (sightTrigger.hitObjects[0].transform.position, acceleration, chaseStopDistance, ignoreY);
			//nofity animator controller
			if(animatorController)
				animatorController.SetBool("Moving", true);
			//disable patrol behaviour
			if(moveToPointsScript)
				moveToPointsScript.enabled = false;
		}
		else
		{	
			//notify animator
			if(animatorController)
				animatorController.SetBool("Moving", false);
			//enable patrol behaviour
			if(moveToPointsScript)
				moveToPointsScript.enabled = true;
		}
		
		//attack
		if (attackTrigger && attackTrigger.collided && attackTrigger.hitObjects.Count > 0)
		{
			dealDamage.Attack(attackTrigger.hitObjects[0], attackDmg, pushHeight, pushForce);
			//notify animator controller
			if(animatorController)
				animatorController.SetBool("Attacking", true);	
		}
		else if(animatorController)
			animatorController.SetBool("Attacking", false);

		if(haveShoot && shootTrigger.colliding && shootTrigger.hitObjects.Count > 0 )
        {
			shootTimeCounter -= Time.deltaTime;
			if (shootTimeCounter < 0)
			{
				shooter.ShootTo(shootTrigger.hitObjects[0]);
				shootTimeCounter = shootTime;
				shooting_number++;

				if (shooting_number >= howManyShoots)
				{
					shootTimeCounter = timeAfterAllShoots;
					shooting_number = 0;
				}
			}
		}
	}
	
	void FixedUpdate()
	{
		characterMotor.ManageSpeed(deceleration, speedLimit, ignoreY);
		characterMotor.RotateToVelocity (rotateSpeed, ignoreY);
	}
	
	//bounce player when they land on this enemy
	public void BouncedOn()
	{	
		if(!playerMove)
			playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
		if (bounceSound)
			AudioSource.PlayClipAtPoint(bounceSound, transform.position);
		if(playerMove)
		{
			Vector3 bounceMultiplier = new Vector3(0f, 1.5f, 0f) * playerMove.onEnemyBounce;
			playerMove.Jump (bounceForce + bounceMultiplier);
		}
		else
			Debug.LogWarning("'Player' tagged object landed on enemy, but without playerMove script attached, is unable to bounce");
	}
}