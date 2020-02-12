using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class SlimeSticky : MonoBehaviour2D
{
	public Slime Parent{get;private set;}
	[SerializeField]
    public GameObject prefab_SlimeStickyNode;

	EdgeCollider2D stickyHull;
	public float Volume {get;set;}
	public int VertexSize{get;private set;}

	public void Initialize(Slime parent){
		stickyHull = GetComponent<EdgeCollider2D>();
	}
	
	private void Update() {
		
	}
}
