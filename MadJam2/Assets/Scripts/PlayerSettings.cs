using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour {

	public static PlayerSettings Instance;

	public KeyCode Forward;
	public KeyCode Back;
	public KeyCode Right;
	public KeyCode Left;
    public KeyCode Jump;
	public KeyCode Dash;

	public void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			//DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(this.gameObject);
			return;
		}

		Forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey","W"));
		Back = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backKey", "S"));
		Right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
		Left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
		Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
		Dash = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dashKey", "LeftShift"));
	}
}
