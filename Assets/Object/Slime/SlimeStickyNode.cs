using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SlimeStickyNode : MonoBehaviour2D
{
	public SlimeSticky Parent{get;private set;}

	///<summary>
	///プロパティからの参照用.RelativeToを利用すること.
	///</summary>
	private SlimeStickyNode _RelativeTo;
	public SlimeStickyNode RelativeTo{
		get{ return _RelativeTo; }
		private set{
			_RelativeTo = value;
			hingeJoint2D.connectedBody = value.GetComponent<Rigidbody2D>();
		}
	}
	
	public Rigidbody2D rigidbody2D{get;private set;}
	public HingeJoint2D hingeJoint2D{get;private set;}
	public float Weight { get{return rigidbody2D.mass;} set{rigidbody2D.mass = value;}}

	public void Initialize(SlimeSticky parent,SlimeStickyNode relativeTo, float weight)
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		hingeJoint2D = GetComponent<HingeJoint2D>();

		Parent = parent;
		RelativeTo = relativeTo;
		Weight = weight;
	}
}
