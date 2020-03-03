# if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GenerativeTilemap_PerlinNoise : GenerativeTilemap
{
	[SerializeField]
	public TileBase tile;
	internal Vector2 d;
	internal Vector2 s;
	internal float t;
	public override TileBase Generate(Vector3Int p){
		return Mathf.PerlinNoise(s.x * p.x + d.x, s.y * p.y + d.y) >= t ? tile : null;
	}
}

[CustomEditor(typeof(GenerativeTilemap_PerlinNoise))]
public class GenerativeTilemap_PerlinNoiseEditor : GenerativeTilemapEditor {
	Vector2 d = new Vector2(0,0);
	Vector2 s = new Vector2(1,1);
	float t   = 0.5f;
	public override void GeneratorInitialize(){
		GenerativeTilemap_PerlinNoise trgt = (GenerativeTilemap_PerlinNoise)target;
		trgt.d = d;
		trgt.s = s;
		trgt.t = t;
	}
	public override void OnInspectorGUI(){
		d = EditorGUILayout.Vector2Field("PerlinNoise 2D Difference",d);
		s = EditorGUILayout.Vector2Field("PerlinNoise 2D Factor",s);
		//"PerlinNoise Threadhold"
		t = GUILayout.HorizontalSlider(t,0,1);
		base.OnInspectorGUI();
	}
}
# endif