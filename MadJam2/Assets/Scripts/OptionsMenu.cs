using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	public AudioMixer audio;

	public bool waitingForKey;
	private Transform controls;
	private Event keyEvent;
	private KeyCode newKey;
	Text buttonText;

	void Awake()
	{
		controls = transform.Find("Controls");
		waitingForKey = false;

		for (int i = 0; i < controls.childCount; i++)
		{
			if (controls.GetChild(i).name == "UpButton")
				controls.GetChild(i).Find("Text").GetComponent<Text>().text = PlayerSettings.Instance.Forward.ToString();
			else if (controls.GetChild(i).name == "DownButton")
				controls.GetChild(i).Find("Text").GetComponent<Text>().text = PlayerSettings.Instance.Back.ToString();
			else if (controls.GetChild(i).name == "LeftButton")
				controls.GetChild(i).Find("Text").GetComponent<Text>().text = PlayerSettings.Instance.Left.ToString();
			else if (controls.GetChild(i).name == "RightButton")
				controls.GetChild(i).Find("Text").GetComponent<Text>().text = PlayerSettings.Instance.Right.ToString();
			else if (controls.GetChild(i).name == "JumpButton")
				controls.GetChild(i).Find("Text").GetComponent<Text>().text = PlayerSettings.Instance.Jump.ToString();
			else if (controls.GetChild(i).name == "DashButton")
				controls.GetChild(i).Find("Text").GetComponent<Text>().text = PlayerSettings.Instance.Dash.ToString();
		}

	}

	public void OnGUI()
	{
		keyEvent = Event.current;

		if (keyEvent.isKey && waitingForKey)
		{
			newKey = keyEvent.keyCode;
			waitingForKey = false;
		}
	}

	public void StartAssigment(string keyName)
	{
		if (!waitingForKey)
			StartCoroutine(AssignKey(keyName));
	}

	public void SendText(Text text)
	{
		buttonText = text;
	}

	IEnumerator WaitForKey()
	{
		while (!keyEvent.isKey)
			yield return null;
	}

	public IEnumerator AssignKey(string keyName)
	{
		waitingForKey = true;
		yield return WaitForKey();

		switch (keyName)
		{
			case "Forward":
				PlayerSettings.Instance.Forward = newKey;
				buttonText.text = PlayerSettings.Instance.Forward.ToString();
				PlayerPrefs.SetString("forwardKey", PlayerSettings.Instance.Forward.ToString());
				break;
			case "Back":
				PlayerSettings.Instance.Back = newKey;
				buttonText.text = PlayerSettings.Instance.Back.ToString();
				PlayerPrefs.SetString("backKey", PlayerSettings.Instance.Back.ToString());
				break;
			case "Left":
				PlayerSettings.Instance.Left = newKey;
				buttonText.text = PlayerSettings.Instance.Left.ToString();
				PlayerPrefs.SetString("leftKey", PlayerSettings.Instance.Left.ToString());
				break;
			case "Right":
				PlayerSettings.Instance.Right = newKey;
				buttonText.text = PlayerSettings.Instance.Right.ToString();
				PlayerPrefs.SetString("rightKey", PlayerSettings.Instance.Right.ToString());
				break;
			case "Jump":
				PlayerSettings.Instance.Jump = newKey;
				buttonText.text = PlayerSettings.Instance.Jump.ToString();
				PlayerPrefs.SetString("jumpKey", PlayerSettings.Instance.Jump.ToString());
				break;
			case "Dash":
				PlayerSettings.Instance.Dash = newKey;
				buttonText.text = PlayerSettings.Instance.Dash.ToString();
				PlayerPrefs.SetString("dashKey", PlayerSettings.Instance.Dash.ToString());
				break;
		}
	}

	public void SetVolume(float volume)
	{
		audio.SetFloat("volume", volume);
	}
}
