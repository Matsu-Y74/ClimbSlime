using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Mapping;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class SlimeSticky : MonoBehaviour2D
{
	class SlimeRendering{
		SlimeSticky Parent{get;}
		MeshFilter Filter{get;}
		Vector3[] vertexs{get;}
		int[] vertexorder{get;}
		public SlimeRendering(SlimeSticky parent, MeshFilter filter){
			Parent = parent;
			Filter = filter;
			vertexs = new Vector3[Parent.VertexCount + 2];
			vertexorder = new int[3 * Parent.VertexCount];
			ParallelMap.IndexedParallelAction(i => {
				vertexorder[3 * i    ] = 0;
				vertexorder[3 * i + 1] = i + 2;
				vertexorder[3 * i + 2] = i + 1;
			},Parent.VertexCount);
		}
		public void Update() {
			Vector3 origin = Parent.transform.position;
			vertexs[0] = origin;
			SingleMap.IndexedSingleAction(Parent.slimeStickyNode,(n,i)=>
				vertexs[i + 1] = n.transform.position);
			ParallelMap.IndexedParallelAction(i => vertexs[i] -= origin,vertexs.Length);

			Mesh mesh = new Mesh();
			mesh.vertices = vertexs;
			mesh.triangles = vertexorder;
			mesh.RecalculateNormals();

			Filter.mesh = mesh;
		}
	}

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
		set{ _Viscosity = value; SingleMap.SingleAction(slimeStickyNode,s => s.Viscosity = _Viscosity,VertexCount);}
	}
	
	///<summary>
	///プロパティからの参照用.Weightを利用すること.
	///</summary>
	private float _Weight;
	public  float Weight{
		get{ return _Weight; }
		set{ _Weight = value; SingleMap.SingleAction(slimeStickyNode,s => s.Weight = _Weight / VertexCount ,VertexCount); }
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

	public float NormalRadius{get;private set;}
	public float NormalLength_NodeConnection{get;private set;}
	protected SlimeStickyNode[] slimeStickyNode{get; private set;}
	private SlimeRendering slimeRendering{get;set;}

	public void Initialize(Slime parent,int vertexcount,float volume,float weight,float viscosity){
		Parent = parent;
		VertexCount = vertexcount;
		Volume = volume;

		Vector2[] points_hull_Initial = new Vector2[VertexCount + 1];

		points_hull_Initial = ParallelMap.IndexedParallelFunc(i => 
			NormalRadius * new Vector2(Mathf.Cos(2f * Mathf.PI * i / VertexCount),Mathf.Sin(2f * Mathf.PI * i/ VertexCount))
		,VertexCount + 1);
		
		slimeStickyNode = SingleMap.IndexedSingleFunc(points_hull_Initial,(n,i) => {

			if(i != VertexCount){
				GameObject obj = GameObject.Instantiate(prefab_SlimeStickyNode,Position2D + n,Quaternion.identity,transform);
				obj.name = $"{gameObject.name}_SlimeStickyNode{i}";
				return obj.GetComponent<SlimeStickyNode>();
			}
			else{ return null; }
		},VertexCount + 1);
		slimeStickyNode[VertexCount] = slimeStickyNode[0];
		
		SingleMap.IndexedSingleAction(slimeStickyNode,(n,i) =>
		n.Initialize(i,this,points_hull_Initial[i], slimeStickyNode[(i - 1 + VertexCount) % VertexCount], slimeStickyNode[(i + 1) % VertexCount]),VertexCount);
		
		slimeRendering = new SlimeRendering(this,GetComponent<MeshFilter>());

		Weight = weight;
		Viscosity = viscosity;
	}

	void FixedUpdate() {
		Vector2[] newPos = SingleMap.SingleFunc(slimeStickyNode,n => n.Position2D, VertexCount);

		Vector2 gravityCenter = Vector2.zero;
		SingleMap.SingleAction(slimeStickyNode, n => gravityCenter += n.Position2D , VertexCount);
		Position2D = gravityCenter / VertexCount;
		
		SingleMap.SingleAction(slimeStickyNode,newPos,(n,p) => n.Position2D = p, VertexCount);
		SingleMap.IndexedSingleAction(i => {
			if(i > 0 && (slimeStickyNode[i].Position2D - Position2D).magnitude > NormalRadius * 1.5f)
				slimeStickyNode[i].Position2D = slimeStickyNode[i - 1].Position2D;
		}
		,VertexCount);

		slimeRendering.Update();

		meanvelocitylog.AddFirst(Velocity);
		while(meanvelocitylog.Count > meanvelocitylog_max)meanvelocitylog.RemoveLast();
	}
	
	public void AddForce(Vector2 force){
		SingleMap.SingleAction(slimeStickyNode,n => n.rigidbody2D.AddForce(force), VertexCount);
	}
	public void AddForce(Vector2 force,ForceMode2D mode){
		SingleMap.SingleAction(slimeStickyNode,n => n.rigidbody2D.AddForce(force,mode), VertexCount);
	}
}
