using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject backPoint;

    [SerializeField]
    GameObject startPoint;

	[SerializeField]
	GameObject deathPoint;

	[SerializeField]
	float jumpForce;

	Rigidbody2D rigidBody2d;

	bool isFirstHit = true;

    float speed = 1f;

    bool haveToShiftPlayerBack = false;

    bool haveToShiftPlayerForward = false;

	bool haveToShiftPlayerToDeath = false;

    bool isJumping = true;

	bool haveToRaycast = false;

	bool lastRaycastHit = false;

	float startGravity;

	bool isFalling = false;

	bool playHitAnimation = false;

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

		SetAnimatorParameters();

		TryToJump();

		CheckRayTrace();

		CheckPlayerShift();

		
	}

	void CheckIfFalling()
	{
		if (rigidBody2d.velocity.y < -0.1) { 
			isFalling = true;
		}
		else
		{
			isFalling = false;
		}
	}

	void SetAnimatorParameters()
	{
		playerAnimator.SetBool("IsJumping", isJumping);
		playerAnimator.SetBool("IsFalling", isFalling);
		playerAnimator.SetBool("IsHit", playHitAnimation);
		playerAnimator.SetBool("IsReturningToStart", haveToShiftPlayerForward);
	}

	void CheckRayTrace()
	{
		if (isJumping)
		{
			PerformRaycast();
		}
		else
		{
			lastRaycastHit = false;
		}
	}

	void PerformRaycast()
	{
		Debug.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * 10, Color.blue);
		RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, (Vector2)transform.position + Vector2.down);
		
		if (hit.collider != null)
		{
			Debug.Log("collider != null");
			if (hit.collider.gameObject.GetComponent<Obstacle>() != null)
			{
				lastRaycastHit = true;
				Debug.Log("raycast with obstacle");
			}
			else
			{
				Debug.Log("non ho fatto collision con obstacle");
				if (lastRaycastHit)
				{
					haveToRaycast = false;
					lastRaycastHit = false;
					GameManager.instance.IncrementScore();
				}
			}
		}
	}

    void CheckPlayerShift()
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

	void TryToJump()
	{
		if (!isJumping)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				rigidBody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
				AudioManager.instance.PlayJumpAudio();
				isJumping = true;
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
		AudioManager.instance.PlayHurtAudio();
		playHitAnimation = true;
		haveToShiftPlayerBack = true;

        yield return new WaitForSeconds(speed);

		playHitAnimation = false;
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

		isFirstHit = true;

		yield return new WaitForSeconds(speed);

		haveToShiftPlayerForward = false;
	}

    

	IEnumerator EndGameCoroutine()
	{
		AudioManager.instance.PlayDeathAudio();
		playHitAnimation = true;
		haveToShiftPlayerToDeath = true;

		haveToShiftPlayerForward = false;

		haveToRaycast = false;

		yield return new WaitForSeconds(speed);

		playHitAnimation = false;
		haveToShiftPlayerToDeath = false;

		EventHandler.instance.EndGameNotify();
	}

    void ShiftPlayerBack()
    {
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, backPoint.transform.position.x, Time.deltaTime * speed), 
                                                    transform.position.y,
                                                    transform.position.z);
    }

    void ShiftPlayerForward()
    {
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, startPoint.transform.position.x, Time.deltaTime * speed),
													transform.position.y,
													transform.position.z);
	}

	void ShiftPlayerToDeath()
	{
		transform.position = new Vector3(Mathf.Lerp(transform.position.x, deathPoint.transform.position.x, Time.deltaTime * speed),
													transform.position.y,
													transform.position.z);
	}

    void GameOver()
    {
		StopAllCoroutines();
		StartCoroutine(EndGameCoroutine());
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
			//haveToRaycast = false;
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
			isJumping = false;
			haveToRaycast = false;
			rigidBody2d.gravityScale = startGravity;
		}
	}
}
