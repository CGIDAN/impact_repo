using UnityEngine;
using System.Collections;

namespace PlanetRunner
{
	public class PlayerController : MonoBehaviour
	{

		[Tooltip("Enables Auto-Move")]
		public bool EnableAutoMove = false;
		[Tooltip("If Auto-Move is enabled, select the direction")]
		public bool AutoMoveRight = true;

		private Transform LookTransform;

		public float jumpForce = 7.0f;

		[Tooltip("Enables DoubleJump. Default is SingleJump")]
		public bool EnableDoubleJump;
		public float doubleJumpForce = 5.0f;

		public float speed = 3.0f;
		public float maxVelocityChange = 10.0f;

		public bool EnableGravitation = false;
		public float gravity = 2.0f;
		[Tooltip("Local gravity: How much local gravitation if the player walks on the planet. More speed needs more local gravity")]
		public float localGravity = 9.8f;

		public float mass = 1.0f;


		[HideInInspector]
		public bool facingRight = true;

		public Transform GroundCheck;

		[HideInInspector]
		public bool grounded = false;

		public LayerMask whatIsGround;


		private bool DoJump = false;

		private bool DoDoubleJump = false;

		[HideInInspector]
		public System.Guid lastPlanetID;
		private System.Guid nextPlanetID;

		private Animator anim;

		private GameController gameController;

		// Movement Variables
		private Vector3 right;
		private Vector3 targetVelocity;


		void Awake()
		{
			anim = gameObject.GetComponent<Animator>();

			GameObject go = GameObject.FindGameObjectWithTag(Const.GAMECONTROLLER);

			if (go != null)
			{
				gameController = go.GetComponent<GameController>();

			}
			else
			{
				Debug.Log("PlayerController: GameController not found");
			}

			// Set the WhatIsGround by default to Ground
			// Disable the following line if you want to use own grounds
			whatIsGround = 1 << LayerMask.NameToLayer(Const.GROUND);
		}

		public void updateGravity()
		{

			Rigidbody2D rigid = GetComponent<Rigidbody2D>();

			if (rigid == null)
			{
				rigid = gameObject.AddComponent<Rigidbody2D>();
			}

			if (EnableGravitation)
			{

				rigid.gravityScale = gravity;
				rigid.mass = mass;

			}
			else
			{
				rigid.gravityScale = 0;
				rigid.mass = 1;
			}

			if (grounded)
			{
				rigid.gravityScale = 0;
				rigid.mass = 1;
			}

			rigid.freezeRotation = true;
		}

		void Update()
		{

			grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.16f, whatIsGround);

			if (anim != null)
			{
				anim.SetBool(Const.GROUND, grounded);
			}
			else
			{
				Debug.Log("Animation-Controller is empty");
			}

			if (gameController != null)
			{
				if (gameController.StartGame)
				{

					// If grounded reset the Jump-Variables
					if (grounded)
					{
						if (DoJump || DoDoubleJump)
						{
							DoJump = false;
							DoDoubleJump = false;
							anim.SetBool("IsJumping", false);
						}
					}

					updateGravity();


					bool doJumpButtonClicked = false;

					// Keyboard-Input
					if (Input.GetButtonDown("Jump"))
					{
						doJumpButtonClicked = true;
						anim.SetBool("IsJumping", true);
					}

					// Touch-Input
					if (!doJumpButtonClicked)
					{
						if (Input.touchCount > 0)
						{
							foreach (Touch t in Input.touches)
							{
								if (t.phase == TouchPhase.Began)
								{
									doJumpButtonClicked = true;
								}
							}
						}
					}
					/*
					// The Jump-Button is clicked
					if (doJumpButtonClicked)
					{

						// Do a single jump if the player is grounded
						if (grounded && !DoJump)
						{
							DoJump = true;
							GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
						}

						// General: Id DoubleJump enabled?
						if (EnableDoubleJump)
						{

							// Do only DoubleJump if the player is not grounded
							if (!grounded && !DoJump && !DoDoubleJump)
							{

								DoDoubleJump = true;
								GetComponent<Rigidbody2D>().AddForce(transform.up * doubleJumpForce, ForceMode2D.Impulse);
							}
						}
					}
					*/
					bool doAlignIt = true;

					if (EnableDoubleJump && DoDoubleJump)
					{
						doAlignIt = false;
					}

					// Only align the player to the planet on SingleJump
					if (doAlignIt && !EnableGravitation)
					{
						alignToPlanet(LookTransform.position);
					}
				}
			}
		}

		void FixedUpdate()
		{

			if (gameController != null)
			{
				if (gameController.StartGame)
				{

					if (grounded)
					{

						if (EnableAutoMove)
						{
							if (!nextPlanetID.ToString().Equals(lastPlanetID.ToString()))
							{
								lastPlanetID = nextPlanetID;
								GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
							}
						}

						if (LookTransform != null)
						{
							// Calculate how fast we should be moving
							Vector3 forward = Vector3.Cross(transform.up, -LookTransform.right).normalized;
							right = Vector3.Cross(transform.up, LookTransform.forward).normalized;


							if (EnableAutoMove)
							{

								if (AutoMoveRight)
								{
									targetVelocity = (forward + right) * speed;

									if (!facingRight)
									{
										Flip();
									}

								}
								else
								{
									targetVelocity = (forward + -right) * speed;

									if (facingRight)
									{
										Flip();
									}
								}


							}
							else
							{
								//targetVelocity = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")) * speed;

							}

							Vector3 velocity = transform.InverseTransformDirection(GetComponent<Rigidbody2D>().velocity);

							velocity.y = 0;
							velocity = transform.TransformDirection(velocity);
							Vector3 velocityChange = transform.InverseTransformDirection(targetVelocity - velocity);

							velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
							velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
							velocityChange.y = 0;

							velocityChange = transform.TransformDirection(velocityChange);

							// You need the MaxVelocityChange variable if you change the AddForde to Force instead Impulse
							GetComponent<Rigidbody2D>().AddForce(velocityChange, ForceMode2D.Impulse);

						}
						else
						{
							Debug.Log("look transform null");
						}

						if (!EnableAutoMove)
						{

							float horizontal = Input.GetAxis("Horizontal");
							horizontal = Mathf.Clamp(horizontal, -1f, 1f);

							if (horizontal > 0f && !facingRight)
							{
								Flip();

							}
							else if (horizontal < 0f && facingRight)
							{
								Flip();
							}
						}

						if (anim != null)
						{
							anim.SetFloat(Const.SPEED, Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude));
						}
						else
						{
							Debug.Log("Animation-Controller is empty");
						}
					}

				}
				else
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
					anim.SetFloat(Const.SPEED, 0.0f);
				}
			}


			targetVelocity = (right * Input.GetAxis("Horizontal")) * speed;
		}



		void Flip()
		{

			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

		private void alignToPlanet(Vector3 targetPosition)
		{

			Vector3 toCenter = targetPosition - transform.position;
			toCenter.Normalize();

			GetComponent<Rigidbody2D>().AddForce(toCenter * localGravity, ForceMode2D.Force);


			Quaternion q = Quaternion.FromToRotation(transform.up, -toCenter);
			q = q * transform.rotation;
			transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);

		}

		public void PlayerDie()
		{
			gameController.DoSetGameOverLevel();
		}

		public void SetPlanetIdAndAlign(System.Guid planetId, Vector3 pos)
		{
			nextPlanetID = planetId;
			alignToPlanet(pos);
		}

		public void SetLookTransform(Transform lookTransform)
		{
			this.LookTransform = lookTransform;
		}

		public void MoveLeft()
		{
			targetVelocity = (right * -1.0f) * speed;
			if (facingRight)
			{
				Flip();
			}
		}

		public void MoveRight()
		{
			targetVelocity = (right * 1.0f) * speed;
			if (!facingRight)
			{
				Flip();
			}
		}
	}
}
