using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour2D
{
	///<summary>
	///inspectorからの参照用.Weightを利用すること.
	///</summary>
	[SerializeField]
	public float INSPECTOR_WEIGHT = 30;
		
	///<summary>
	///inspectorからの参照用.Accelerationを利用すること.
	///</summary>
	[SerializeField]
	public float INSPECTOR_ACCELERATION = 100f;
	
	///<summary>
	///inspectorからの参照用.StickyVertexCountを利用すること.
	///</summary>
	[SerializeField]
	public int INSPECTOR_STICKY_VERTEXCOUNT = 32;

	///<summary>
	///inspectorからの参照用.StickyVolumeを利用すること.
	///</summary>
	[SerializeField]
	public float INSPECTOR_STICKY_VOLUME = 10f;
		
	///<summary>
	///inspectorからの参照用.Viscosityを利用すること.
	///</summary>
	[SerializeField]
	public float INSPECTOR_STICKY_VISCOSITY = 1;
	
	///<summary>
	///inspectorからの参照用.MaxVelocityを利用すること.
	///</summary>
	[SerializeField]
	public float INSPECTOR_MAXVELOCITY = 1;

	public SlimeSticky slimeSticky{get; private set;}
	public float Weight { get{return slimeSticky.Weight;} set{slimeSticky.Weight = value;}}
	
	public float Acceleration { get;set; }
	public float Viscosity { get{return slimeSticky.Viscosity;} set{slimeSticky.Viscosity = value;}}
	public float StickyVolume { get{return slimeSticky.Volume;} }
	public float StickyVertexCount { get{return slimeSticky.VertexCount;}}
	public float MaxVelocity { get; set;}
	
	public Vector2 Velocity { get{return slimeSticky.Velocity;}}
	public Vector2 MeanVelocity { get{return slimeSticky.MeanVelocity;}}
	
	public void Initialize(){
		slimeSticky = GetComponent<SlimeSticky>();
		Acceleration = INSPECTOR_ACCELERATION;
		MaxVelocity = INSPECTOR_MAXVELOCITY;
		slimeSticky.Initialize(
			this,
			INSPECTOR_STICKY_VERTEXCOUNT,
			INSPECTOR_STICKY_VOLUME,
			INSPECTOR_WEIGHT,
			INSPECTOR_STICKY_VISCOSITY);
	}
	
	void Awake()
	{
		Initialize();
	}

	public void AddForce2D(Vector2 force){ slimeSticky.AddForce(force); }
	public void AddForce2D(Vector2 force,ForceMode2D mode){	slimeSticky.AddForce(force,mode); }

	void KeyMove(){
		
		Vector2 p = Vector2.zero;
		p += (Input.GetKey(KeyCode.S)) ? Vector2.down  : Vector2.zero;
		p += (Input.GetKey(KeyCode.A)) ? Vector2.left  : Vector2.zero;
		p += (Input.GetKey(KeyCode.D)) ? Vector2.right : Vector2.zero;;

		if(!(Vector2.Dot(p,slimeSticky.MeanVelocity) >= 0 && slimeSticky.MeanVelocity.magnitude > MaxVelocity)){
			AddForce2D(Acceleration * p.normalized * (MaxVelocity - slimeSticky.MeanVelocity.magnitude)/MaxVelocity,ForceMode2D.Force);
		}
	}

	void FixedUpdate() {
		KeyMove();
	}
}