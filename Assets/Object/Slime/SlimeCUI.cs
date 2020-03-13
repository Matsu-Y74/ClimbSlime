using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIStringInfo{
	public string String{get;}
	public int? CharactorStreamingFrame{get;}

	public CUIStringInfo(string str){
		String = str;
		CharactorStreamingFrame = null;
	}
	
	public CUIStringInfo(string str, int? charactorStreamingFrame){
		String = str;
		CharactorStreamingFrame = charactorStreamingFrame;
	}
}

public class SlimeCUI : MonoBehaviour
{
	[SerializeField]
	GameObject slimeCUIUnit;
	RectTransform rect;

	public int MaxLine {get;}= 12;

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
		rect.sizeDelta = new Vector2(Screen.width * 0.9f,Screen.height);
	}

	public int DefaultFrame_StreamFlush_interval {get;} = 30;
	public int DefaultFrame_CharactorFlush_interval {get;} = 0;

	int Frame_StringMove = 1;
	Coroutine coroutine_Flushing = null;
	Queue<Queue<CUIStringInfo>> stringInfos = new Queue<Queue<CUIStringInfo>>();
	LinkedList<SlimeCUIUnit> slimeCUIUnits = new LinkedList<SlimeCUIUnit>();
	IEnumerator Flush(){
		while(true){
			while(stringInfos.Count > 0){
				yield return StartCoroutine(FlushString(stringInfos.Dequeue()));
				if(Frame_StringMove != 0)
					yield return new WaitForSeconds(Math.Max(Frame_StringMove * Time.deltaTime,float.Epsilon));
			}
			yield return null;
		}
	}
	IEnumerator FlushString(Queue<CUIStringInfo> stringinfo){
		if(slimeCUIUnits.Count >= MaxLine){
			foreach(var x in slimeCUIUnits){
				x.Move(Frame_StringMove);
			}
			if(Frame_StringMove != 0)
				yield return new WaitForSeconds(Math.Max(Frame_StringMove * Time.deltaTime,float.Epsilon));
		}
		var unit = Instantiate(slimeCUIUnit,transform.position,Quaternion.identity,transform).GetComponent<SlimeCUIUnit>();
		unit.Initialize(this,slimeCUIUnits?.Last?.Value);
		slimeCUIUnits.AddLast(unit);
		yield return unit.Stream(stringinfo);
		yield break;
	}
	
	public void Streaming(string strings) { Streaming(new string[]{strings}); }
	public void Streaming(IEnumerable<string> strings) { Streaming(strings,DefaultFrame_CharactorFlush_interval); }
	public void Streaming(IEnumerable<string> strings,int? frame_charactorflush){
		Queue<CUIStringInfo> q = new Queue<CUIStringInfo>();
		foreach(var s in strings)
			q.Enqueue(new CUIStringInfo(s,frame_charactorflush));
		
		stringInfos.Enqueue(q);
	}
	public void Streaming(IEnumerable<string> strings,IEnumerable<int?> frames_charactorflush){
		Queue<CUIStringInfo> q = new Queue<CUIStringInfo>();
		var es = strings.GetEnumerator();
		var ef = frames_charactorflush.GetEnumerator();
		while(es.MoveNext() && ef.MoveNext()){
			q.Enqueue(new CUIStringInfo(es.Current,ef.Current));
		}
		stringInfos.Enqueue(q);
	}
	public void Streaming(IEnumerable<IEnumerable<CUIStringInfo>> cuistringinfos){
		foreach(var c in cuistringinfos)Streaming(c);
	}
	public void Streaming(IEnumerable<CUIStringInfo> cuistringinfo){
		stringInfos.Enqueue(new Queue<CUIStringInfo>(cuistringinfo));
	}

	bool _IsActivated = false;
	public bool IsActivated{
		get{return _IsActivated;}
		set{
			if(value != _IsActivated){
				if(!_IsActivated){
					StartCoroutine(Coroutine_Activate());
				}
				else
					StartCoroutine(Coroutine_ShutDown());
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
		_IsActivated = true;
		IsFlushing = true;
		initial();
		Streaming("");
		Streaming("ENMIRAI Biocomputing-OS");
		Streaming("");
		Streaming("ENMIRAI Laboratories 2020 - [OUTOFRANGE EXCEPTION: YEAR_EXPIRED]");
		Streaming("all rights (including knowledge and recognition) are reserved.");
		Streaming("");
		Streaming(
			new string[]{"Initializing","... ", "OK"},
			new int?[]  {          null,    50, null}
		);
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
		Streaming(
			new string[]{"Shutting down","... ", "OK"},
			new int?[]  {          null,    50, null}
		);
		yield return coroutine_Flushing;
		final();
		IsFlushing = false;
		_IsActivated = false;
		yield break;
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!IsActivated) Activate();
			else ShutDown();
		}
	}
}
