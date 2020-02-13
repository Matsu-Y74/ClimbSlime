using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class SlimeSticky : MonoBehaviour2D
{
	[SerializeField]
    public GameObject prefab_SlimeStickyNode;

	public Slime Parent{get;private set;}
	public int VertexCount{get;private set;}

	///<summary>
	///プロパティからの参照用.Volumeを利用すること.
	///</summary>
	private float _Volume;
	public float Volume{
		get{ return _Volume; }
		set{
			_Volume = value; 
			float l = 2 * Mathf.Sqrt( _Volume / Mathf.Cos(Mathf.PI / VertexCount) / VertexCount);
			Utility_Parallel.SingleAction(slimeStickyNode,s => s.springJoint2D.distance = l);
		}
	}
	
	///<summary>
	///プロパティからの参照用.Resilienceを利用すること.
	///</summary>
	private float _Resilience;
	public float Resilience{
		get{ return _Resilience; }
		set{ _Resilience = value; Utility_Parallel.SingleAction(slimeStickyNode,s => s.Resilience = _Resilience,VertexCount); }
	}

	///<summary>
	///プロパティからの参照用.Viscosityを利用すること.
	///</summary>
	private float _Viscosity;
	public float Viscosity{
		get{ return _Viscosity; }
		set{ _Viscosity = value; Utility_Parallel.SingleAction(slimeStickyNode,s => s.Viscosity = _Viscosity,VertexCount);}
	}
	
	///<summary>
	///プロパティからの参照用.Weightを利用すること.
	///</summary>
	private float _Weight;
	public  float Weight{
		get{ return _Weight; }
		set{ _Weight = value; Utility_Parallel.SingleAction(slimeStickyNode,s => s.Weight = _Weight / VertexCount ,VertexCount); }
	}
	
	EdgeCollider2D stickyHull;
	Vector2[] points_hull;
	protected SlimeStickyNode[] slimeStickyNode{get; private set;}

	public void Initialize(Slime parent,int vertexcount,float volume,float weight,float resilience,float viscosity){
		Parent = parent;
		VertexCount = vertexcount;

		stickyHull = GetComponent<EdgeCollider2D>();
		points_hull = new Vector2[VertexCount + 1];

		float r = Mathf.Sqrt(volume * 2f / Mathf.Sin(2f * Mathf.PI / VertexCount) / VertexCount);
		points_hull = Utility_Parallel.IndexedParallelFunc(i => 
			r * new Vector2(Mathf.Cos(2f * Mathf.PI * i / VertexCount),Mathf.Sin(2f * Mathf.PI * i/ VertexCount))
		,VertexCount + 1);
		
		slimeStickyNode = Utility_Parallel.IndexedSingleFunc(points_hull,(p,i) => {
			if(i != VertexCount){
				GameObject obj = GameObject.Instantiate(prefab_SlimeStickyNode,p,Quaternion.identity,transform);
				obj.name = $"{gameObject.name}_SlimeStickyNode{i}";
				return obj.GetComponent<SlimeStickyNode>();
			}
			else{
				return null;
			}
		},VertexCount + 1);
		slimeStickyNode[VertexCount] = slimeStickyNode[0];
		stickyHull.points = points_hull;
		
		Utility_Parallel.IndexedSingleAction(slimeStickyNode,(p,i) =>
		p.Initialize(this, slimeStickyNode[(i - 1 + VertexCount) % VertexCount], slimeStickyNode[(i + 1) % VertexCount]),VertexCount);
		
		Volume = volume;
		Weight = weight;
		Resilience = resilience;
		Viscosity = viscosity;
	}

	private void FixedUpdate() {
		Vector2 grav = Vector2.zero;
		foreach(var x in slimeStickyNode){
			grav += x.Position2D / VertexCount;
		}
		Vector2 delta = grav - Position2D;
		transform.position = grav;

		Utility_Parallel.SingleAction(slimeStickyNode,x => x.Position2D -= delta,VertexCount);

		points_hull = Utility_Parallel.SingleFunc(slimeStickyNode,p=>p.Position2D-Position2D);
		stickyHull.points = points_hull;
	}
}
