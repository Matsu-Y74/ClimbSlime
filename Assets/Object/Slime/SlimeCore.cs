using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SlimeCore : MonoBehaviour2D
{
	public Slime Parent{get;private set;}
	CircleCollider2D circleCollider2D;

	public float CoreRadius {
		get{return circleCollider2D.radius;}
		set{circleCollider2D.radius = value;}
	}

	public void Initialize(Slime parent){
		circleCollider2D = GetComponent<CircleCollider2D>();
		Parent = parent;
	}
}
