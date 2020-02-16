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
			NormalRadius = 2 * Mathf.Sqrt( _Volume / Mathf.Cos(Mathf.PI / VertexCount) / VertexCount);
			NormalLength_NodeConnection = NormalRadius * Mathf.Sin(Mathf.PI / VertexCount) * 2;
		}
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
	
	public Vector2 Velocity
	{
		get{
			Vector2 v = Vector2.zero;
			foreach(var n in slimeStickyNode) v += n.rigidbody2D.velocity;
			return v;
		}
	}
	
	int meanvelocitylog_max = 10;
	LinkedList<Vector2> meanvelocitylog = new LinkedList<Vector2>();
	public Vector2 MeanVelocity
	{
		get{
			if(meanvelocitylog.Count == 0)
				return Velocity;
			else{
				Vector2 v = Vector2.zero;
				foreach(var x in meanvelocitylog) v += x;
				return v / meanvelocitylog.Count;
			}
		}
	}

	EdgeCollider2D stickyHull;
	Vector2[] points_hull;
	public float NormalRadius{get;private set;}
	public float NormalLength_NodeConnection{get;private set;}
	protected SlimeStickyNode[] slimeStickyNode{get; private set;}

	public void Initialize(Slime parent,int vertexcount,float volume,float weight,float viscosity){
		Parent = parent;
		VertexCount = vertexcount;
		Volume = volume;

		stickyHull = GetComponent<EdgeCollider2D>();
		points_hull = new Vector2[VertexCount + 1];

		points_hull = Utility_Parallel.IndexedParallelFunc(i => 
			NormalRadius * new Vector2(Mathf.Cos(2f * Mathf.PI * i / VertexCount),Mathf.Sin(2f * Mathf.PI * i/ VertexCount))
		,VertexCount + 1);
		
		slimeStickyNode = Utility_Parallel.IndexedSingleFunc(points_hull,(n,i) => {
			if(i != VertexCount){
				GameObject obj = GameObject.Instantiate(prefab_SlimeStickyNode,n,Quaternion.identity,transform);
				obj.name = $"{gameObject.name}_SlimeStickyNode{i}";
				return obj.GetComponent<SlimeStickyNode>();
			}
			else{ return null; }
		},VertexCount + 1);
		slimeStickyNode[VertexCount] = slimeStickyNode[0];
		stickyHull.points = points_hull;
		
		Utility_Parallel.IndexedSingleAction(slimeStickyNode,(n,i) =>
		n.Initialize(i,this,points_hull[i], slimeStickyNode[(i - 1 + VertexCount) % VertexCount], slimeStickyNode[(i + 1) % VertexCount]),VertexCount);
		
		Weight = weight;
		Viscosity = viscosity;
	}

	void FixedUpdate() {
		Vector2[] newPos = Utility_Parallel.SingleFunc(slimeStickyNode,n => n.Position2D, VertexCount);

		Vector2 gravityCenter = Vector2.zero;
		Utility_Parallel.SingleAction(slimeStickyNode, n => gravityCenter += n.Position2D , VertexCount);
		Position2D = gravityCenter / VertexCount;
		Utility_Parallel.SingleAction(slimeStickyNode,newPos,(n,p) => n.Position2D = p, VertexCount);

		stickyHull.points = Utility_Parallel.SingleFunc(slimeStickyNode,n => n.LocalPosition2D);

		meanvelocitylog.AddFirst(Velocity);
		while(meanvelocitylog.Count > meanvelocitylog_max)meanvelocitylog.RemoveLast();
	}
	
	public void AddForce(Vector2 force){
		Utility_Parallel.SingleAction(slimeStickyNode,n => n.rigidbody2D.AddForce(force), VertexCount);
	}
	public void AddForce(Vector2 force,ForceMode2D mode){
		Utility_Parallel.SingleAction(slimeStickyNode,n => n.rigidbody2D.AddForce(force,mode), VertexCount);
	}
}
