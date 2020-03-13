using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCUI : MonoBehaviour
{
	[SerializeField]
	GameObject slimeCUIUnit;
	RectTransform rect;

	int Frame_StringMove              = 5;
	int Frame_CharactorFlush_interval = 2;
	int Frame_StreamFlush_interval    = 5;

	int MaxLine {get;}= 12;

	bool _IsFlushing = false;
	public bool IsFlushing{
		get{ return _IsFlushing; }
		set{
			if(value != _IsFlushing){
				if(value){
					if(coroutine_Flushing == null)
						coroutine_Flushing = StartCoroutine(Flush());
				}
				else{ StopCoroutine(coroutine_Flushing); }
				_IsFlushing = value;
			}
		}
	}
	
	void Awake() {
		rect = GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(Screen.width * 0.8f,Screen.height);
	}

	Coroutine coroutine_Flushing = null;
	LinkedList<SlimeCUIUnit> slimeCUIUnits = new LinkedList<SlimeCUIUnit>();
	IEnumerator Flush(){
		while(true){
			if(strqueue.Count > 0)
				yield return StartCoroutine(FlushString(strqueue.Dequeue()));
			yield return new WaitForSeconds(Math.Max(Frame_StreamFlush_interval * Time.deltaTime,float.Epsilon));
		}
	}
	IEnumerator FlushString(string str){
		if(slimeCUIUnits.Count >= MaxLine){
			foreach(var x in slimeCUIUnits)
				x.Move(Frame_StringMove);
		}
		yield return new WaitForSeconds(Math.Max(Frame_StringMove * Time.deltaTime,float.Epsilon));
		var unit = Instantiate(slimeCUIUnit,transform.position,Quaternion.identity,transform).GetComponent<SlimeCUIUnit>();
		unit.Initialize(this,slimeCUIUnits?.Last?.Value);
		slimeCUIUnits.AddLast(unit);
		foreach(var c in str){
			unit.text.text += c;
			yield return new WaitForSeconds(Math.Max(Frame_CharactorFlush_interval * Time.deltaTime,float.Epsilon));
		}
		yield break;
	}
	Queue<string> strqueue = new Queue<string>();
	public void Streaming<T>(T strings)where T : IEnumerable<string>{
		foreach(string s in strings)
			strqueue.Enqueue(s);
	}
	public void Streaming(params string[] strings){
		foreach(string s in strings)
			strqueue.Enqueue(s);
	}

	bool _IsActivated = false;
	public bool IsActivated{
		get{return _IsActivated;}
		set{
			if(value != _IsActivated){
				if(!_IsActivated){
					StartCoroutine(Coroutine_Activate(()=>_IsActivated = true));
				}
				else
					StartCoroutine(Coroutine_ShutDown(()=>_IsActivated = false));
			}
		}
	}

	public void Activate(Action initial){
		if(!IsActivated){
			StartCoroutine(Coroutine_Activate(initial));
		}
	}
	public void Activate(){
		if(!IsActivated){
			StartCoroutine(Coroutine_Activate());
		}
	}
	IEnumerator Coroutine_Activate(){
		yield return StartCoroutine(Coroutine_Activate(()=>{}));
	}
	IEnumerator Coroutine_Activate(Action initial){
		initial();
		IsFlushing = true;
		yield break;
	}

	public void ShutDown(){
		if(IsActivated){
			StartCoroutine(Coroutine_ShutDown());
		}
	}
	public void ShutDown(Action final){
		if(IsActivated){
			StartCoroutine(Coroutine_ShutDown(final));
		}
	}
	IEnumerator Coroutine_ShutDown(){
		yield return StartCoroutine(Coroutine_ShutDown(()=>{}));
	}
	IEnumerator Coroutine_ShutDown(Action final){
		IsFlushing = false;
		final();
		yield break;
	}

	void Update() {
		
	}
}
