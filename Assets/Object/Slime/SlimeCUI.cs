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
	public Text _displaedtext;
	public Text displaedtext{
		get{ return _displaedtext; }
		set{
			_displaedtext = value;
			_displaedtext.color = color;
		}
	}
	public Color _color;
	public Color color{
		get{ return _color; }
		set{
			_color = value;
			if(displaedtext != null)
				displaedtext.color = value;
		}
	}

	public Action<CUIStringInfo> Action_Final;

	public bool streamed { get{ return Strings.streamed; } }

	public CUIStringInfo(SlimeCUI parent, string str, Color col) : this(parent, new string[]{str}, col, null){}
	public CUIStringInfo(SlimeCUI parent, string str, int frame, Color col) : this(parent, new string[]{str}, frame, col, null){}
	public CUIStringInfo(SlimeCUI parent, IEnumerable<string> strs, Color col) : this(parent, strs, col, null){}
	public CUIStringInfo(SlimeCUI parent, IEnumerable<string> strs, int frame_charactorflush, Color col)  : this(parent, strs, frame_charactorflush, col, null){}
	public CUIStringInfo(SlimeCUI parent, IEnumerable<string> strs, IEnumerable<int?> frames_charactorflush, Color col)  : this(parent,strs, frames_charactorflush, col, null){}

	public CUIStringInfo(SlimeCUI parent, string str, Color col, Action<CUIStringInfo> action_Final) : this(parent, new string[]{str}, col, action_Final){}
	public CUIStringInfo(SlimeCUI parent, string str, int frame, Color col, Action<CUIStringInfo> action_Final) : this(parent, new string[]{str}, frame, col, action_Final){}
	public CUIStringInfo(SlimeCUI parent, IEnumerable<string> strs, Color col, Action<CUIStringInfo> action_Final){
		color = col;
		Action_Final = action_Final;
		LinkedList<CUIStringPartInfo> l = new LinkedList<CUIStringPartInfo>();
		foreach(var s in strs){
			l.AddLast(new LinkedListNode<CUIStringPartInfo>(new CUIStringPartInfo(parent,s)));
		}
		Strings = new IEnumeratorCUIStringInfo(l.GetEnumerator());
	}
	public CUIStringInfo(SlimeCUI parent, IEnumerable<string> strs, int frame_charactorflush, Color col, Action<CUIStringInfo> action_Final) {
		color = col;
		Action_Final = action_Final;
		LinkedList<CUIStringPartInfo> l = new LinkedList<CUIStringPartInfo>();
		foreach(var s in strs){
			l.AddLast(new LinkedListNode<CUIStringPartInfo>(new CUIStringPartInfo(parent,s,frame_charactorflush)));
		}
		Strings = new IEnumeratorCUIStringInfo(l.GetEnumerator());
	}
	public CUIStringInfo(SlimeCUI parent, IEnumerable<string> strs, IEnumerable<int?> frames_charactorflush, Color col, Action<CUIStringInfo> action_Final) {
		color = col;
		Action_Final = action_Final;
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
	public Color DefaultStreamColor {get;}= Color.white;

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
			while(slimeCUIUnits.Count >= MaxLine){
				Destroy(slimeCUIUnits.First.Value.gameObject);
				slimeCUIUnits.RemoveFirst();
			}
		}
		var unit = Instantiate(slimeCUIUnit,transform.position,Quaternion.identity,transform).GetComponent<SlimeCUIUnit>();
		unit.Initialize(this,slimeCUIUnits?.Last?.Value);
		slimeCUIUnits.AddLast(unit);
		yield return unit.Flush(stringinfo);
		yield break;
	}
	
	public CUIStringInfo Streaming(string str){ return Streaming(str, DefaultStreamColor); }
	public CUIStringInfo Streaming(IEnumerable<string> strings){ return Streaming(strings, DefaultStreamColor); }
	public CUIStringInfo Streaming(IEnumerable<string> strings,int frame_charactorflush){ return Streaming(strings, frame_charactorflush, DefaultStreamColor); }
	public CUIStringInfo Streaming(IEnumerable<string> strings,IEnumerable<int?> frames_charactorflush){ return Streaming(strings, frames_charactorflush, DefaultStreamColor); }
	public LinkedList<CUIStringInfo> Streaming(IEnumerable<Tuple<IEnumerable<string>,IEnumerable<int?>>> cuistringinfos){
		LinkedList<Tuple<IEnumerable<string>,IEnumerable<int?>,Color?>> l = new LinkedList<Tuple<IEnumerable<string>, IEnumerable<int?>, Color?>>();
		foreach(var x in cuistringinfos)
			l.AddLast(new LinkedListNode<Tuple<IEnumerable<string>, IEnumerable<int?>, Color?>>(
				new Tuple<IEnumerable<string>, IEnumerable<int?>, Color?>(x.Item1,x.Item2,null)
			));
		return Streaming(l);
	}

	public CUIStringInfo Streaming(string str, Color color){
		CUIStringInfo i = new CUIStringInfo(this,str,color);
		stringInfos.Enqueue(i);
		return i;
	}
	public CUIStringInfo Streaming(IEnumerable<string> strings, Color color){
		CUIStringInfo i = new CUIStringInfo(this,strings,color);
		stringInfos.Enqueue(i);
		return i;
	}
	public CUIStringInfo Streaming(IEnumerable<string> strings,int frame_charactorflush, Color color){
		CUIStringInfo i = new CUIStringInfo(this,strings,frame_charactorflush,color);
		stringInfos.Enqueue(i);
		return i;
	}
	public CUIStringInfo Streaming(IEnumerable<string> strings,IEnumerable<int?> frames_charactorflush, Color color){
		CUIStringInfo i = new CUIStringInfo(this,strings,frames_charactorflush,color);
		stringInfos.Enqueue(i);
		return i;
	}
	public LinkedList<CUIStringInfo> Streaming(IEnumerable<Tuple<IEnumerable<string>,IEnumerable<int?>,Color?>> cuistringinfos){
		LinkedList<CUIStringInfo> l = new LinkedList<CUIStringInfo>();
		foreach(var c in cuistringinfos){
			l.AddLast(new LinkedListNode<CUIStringInfo>(
				Streaming(
					c.Item1,
					c.Item2,
					c.Item3.HasValue ? c.Item3.Value : DefaultStreamColor
				)
			));
		}
		return l;
	}

	public CUIStringInfo Streaming(string str, Action<CUIStringInfo> action){ return Streaming(str, DefaultStreamColor); }
	public CUIStringInfo Streaming(IEnumerable<string> strings, Action<CUIStringInfo> action){ return Streaming(strings, DefaultStreamColor); }
	public CUIStringInfo Streaming(IEnumerable<string> strings,int frame_charactorflush, Action<CUIStringInfo> action){ return Streaming(strings, frame_charactorflush, DefaultStreamColor); }
	public CUIStringInfo Streaming(IEnumerable<string> strings,IEnumerable<int?> frames_charactorflush, Action<CUIStringInfo> action){ return Streaming(strings, frames_charactorflush, DefaultStreamColor); }
	public LinkedList<CUIStringInfo> Streaming(IEnumerable<Tuple<IEnumerable<string>,IEnumerable<int?>>> cuistringinfos, Action<CUIStringInfo> action){
		LinkedList<Tuple<IEnumerable<string>,IEnumerable<int?>,Color?>> l = new LinkedList<Tuple<IEnumerable<string>, IEnumerable<int?>, Color?>>();
		foreach(var x in cuistringinfos)
			l.AddLast(new LinkedListNode<Tuple<IEnumerable<string>, IEnumerable<int?>, Color?>>(
				new Tuple<IEnumerable<string>, IEnumerable<int?>, Color?>(x.Item1,x.Item2,null)
			));
		return Streaming(l);
	}

	public CUIStringInfo Streaming(string str, Color color, Action<CUIStringInfo> action){
		CUIStringInfo i = new CUIStringInfo(this,str,color,action);
		stringInfos.Enqueue(i);
		return i;
	}
	public CUIStringInfo Streaming(IEnumerable<string> strings, Color color, Action<CUIStringInfo> action){
		CUIStringInfo i = new CUIStringInfo(this,strings,color,action);
		stringInfos.Enqueue(i);
		return i;
	}
	public CUIStringInfo Streaming(IEnumerable<string> strings,int frame_charactorflush, Color color, Action<CUIStringInfo> action){
		CUIStringInfo i = new CUIStringInfo(this,strings,frame_charactorflush,color,action);
		stringInfos.Enqueue(i);
		return i;
	}
	public CUIStringInfo Streaming(IEnumerable<string> strings,IEnumerable<int?> frames_charactorflush, Color color, Action<CUIStringInfo> action){
		CUIStringInfo i = new CUIStringInfo(this,strings,frames_charactorflush,color,action);
		stringInfos.Enqueue(i);
		return i;
	}
	public LinkedList<CUIStringInfo> Streaming(IEnumerable<Tuple<IEnumerable<string>,IEnumerable<int?>,Color?>> cuistringinfos, Action<CUIStringInfo> action){
		LinkedList<CUIStringInfo> l = new LinkedList<CUIStringInfo>();
		foreach(var c in cuistringinfos){
			l.AddLast(new LinkedListNode<CUIStringInfo>(
				Streaming(
					c.Item1,
					c.Item2,
					c.Item3.HasValue ? c.Item3.Value : DefaultStreamColor
				)
			));
		}
		l.Last.Value.Action_Final = action;
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
		if(coroutine_Flushing == null)
			yield return StartCoroutine(Coroutine_FirstActivate(initial));
		else{
			IsFlushing = true;
			_IsActivated = true;
			initial();
			Streaming(				"");
			Streaming(				"SETTING UP STARTED");
			Streaming(new string[]{	" Network Accessing" ,"...", "FAILED"}, new int?[]{null, 40, null},DefaultStreamColor, (x) => x.color = Color.red);
			Streaming(				"SETTING UP FINISHED");
			Streaming(				"");
			Streaming(				"ENMIRAI Biocomputing-OS");
			Streaming(				"");
			Streaming(				"ENMIRAI Laboratories 2020 - [OUTOFRANGE EXCEPTION: YEAR_EXPIRED]", Color.red);
			Streaming(				"all rights (including knowledge and recognition) are reserved.");
			Streaming(				"");
			Streaming(				"Unauthorized manipuration will be TRACED, MONITORED, and TERMINATED.");
			Streaming(				"");
			yield break;
		}
	}
	IEnumerator Coroutine_FirstActivate(Action initial){
		IsFlushing = true;
		_IsActivated = true;
		initial();
		Streaming(				"");
		Streaming(				"SETTING UP STARTED");
		Streaming(new string[]{	" Organic Interface"				,"...", "OK"    }, new int?[]{null, 40, null});
		Streaming(new string[]{	" Organic Energy Attach"			,"...", "OK"    }, new int?[]{null, 40, null});
		Streaming(new string[]{	" Kernel Initialize"				,"...", "OK"    }, new int?[]{null, 10, null});
		Streaming(new string[]{	" Memory and Storage Setting up"	,"...", "OK"    }, new int?[]{null, 20, null});
		Streaming(new string[]{	" Memory Surpervisor"				,"...", "OK"    }, new int?[]{null, 20, null});
		Streaming(new string[]{	" Kernel Finalize"					,"...", "OK"    }, new int?[]{null, 10, null});
		Streaming(new string[]{	" Network Accessing"				,"...", "FAILED"}, new int?[]{null, 40, null}, DefaultStreamColor,(t) => t.color = Color.red);
		Streaming(new string[]{	" Organ-Computer synthesize"		,"...", "OK"    }, new int?[]{null, 20, null});
		Streaming(				"SETTING UP FINISHED");
		Streaming(				"");
		Streaming(				"ENMIRAI Biocomputing-OS");
		Streaming(				"");
		Streaming(				"ENMIRAI Laboratories 2020 - [OUTOFRANGE EXCEPTION: YEAR_EXPIRED]", Color.red);
		Streaming(				"all rights (including knowledge and recognition) are reserved.");
		Streaming(				"");
		Streaming(				"Unauthorized manipuration will be possibility to be TRACED, MONITORED, and TERMINATED.");
		Streaming(				"");
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
		stringInfos.Clear();
		yield break;
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space)){
			if(!IsActivated) IsActivated = true;
			else IsActivated = false;
		}
	}
}
