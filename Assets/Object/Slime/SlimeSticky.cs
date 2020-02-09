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

    public void Initialize(Slime parent){
        edgeCollider2D = GetComponent<EdgeCollider2D>();

        Parent = parent;
        VertexSize = parent.STICKY_VERTEX;
        Volume = parent.STICKY_VOLUME;

        Vector2[] points_initial = new Vector2[VertexSize + 1];
        float d = Mathf.Sqrt(2f / Mathf.Sin(2f * Mathf.PI / VertexSize) * Volume / VertexSize);

        edgeCollider2D.points = Utility_Parallel.IndexedParallelFunc(VertexSize + 1,
            i => d * new Vector2(Mathf.Cos(2f * Mathf.PI * i / VertexSize),Mathf.Sin(2f * Mathf.PI * i / VertexSize))
        );
    }
}
