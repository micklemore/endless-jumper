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

	float startGravity;

	IEnumerator shiftPlayerBackCoroutine;

    IEnumerator shiftPlayerForwardCoroutine;

    IEnumerator waitToComeBackCoroutine;

	IEnumerator endGameCoroutine;

	private void Start()
	{
		rigidBody2d = GetComponent<Rigidbody2D>();
		startGravity = rigidBody2d.gravityScale;

		//initialize coroutines
		shiftPlayerBackCoroutine = ShiftPlayerBackCoroutine();
        shiftPlayerForwardCoroutine = ShiftPlayerForwardCoroutine();
        waitToComeBackCoroutine = WaitToComeBackCoroutine();
		endGameCoroutine = EndGameCoroutine();
	}

	private void Update()
	{
		TryToJump();

		CheckPlayerShift();

		CheckRayTrace();
		Debug.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * 10, Color.blue);
	}

	void CheckRayTrace()
	{
		if (isJumping && haveToRaycast)
		{
			PerformRaycast();
		}
	}

	void PerformRaycast()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, (Vector2)transform.position + Vector2.down);

		if (hit.collider != null && hit.collider.gameObject.GetComponent<Obstacle>())
		{
			haveToRaycast = false;
			Debug.Log("Ho saltato un " + hit.collider.name);
			EventHandler.instance.IncrementScoreNotify();
		}
	}

    void CheckPlayerShift()
    {
        if (haveToShiftPlayerBack)
        {
            ShiftPlayerBack();
        }
        if (haveToShiftPlayerForward)
        {
            ShiftPlayerForward();
        }
		if(haveToShiftPlayerToDeath)
		{
			ShiftPlayerToDeath();
		}
    }

	void TryToJump()
	{
		if (Input.GetKeyDown("space"))
		{
			if (!isJumping)
			{
				rigidBody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
				isJumping = true;
				haveToRaycast = true;
				Debug.Log("jumping");
			}
			else
			{
				rigidBody2d.gravityScale = startGravity + 5;
			}
		}
	}

	IEnumerator ShiftPlayerBackCoroutine()
    {
        Debug.Log("torno indietro");

		haveToShiftPlayerBack = true;

        yield return new WaitForSeconds(speed);

        haveToShiftPlayerBack = false;

        Debug.Log("haveToShiftPlayerBack a false");

		StartCoroutine(waitToComeBackCoroutine);
    }

    IEnumerator ShiftPlayerForwardCoroutine()
    {
		Debug.Log("torno avanti");

		haveToShiftPlayerForward = true;

		yield return new WaitForSeconds(speed);

		haveToShiftPlayerForward = false;

		Debug.Log("first hit valeva: " + isFirstHit);

		isFirstHit = true;

		Debug.Log("ora vale: " + isFirstHit);
	}

    IEnumerator WaitToComeBackCoroutine()
    {
		Debug.Log("attendo..");
		
        yield return new WaitForSeconds(GameManager.instance.GetSecondsToReturnToStartPosition());

        StartCoroutine(shiftPlayerForwardCoroutine);
	}

	IEnumerator EndGameCoroutine()
	{
		haveToShiftPlayerToDeath = true;

		haveToShiftPlayerForward = false;

		yield return new WaitForSeconds(speed);

		haveToShiftPlayerToDeath = false;

		EventHandler.instance.EndGameNotify();

		//to stop game
		Time.timeScale = 0;
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

	void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log("Collision");
		if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
			Debug.Log("è un obstacle");
			if (isFirstHit)
            {
				
				isFirstHit = false;
				Debug.Log("è la prima hit, ora setto isFirstHit a: " + isFirstHit); 
				StartCoroutine(shiftPlayerBackCoroutine);
            }
            else
            {
                GameOver();
            }
        }
	}

    void GameOver()
    {
		StopAllCoroutines();
		StartCoroutine(endGameCoroutine);
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
