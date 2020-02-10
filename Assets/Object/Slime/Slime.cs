using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Slime : MonoBehaviour2D
{
	public Rigidbody2D rigidbody2D{get;private set;}

	public SlimeCore slimeCore{get; private set;}
	public SlimeSticky slimeSticky{get; private set;}

	[SerializeField]
	public float WEIGHT = 30;

	[SerializeField]
	public float CORE_RADIUS = 0.5f; 
	public float CoreRadius {
		get{return slimeCore.CoreRadius;}
		set{slimeCore.CoreRadius = value;}
	}
	
	[SerializeField]
	public float STICKY_VOLUME = 10f;
	public float StickyVolume {
		get{return slimeSticky.Volume;}
		set{slimeSticky.Volume = value;}
	}
	[SerializeField]
	public int STICKY_VERTEX = 32;
	public float StickyVertex {
		get{return slimeSticky.VertexSize;}
	}
	
	void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.mass = WEIGHT;

		slimeCore = transform.Find("Core").GetComponent<SlimeCore>();
		slimeCore.Initialize(this);

		slimeSticky = transform.Find("Sticky").GetComponent<SlimeSticky>();
		slimeSticky.Initialize(this);
	}
}
