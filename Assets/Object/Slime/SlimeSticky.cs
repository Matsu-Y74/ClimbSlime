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
	public float Volume {get;set;}
	public int VertexCount{get;private set;}
	public float Resilience{get; set;}
	public float Viscosity{get; set;}
	
	///<summary>
	///プロパティからの参照用.Weightを利用すること.
	///</summary>
	private float _Weight;
	public  float Weight{
		get{
			return _Weight;
		}
		set{
			_Weight = value;
			foreach(var x in slimeStickyNode){
				x.Weight = _Weight / VertexCount;
			}
		}
	}
	
	EdgeCollider2D stickyHull;
	Vector2[] points_hull;
	protected SlimeStickyNode[] slimeStickyNode{get; private set;}

	public void Initialize(Slime parent,float volume,int vertexcount,float weight,float resilience,float viscosity){
		Parent = parent;
		Volume = volume;
		VertexCount = vertexcount;
		Resilience = resilience;
		Viscosity = viscosity;

		stickyHull = GetComponent<EdgeCollider2D>();
		points_hull = new Vector2[VertexCount + 1];

		float r = Mathf.Sqrt(Volume * 2f / Mathf.Sin(2f * Mathf.PI / VertexCount) / VertexCount);
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

		_Weight = weight;
		
		Utility_Parallel.IndexedSingleAction(slimeStickyNode,(p,i) =>
		p.Initialize(this, slimeStickyNode[(i - 1 + VertexCount) % VertexCount], slimeStickyNode[(i + 1) % VertexCount],weight),VertexCount);
		Utility_Parallel.SingleAction(slimeStickyNode, n => n.LinkageInitialize() ,VertexCount);
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
		Utility_Parallel.SingleAction(slimeStickyNode, n => n.Resilience(Resilience,Viscosity) ,VertexCount);
	}
}
