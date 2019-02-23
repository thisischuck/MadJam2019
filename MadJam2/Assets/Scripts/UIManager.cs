using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	// Start is called before the first frame update
	private void Awake()
    {
        if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
