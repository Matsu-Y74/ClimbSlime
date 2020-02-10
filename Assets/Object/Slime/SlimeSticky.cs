using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class SlimeSticky : MonoBehaviour2D
{
	public Slime Parent{get;private set;}

	EdgeCollider2D edgeCollider2D;
	public float Volume {get;set;}
	public int VertexSize{get;private set;}
	public Vector2[] direction;

	public void Initialize(Slime parent){
		edgeCollider2D = GetComponent<EdgeCollider2D>();

		Parent = parent;
		VertexSize = parent.STICKY_VERTEX;
		Volume = parent.STICKY_VOLUME;

		Vector2[] points_initial = new Vector2[VertexSize + 1];

		direction = Utility_Parallel.IndexedParallelFunc(VertexSize,
			i => new Vector2(Mathf.Cos(2f * Mathf.PI * i / VertexSize),Mathf.Sin(2f * Mathf.PI * i / VertexSize))
		);

		float d = Mathf.Sqrt(2f / Mathf.Sin(2f * Mathf.PI / VertexSize) * Volume / VertexSize);
		edgeCollider2D.points = Utility_Parallel.ParallelFunc(direction, x => d * x );
	}

	public Vector2?[] CollisionPointScan(){
		float   radius = Parent.slimeCore.CoreRadius + float.Epsilon;
		Vector2 origin = Parent.slimeCore.Position2D;
		RaycastHit2D[] Hits =  Utility_Parallel.ParallelFunc(direction, d =>
			Physics2D.Raycast(origin + d * radius, d)
		);
		return Utility_Parallel.ParallelFunc(Hits, hit => hit.collider ? new Vector2?(hit.point) : null);
	}

	public void DistanceDifferences(){
		distances = Utility_Parallel.ParallelFunc(edgeCollider2D.points, CollisionPointScan(),);
	}
	
	public void GeometryUpdate(){
		
	}
}
