using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SlimeStickyNode : MonoBehaviour2D
{
	public SlimeSticky Parent{get;private set;}

	public SlimeStickyNode PrevConnectTo{get;private set;}
	public SlimeStickyNode NextConnectTo{get;private set;}
	
	public Rigidbody2D rigidbody2D{get;private set;}
	public HingeJoint2D hingeJoint2D;
	public float Weight { get{return rigidbody2D.mass;} set{rigidbody2D.mass = value;}}

	public void Initialize(SlimeSticky parent,SlimeStickyNode prevConnectTo,SlimeStickyNode nextConnectTo, float weight)
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		hingeJoint2D = GetComponent<HingeJoint2D>();

		Parent = parent;
		PrevConnectTo = prevConnectTo;
		NextConnectTo = nextConnectTo;
		hingeJoint2D.connectedBody = NextConnectTo.GetComponent<Rigidbody2D>();
		Weight = weight;
	}
	
	private float DefaultAngle;
	public void LinkageInitialize()
	{
		DefaultAngle = Vector2.SignedAngle(Position2D - PrevConnectTo.Position2D,NextConnectTo.Position2D - Position2D);
	}

	public void Resilience(float resilience,float viscosity){
		float delta = Vector2.SignedAngle(Position2D - PrevConnectTo.Position2D,NextConnectTo.Position2D - Position2D) - DefaultAngle;
		if(delta <= -180){ delta += 360; }
		if(delta > 180){ delta -= 360; }
		var m = new JointMotor2D();
		m.motorSpeed     = -delta * resilience;
		m.maxMotorTorque = Mathf.Abs(delta * viscosity);
		hingeJoint2D.motor = m;
	}
}
