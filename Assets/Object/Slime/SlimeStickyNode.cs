using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SlimeStickyNode : MonoBehaviour2D
{
	public int NodeID{get;private set;}
	public SlimeSticky Parent{get;private set;}
	public Vector2 DefaultPosition{get;private set;}

	public SlimeStickyNode PrevConnectTo{get;private set;}
	public SlimeStickyNode NextConnectTo{get;private set;}
	
	public Rigidbody2D rigidbody2D{get;private set;}
	public float Weight { get{return rigidbody2D.mass;} set{rigidbody2D.mass = value;}}

	public float Viscosity{get;set;}

	public void Initialize(
		int nodeID,
		SlimeSticky parent,
		Vector2 defaultPosition,
		SlimeStickyNode prevConnectTo,
		SlimeStickyNode nextConnectTo
	){
		rigidbody2D = GetComponent<Rigidbody2D>();

		NodeID = nodeID;
		DefaultPosition = defaultPosition;
		Parent = parent;
		PrevConnectTo = prevConnectTo;
		NextConnectTo = nextConnectTo;
	}

	private void FixedUpdate() {
		Resilience();
	}

	public void Resilience(){
		Vector2 z = Vector2.zero;
		
		z += (DefaultPosition.x * Parent.Right2D + DefaultPosition.y * Parent.Up2D - LocalPosition2D) ;
		z += (PrevConnectTo.LocalPosition2D - LocalPosition2D) * ((PrevConnectTo.LocalPosition2D - LocalPosition2D).magnitude - Parent.NormalLength_NodeConnection);
		z += (NextConnectTo.LocalPosition2D - LocalPosition2D) * ((NextConnectTo.LocalPosition2D - LocalPosition2D).magnitude - Parent.NormalLength_NodeConnection);
		
		rigidbody2D.AddForce(z / Viscosity,ForceMode2D.Force);
	}
}
