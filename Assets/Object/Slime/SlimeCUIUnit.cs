using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeCUIUnit : MonoBehaviour2D
{
	[SerializeField]
	public Text text;
	[SerializeField]
	RectTransform rect;

	public SlimeCUI      Parent{get;private set;}
	public RectTransform ParentRect{get;private set;}
	public SlimeCUIUnit  AboveString{get;private set;}

	public void Initialize(SlimeCUI parent, SlimeCUIUnit abovestring){
		Parent = parent;
		ParentRect = Parent.GetComponent<RectTransform>();
		AboveString = abovestring;
		int height = Mathf.CeilToInt(ParentRect.rect.height / 36f);
		rect.sizeDelta = new Vector2(ParentRect.rect.width,height);
		text.fontSize = Mathf.CeilToInt(Mathf.CeilToInt(height * 10f / 11f));
		if(abovestring == null)
			rect.position = (Vector2)ParentRect.position + new Vector2(0 , ParentRect.rect.height - rect.sizeDelta.y) / 2f;
		else
			rect.position = (Vector2)abovestring.rect.position - new Vector2(0 , rect.sizeDelta.y);
	}
	
	Vector2? dest = null;
	Coroutine move = null;
	public void Move(int frame){
		dest = (dest == null ? Position2D + rect.rect.height * Vector2.up : dest + rect.rect.height * Vector2.up);
		if(move != null)
			StopCoroutine(move);
		move = StartCoroutine(MoveCoroutine(frame,dest.Value));
	}
	IEnumerator MoveCoroutine(int frame,Vector2 destination){
		Vector2 f = Position2D;
		Vector2 t = destination;
		for(int i = 1; i < frame; i++){
			Position2D = (f * (frame - i) + destination * i) / frame;
			yield return null;
		}
		Position2D = destination;
		dest = null;
		yield break;
	}

	Coroutine stream = null;
	public Coroutine Stream(Queue<CUIStringInfo> stringinfo){
		if(stream != null)
			StopCoroutine(stream);
		stream = StartCoroutine(StreamCoroutine(stringinfo));
		return stream;
	}
	IEnumerator StreamCoroutine(Queue<CUIStringInfo> stringinfo){
		while(stringinfo.Count > 0){
			var unit = stringinfo.Dequeue();
			int frame = unit.CharactorStreamingFrame ?? Parent.DefaultFrame_CharactorFlush_interval;
			foreach(var c in unit.String){
				text.text += c;
				if(frame != 0)
					yield return new WaitForSeconds(Math.Max(frame * Time.deltaTime,float.Epsilon));
			}
		}
		yield break;
	}
}
