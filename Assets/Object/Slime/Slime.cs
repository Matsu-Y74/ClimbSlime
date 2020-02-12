using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour2D
{
	[SerializeField]
	///<summary>
	///inspectorからの参照用.Weightを利用すること.
	///</summary>
	public float INSPECTOR_WEIGHT = 30;
	
	///<summary>
	///inspectorからの参照用.StickyVolumeを利用すること.
	///</summary>
	[SerializeField]
	public float INSPECTOR_STICKY_VOLUME = 10f;
	
	///<summary>
	///inspectorからの参照用.StickyVertexCountを利用すること.
	///</summary>
	[SerializeField]
	public int INSPECTOR_STICKY_VERTEXCOUNT = 32;

	public SlimeSticky slimeSticky{get; private set;}
	public float Weight { get{return slimeSticky.Weight;} set{slimeSticky.Weight = value;}}
	public float StickyVolume { get{return slimeSticky.Volume;} set{slimeSticky.Volume = value;}}
	public float StickyVertexCount { get{return slimeSticky.VertexCount;}}
	
	public void Initialize(){
		slimeSticky = GetComponent<SlimeSticky>();
		slimeSticky.Initialize(this,INSPECTOR_STICKY_VOLUME,INSPECTOR_STICKY_VERTEXCOUNT,INSPECTOR_WEIGHT);

		Weight = INSPECTOR_WEIGHT;
	}
	
	void Awake()
	{
		Initialize();
	}
}