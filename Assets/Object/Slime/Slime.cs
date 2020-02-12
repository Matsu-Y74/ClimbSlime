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
	///inspectorからの参照用.StickyVertexを利用すること.
	///</summary>
	[SerializeField]
	public int INSPECTOR_STICKY_VERTEXSIZE = 32;

	public SlimeSticky slimeSticky{get; private set;}
	public float Weight { get; set;}
	public float StickyVolume { get{return slimeSticky.Volume;}	set{slimeSticky.Volume = value;}}
	public float StickyVertex {	get{return slimeSticky.VertexSize;}}
	
	void Awake()
	{
		Initialize();
	}

	public void Initialize(){
		Weight = INSPECTOR_WEIGHT;
		slimeSticky = transform.Find("Sticky").GetComponent<SlimeSticky>();
		slimeSticky.Initialize(this);
	}
}
