using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public PlayerController playerPrefab;
	public Camera PlayerCamera;
	private PlayerController currentPlayer;
	public Vector3 SpawnPosition;

	private bool gameRunning;

	private void Awake()
	{
		if (Instance == null)
		{
			GameManager.Instance = this;
			gameRunning = false;
			//DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(this.gameObject);
			return;
		}
	}

	void Update()
	{
		if (gameRunning)
		{
			
		}
	}

	public void StartGame()
	{
		gameRunning = true;
		SetUpPlayer();
	}

	public void SetUpPlayer()
	{
		PlayerController player = FindObjectOfType<PlayerController>();
		if (player == null)
			currentPlayer = Instantiate(playerPrefab);
		else currentPlayer = player;

		currentPlayer.transform.position = SpawnPosition;
		currentPlayer.isMovable = true;
	}

	public void EndGame(bool won)
	{
		//UIManager.Instance.SetEnd(true, pickedUpItems);
		//FindObjectOfType<AudioManager>().Play("Moan");
		//currentPoop.transform.position = currentPlayer.transform.position - (Vector3.down * 0.5f);
		gameRunning = false;
		currentPlayer.isMovable = false;
	}
}
