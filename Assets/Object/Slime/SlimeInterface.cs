using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeInterface : MonoBehaviour
{
	SlimeCUI slimeCUI;

    void Awake()
    {
		slimeCUI = transform.Find("SlimeCUIFrame").GetComponent<SlimeCUI>();
    }
	
	private void Update() {
		
	}
}
