using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIStringPartInfo : IEnumerable<char>{
	public class IEnumeratorCUIStringPartInfo : IEnumerator<char>{
		public bool streamed {get;private set;} = false;
		IEnumerator<char> Str;
		public IEnumeratorCUIStringPartInfo(string str){ Str = str.GetEnumerator(); }
		public char Current { get{return Str.Current;} }
		public void Dispose(){ Str.Dispose(); }
		object System.Collections.IEnumerator.Current{ get{return Str.Current;} }
		public bool MoveNext()
		{
			bool b = Str.MoveNext();
			if(!b) streamed = true;
			return b;
		}
		public void Reset()
		{
			Str.Reset();
			streamed = false;
		}
	}

	public IEnumeratorCUIStringPartInfo String{get;}

	public bool streamed { get{ return String.streamed; } }

	public SlimeCUI Parent{get;}
	public int CharactorStreamingFrame{get;}

	public CUIStringPartInfo(SlimeCUI parent, string str){
		Parent = parent;
		String = new IEnumeratorCUIStringPartInfo(str);
		CharactorStreamingFrame = parent.DefaultFrame_CharactorFlush_interval;
	}
	public CUIStringPartInfo(SlimeCUI parent, string str, int charactorStreamingFrame){
		Parent = parent;
		String = new IEnumeratorCUIStringPartInfo(str);
		CharactorStreamingFrame = charactorStreamingFrame;
	}

	public IEnumerator<char> GetEnumerator(){ return String; }
	IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}
public class CUIStringInfo : IEnumerable<CUIStringPartInfo>{
	public class IEnumeratorCUIStringInfo : IEnumerator<CUIStringPartInfo>{
		public bool streamed {get;private set;} = false;
		IEnumerator<CUIStringPartInfo> Strings;
		public IEnumeratorCUIStringInfo(IEnumerator<CUIStringPartInfo> strings){
			Strings = strings;
		}
		public CUIStringPartInfo Current { get{return Strings.Current;} }
		public void Dispose(){ Strings.Dispose(); }
		object System.Collections.IEnumerator.Current{ get{return Strings.Current;} }
		public bool MoveNext()
		{
			bool b = Strings.MoveNext();
			if(!b) streamed = true;
			return b;
		}
		public void Reset()
		{
			Strings.Reset();
			streamed = false;
		}
	}

	IEnumeratorCUIStringInfo Strings;

	public bool streamed { get{ return Strings.streamed; } }

	public CUIStringInfo(SlimeCUI parent, string str) : this(parent, new string[]{str}){}
	public CUIStringInfo(SlimeCUI parent, string str, int frame) : this(parent, new string[]{str}){}
	public CUIStringInfo(SlimeCUI parent, IEnumerable<string> strs){
		LinkedList<CUIStringPartInfo> l = new LinkedList<CUIStringPartInfo>();
		foreach(var s in strs){
			l.AddLast(new LinkedListNode<CUIStringPartInfo>(new CUIStringPartInfo(parent,s)));
		}
		Strings = new IEnumeratorCUIStringInfo(l.GetEnumerator());
	}
	public CUIStringInfo(SlimeCUI parent, IEnumerable<string> strs, int frame_charactorflush) {
		LinkedList<CUIStringPartInfo> l = new LinkedList<CUIStringPartInfo>();
		foreach(var s in strs){
			l.AddLast(new LinkedListNode<CUIStringPartInfo>(new CUIStringPartInfo(parent,s,frame_charactorflush)));
		}
		Strings = new IEnumeratorCUIStringInfo(l.GetEnumerator());
	}
	public CUIStringInfo(SlimeCUI parent, IEnumerable<string> strs, IEnumerable<int?> frames_charactorflush) {
		LinkedList<CUIStringPartInfo> l = new LinkedList<CUIStringPartInfo>();
		var es = strs.GetEnumerator();
		var ef = frames_charactorflush.GetEnumerator();
		while(es.MoveNext() && ef.MoveNext()){
			l.AddLast(new LinkedListNode<CUIStringPartInfo>(
				new CUIStringPartInfo(parent,es.Current,ef.Current ?? parent.DefaultFrame_CharactorFlush_interval))
			);
		}
		Strings = new IEnumeratorCUIStringInfo(l.GetEnumerator());
	}

	public IEnumerator<CUIStringPartInfo> GetEnumerator(){ return Strings; }
	IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
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
	Queue<CUIStringInfo> stringInfos = new Queue<CUIStringInfo>();
	LinkedList<SlimeCUIUnit> slimeCUIUnits = new LinkedList<SlimeCUIUnit>();
	IEnumerator Flush(){
		while(true){
			while(stringInfos.Count > 0){
				var str = stringInfos.Dequeue();
				yield return StartCoroutine(FlushString(str));
				if(Frame_StringMove != 0)
					yield return new WaitForSeconds(Math.Max(Frame_StringMove * Time.deltaTime,float.Epsilon));
			}
			yield return new WaitUntil(()=>IsFlushing);
		}
	}
	IEnumerator FlushString(CUIStringInfo stringinfo){
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
		yield return unit.Flush(stringinfo);
		yield break;
	}
	
	public CUIStringInfo Streaming(string str){
		CUIStringInfo i = new CUIStringInfo(this,str);
		stringInfos.Enqueue(i);
		return i;
	}
	public CUIStringInfo Streaming(IEnumerable<string> strings){
		CUIStringInfo i = new CUIStringInfo(this,strings);
		stringInfos.Enqueue(i);
		return i;
	}
	public CUIStringInfo Streaming(IEnumerable<string> strings,int frame_charactorflush){
		CUIStringInfo i = new CUIStringInfo(this,strings,frame_charactorflush);
		stringInfos.Enqueue(i);
		return i;
	}
	public CUIStringInfo Streaming(IEnumerable<string> strings,IEnumerable<int?> frames_charactorflush){
		CUIStringInfo i = new CUIStringInfo(this,strings,frames_charactorflush);
		stringInfos.Enqueue(i);
		return i;
	}
	public LinkedList<CUIStringInfo> Streaming(IEnumerable<Tuple<IEnumerable<string>,IEnumerable<int?>>> cuistringinfos){
		LinkedList<CUIStringInfo> l = new LinkedList<CUIStringInfo>();
		foreach(var c in cuistringinfos){
			l.AddLast(new LinkedListNode<CUIStringInfo>(Streaming(c.Item1,c.Item2)));
		}
		return l;
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
		var c = Streaming(
			new string[]{"Shutting down","... ", "OK"},
			new int?[]  {          null,    50, null}
		);
		yield return new WaitUntil(() => c.streamed);
		final();
		IsFlushing = false;
		_IsActivated = false;
		foreach(var u in slimeCUIUnits)
			Destroy(u.gameObject);
		slimeCUIUnits.Clear();
		yield break;
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!IsActivated) IsActivated = true;
			else IsActivated = false;
		}
	}
}
