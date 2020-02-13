using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour2D
{
	///<summary>
	///inspectorからの参照用.Weightを利用すること.
	///</summary>
	[SerializeField]
	public float INSPECTOR_WEIGHT = 30;
	
	///<summary>
	///inspectorからの参照用.StickyVertexCountを利用すること.
	///</summary>
	[SerializeField]
	public int INSPECTOR_STICKY_VERTEXCOUNT = 32;

	///<summary>
	///inspectorからの参照用.StickyVolumeを利用すること.
	///</summary>
	[SerializeField]
	public float INSPECTOR_STICKY_VOLUME = 10f;
	
	///<summary>
	///inspectorからの参照用.Resilienceを利用すること.
	///</summary>
	[SerializeField]
	public float INSPECTOR_STICKY_RESILIENCE = 1;
	
	///<summary>
	///inspectorからの参照用.Viscosityを利用すること.
	///</summary>
	[SerializeField]
	public float INSPECTOR_STICKY_VISCOSITY = 1;

	public SlimeSticky slimeSticky{get; private set;}
	public float Weight { get{return slimeSticky.Weight;} set{slimeSticky.Weight = value;}}
	public float Resilience { get{return slimeSticky.Resilience;} set{slimeSticky.Resilience = value;}}
	public float Viscosity { get{return slimeSticky.Viscosity;} set{slimeSticky.Viscosity = value;}}
	public float StickyVolume { get{return slimeSticky.Volume;} }
	public float StickyVertexCount { get{return slimeSticky.VertexCount;}}
	
	public void Initialize(){
		slimeSticky = GetComponent<SlimeSticky>();
		slimeSticky.Initialize(
			this,
			INSPECTOR_STICKY_VERTEXCOUNT,
			INSPECTOR_STICKY_VOLUME,
			INSPECTOR_WEIGHT,
			INSPECTOR_STICKY_RESILIENCE,
			INSPECTOR_STICKY_VISCOSITY);
	}
	
	void Awake()
	{
		Initialize();
	}
}