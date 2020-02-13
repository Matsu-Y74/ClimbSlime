using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SlimeStickyNode : MonoBehaviour2D
{
	public SlimeSticky Parent{get;private set;}

	public SlimeStickyNode PrevConnectTo{get;private set;}
	public SlimeStickyNode NextConnectTo{get;private set;}
	
	public Rigidbody2D rigidbody2D{get;private set;}
	public SpringJoint2D springJoint2D;
	public float Weight { get{return rigidbody2D.mass;} set{rigidbody2D.mass = value;}}

	//Resilience <-> DdampingRatio
	//Viscosity <-> Frequency
	public float Resilience{get;set;}
	public float Viscosity{get;set;}

	public void Initialize(SlimeSticky parent,SlimeStickyNode prevConnectTo,SlimeStickyNode nextConnectTo)
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		springJoint2D = GetComponent<SpringJoint2D>();

		Parent = parent;
		PrevConnectTo = prevConnectTo;
		NextConnectTo = nextConnectTo;
		springJoint2D.connectedBody = NextConnectTo.GetComponent<Rigidbody2D>();
	}

	public void FixedUpdate(){
		
	}
}
