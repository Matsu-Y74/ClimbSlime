# if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
using UnityEditor;

//https://kan-kikuchi.hatenablog.com/entry/CustomEditor_Button
//https://qiita.com/Teach/items/1e7f5d9db0a377ceb9a5

public abstract class GenerativeTilemap : MonoBehaviour {
	public abstract TileBase Generate(Vector3Int position);
}

[CustomEditor(typeof(GenerativeTilemap))]
public abstract class GenerativeTilemapEditor : Editor {
	Vector3Int begin = new Vector3Int(-100,-100,0);
	Vector3Int end   = new Vector3Int( 100, 100,0);
	public abstract void GeneratorInitialize();
	public override void OnInspectorGUI(){
		base.OnInspectorGUI();
		begin = EditorGUILayout.Vector3IntField("Generateion Field Begin", begin);
		end   = EditorGUILayout.Vector3IntField("Generateion Field End",end);
		if (GUILayout.Button("Generate")){ Generate(begin,end); }
	}
	void Generate(Vector3Int b, Vector3Int e){
		GeneratorInitialize();
		GenerativeTilemap gtilemap = ((GenerativeTilemap)target);
		Tilemap tilemap = gtilemap.GetComponent<Tilemap>();
		tilemap.ClearAllTiles();
		for(int z = Math.Min(b.z,e.z);z <= Math.Max(b.z,e.z);z++){
			for(int y = Math.Min(b.y,e.y);y <= Math.Max(b.y,e.y);y++){
				for(int x = Math.Min(b.x,e.x);x <= Math.Max(b.x,e.x);x++){
					tilemap.SetTile(new Vector3Int(x,y,z),gtilemap.Generate(new Vector3Int(x,y,z)));
				}
			}
		}
	}
}
# endif