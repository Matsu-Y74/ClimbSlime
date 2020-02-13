using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviour2D : MonoBehaviour
{
	public Vector2 Position2D{
		get{ return new Vector2(transform.position.x,transform.position.y).normalized; }
		set{ transform.position = new Vector3(value.x,value.y,0); }
	}
	public Vector2 Right2D{
		get{ return new Vector2(transform.right.x,transform.right.y).normalized; }
	}
	public Vector2 Up2D{
		get{ return new Vector2(transform.up.x,transform.up.y).normalized; }
	}
}
