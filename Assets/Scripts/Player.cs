using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    Transform backPoint;

    [SerializeField]
    Transform startPoint;

	[SerializeField]
	Transform deathPoint;

	[SerializeField]
	float jumpForce;

	Rigidbody2D rigidBody2d;

	bool isFirstHit = true;

    float playerShiftSpeed = 1f;

    bool haveToShiftPlayerBack = false;

    bool haveToShiftPlayerForward = false;

	bool haveToShiftPlayerToDeath = false;

    bool isJumping = true;

	bool haveToRaycast = false;

	bool lastRaycastHit = false;

	float startGravity;

	bool isFalling = false;

	bool isShiftingBack = false;

	bool hasLiftedFinger = false;

	Animator playerAnimator;

	void Start()
	{
		rigidBody2d = GetComponent<Rigidbody2D>();

		playerAnimator = GetComponent<Animator>();

		startGravity = rigidBody2d.gravityScale;
	}

	void Update()
	{
		CheckIfFalling();

		TryToJumpWithSpacebar();

		TryToJumpWithTouch();

		CheckIfRayTraceIsNeededToDetectObstacles();

		CheckIfPlayerHasToBeShift();
	}

	void CheckIfFalling()
	{
		if (rigidBody2d.velocity.y < -0.1) { 
			isFalling = true;
			SetAnimatorParameter("IsFalling", isFalling);
		}
		else
		{
			isFalling = false;
			SetAnimatorParameter("IsFalling", isFalling);
		}
	}

	void SetAnimatorParameter(string parameter, bool value)
	{
		playerAnimator.SetBool(parameter, value);
	}

	void CheckIfRayTraceIsNeededToDetectObstacles()
	{
		if (isJumping && haveToRaycast && !isShiftingBack)
		{
			PerformRaycastToDetectObstacles();
		}
		else
		{
			lastRaycastHit = false;
		}
	}

	void PerformRaycastToDetectObstacles()
	{
		Debug.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * 10, Color.blue);
		RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, (Vector2)transform.position + Vector2.down);
		
		if (hit.collider != null)
		{
			if (hit.collider.gameObject.GetComponent<Obstacle>() != null)
			{
				lastRaycastHit = true;
			}
			else
			{
				if (lastRaycastHit)
				{
					haveToRaycast = false;
					lastRaycastHit = false;
					GameManager.instance.IncrementScore();
				}
			}
		}
	}

    void CheckIfPlayerHasToBeShift()
    {
		if (haveToShiftPlayerToDeath)
		{
			ShiftPlayerToDeath();
		}
		else if (haveToShiftPlayerBack)
        {
            ShiftPlayerBack();
        }
        else if (haveToShiftPlayerForward)
        {
            ShiftPlayerForward();
        }
    }

	void TryToJumpWithTouch()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (!isJumping && touch.phase == TouchPhase.Began)
			{
				Jump();
				haveToRaycast = true;
				hasLiftedFinger = false;
			}
			else if (isJumping)
			{
				if (hasLiftedFinger && (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
				{
					rigidBody2d.gravityScale = startGravity + 5;
				}
			}
			if (touch.phase == TouchPhase.Ended)
			{
				rigidBody2d.gravityScale = startGravity;
				hasLiftedFinger = true;
			}
		}
	}

	void TryToJumpWithSpacebar()
	{
		if (!isJumping)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Jump();

				haveToRaycast = true;
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				rigidBody2d.gravityScale = startGravity + 5;
			}
			if (Input.GetKeyUp(KeyCode.Space))
			{
				rigidBody2d.gravityScale = startGravity;
			}
		}
	}

	IEnumerator ShiftPlayerBackCoroutine()
    {
		EventHandler.instance.PlayHurtAudioNotify();
		SetAnimatorParameter("IsHit", true);
		haveToShiftPlayerBack = true;
		isShiftingBack = true;

        yield return new WaitForSeconds(playerShiftSpeed);

		SetAnimatorParameter("IsHit", false);
		haveToShiftPlayerBack = false;

		StartCoroutine(WaitToComeBackCoroutine());
    }

	IEnumerator WaitToComeBackCoroutine()
    {		
        yield return new WaitForSeconds(GameManager.instance.SecondsToReturnToStartPosition);

        StartCoroutine(ShiftPlayerForwardCoroutine());
	}

    IEnumerator ShiftPlayerForwardCoroutine()
    {
		haveToShiftPlayerForward = true;
		isShiftingBack = false;
		SetAnimatorParameter("IsReturningToStart", haveToShiftPlayerForward);

		isFirstHit = true;

		yield return new WaitForSeconds(playerShiftSpeed);

		haveToShiftPlayerForward = false;
		SetAnimatorParameter("IsReturningToStart", haveToShiftPlayerForward);
	}

    
	IEnumerator EndGameCoroutine()
	{
		EventHandler.instance.PlayDeathAudioNotify();
		SetAnimatorParameter("IsHit", true);
		haveToShiftPlayerToDeath = true;

		haveToShiftPlayerForward = false;
		SetAnimatorParameter("IsReturningToStart", haveToShiftPlayerForward);

		haveToRaycast = false;

		yield return new WaitForSeconds(playerShiftSpeed);

		SetAnimatorParameter("IsHit", false);
		haveToShiftPlayerToDeath = false;

		EventHandler.instance.EndGameNotify();
	}

    void ShiftPlayerBack()
    {
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, backPoint.transform.position.x, Time.deltaTime * playerShiftSpeed), 
                                                    transform.position.y,
                                                    transform.position.z);
    }

    void ShiftPlayerForward()
    {
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, startPoint.transform.position.x, Time.deltaTime * playerShiftSpeed),
													transform.position.y,
													transform.position.z);
	}

	void ShiftPlayerToDeath()
	{
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, deathPoint.transform.position.x, Time.deltaTime * playerShiftSpeed),
													transform.position.y,
													transform.position.z);
	}

    void GameOver()
    {
		StopAllCoroutines();
		StartCoroutine(EndGameCoroutine());
	}

	void Jump()
	{
		rigidBody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		isJumping = true;
		SetAnimatorParameter("IsJumping", isJumping);
		EventHandler.instance.PlayJumpAudioNotify();
	}

	void Landed()
	{
		isJumping = false;
		SetAnimatorParameter("IsJumping", isJumping);
		rigidBody2d.gravityScale = startGravity;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
			haveToRaycast = false;
			if (isFirstHit)
            {	
				isFirstHit = false;
				StartCoroutine(ShiftPlayerBackCoroutine());
            }
            else
            {
				GameOver();
            }
        }
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			Landed();

			haveToRaycast = false;
		}
	}
}
