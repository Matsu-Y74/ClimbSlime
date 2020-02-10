using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

static class Utility_Parallel{	
		public static TResult[] SingleFunc<TResult>(Func<TResult> f,int length){
		TResult[] r = new TResult[length];
		for(int i = 0; i < length ; i++) r[i] = f();
		return r;
	}
	
	public static TResult[] SingleFunc<T,TResult>(T[] array, Func<T,TResult> f){
		return SingleFunc(array,f,array.Length);
	}
	public static TResult[] SingleFunc<T,TResult>(T[] array, Func<T,TResult> f, int length){
		try{
			if(array.Length < length){throw new Exception($"length {length} is larger than array's length {array.Length}.");}
			else{
				TResult[] r = new TResult[length];
				for(int i = 0; i < length ; i++) r[i] = f(array[i]);
				return r;
			}
		}
		catch(Exception e){throw e;}
	}
	public static TResult[] SingleFunc<T,TResult>(List<T> list, Func<T,TResult> f){
		return SingleFunc(list,f,list.Count);
	}
	public static TResult[] SingleFunc<T,TResult>(List<T> list, Func<T,TResult> f, int length){
		try{
			if(list.Count < length){throw new Exception($"length {length} is larger than list's length {list.Count}.");}
			else{
				TResult[] r = new TResult[length];
				for(int i = 0; i < length ; i++) r[i] = f(list[i]);
				return r;
			}
		}
		catch(Exception e){throw e;}
	}

	public static TResult[] SingleFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<T1,T2,TResult> f){
		return SingleFunc(array1,array2,f,Math.Min(array1.Length,array2.Length));	
	}
	public static TResult[] SingleFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<T1,T2,TResult> f, int length)
	{
		try{
			if(Math.Min(array1.Length,array2.Length) < length){
				throw new Exception($"length {length} is larger than arrays' length {Math.Min(array1.Length,array2.Length)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				for(int i = 0; i < length ; i++) rs[i] = f(array1[i],array2[i]);
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}
	public static TResult[] SingleFunc<T1,T2,TResult>(List<T1> list1, List<T2> list2, Func<T1,T2,TResult> f){
		return SingleFunc(list1,list2,f,Math.Min(list1.Count,list2.Count));	
	}
	public static TResult[] SingleFunc<T1,T2,TResult>(List<T1> list1, List<T2> list2, Func<T1,T2,TResult> f, int length)
	{
		try{
			if(Math.Min(list1.Count,list2.Count) < length){
				throw new Exception($"length {length} is larger than lists' length {Math.Min(list1.Count,list2.Count)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				for(int i = 0; i < length ; i++) rs[i] = f(list1[i],list2[i]);
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}

	public static TResult[] SingleFunc<T1,T2,T3,TResult>(T1[] array1, T2[] array2, T3[] array3, Func<T1,T2,T3,TResult> f){
		return SingleFunc(array1,array2,array3,f,Math.Min(Math.Min(array1.Length,array2.Length),array3.Length));	
	}
	public static TResult[] SingleFunc<T1,T2,T3,TResult>(T1[] array1, T2[] array2, T3[] array3, Func<T1,T2,T3,TResult> f, int length)
	{
		try{
			if(Math.Min(Math.Min(array1.Length,array2.Length),array3.Length) < length){
				throw new Exception($"length {length} is larger than arrays' length {Math.Min(Math.Min(array1.Length,array2.Length),array3.Length)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				for(int i = 0; i < length; i++) rs[i] = f(array1[i],array2[i],array3[i]);
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}
	public static TResult[] SingleFunc<T1,T2,T3,TResult>(List<T1> list1, List<T2> list2, List<T3> list3, Func<T1,T2,T3,TResult> f){
		return SingleFunc(list1,list2,list3,f,Math.Min(Math.Min(list1.Count,list2.Count),list3.Count));	
	}
	public static TResult[] SingleFunc<T1,T2,T3,TResult>(List<T1> list1, List<T2> list2, List<T3> list3, Func<T1,T2,T3,TResult> f, int length)
	{
		try{
			if(Math.Min(Math.Min(list1.Count,list2.Count),list3.Count) < length){
				throw new Exception($"length {length} is larger than lists' length {Math.Min(Math.Min(list1.Count,list2.Count),list3.Count)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				for(int i = 0; i < length; i++) rs[i] = f(list1[i],list2[i],list3[i]);
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}


	public static TResult[] IndexedSingleFunc<TResult>(Func<int,TResult> f,int length){
		TResult[] r = new TResult[length];
		for(int i = 0; i < length ; i++) r[i] = f(i);
		return r;
	}
	
	public static TResult[] IndexedSingleFunc<T,TResult>(T[] array, Func<T,int,TResult> f){
		return IndexedSingleFunc(array,f,array.Length);
	}
	public static TResult[] IndexedSingleFunc<T,TResult>(T[] array, Func<T,int,TResult> f, int length){
		try{
			if(array.Length < length){throw new Exception($"length {length} is larger than array's length {array.Length}.");}
			else{
				TResult[] r = new TResult[length];
				for(int i = 0; i < length ; i++) r[i] = f(array[i], i);
				return r;
			}
		}
		catch(Exception e){throw e;}
	}
	public static TResult[] IndexedSingleFunc<T,TResult>(List<T> list, Func<T,int,TResult> f){
		return IndexedSingleFunc(list,f,list.Count);
	}
	public static TResult[] IndexedSingleFunc<T,TResult>(List<T> list, Func<T,int,TResult> f, int length){
		try{
			if(list.Count < length){throw new Exception($"length {length} is larger than list's length {list.Count}.");}
			else{
				TResult[] r = new TResult[length];
				for(int i = 0; i < length ; i++) r[i] = f(list[i], i);
				return r;
			}
		}
		catch(Exception e){throw e;}
	}

	public static TResult[] IndexedSingleFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<T1,T2,int,TResult> f){
		return IndexedSingleFunc(array1,array2,f,Math.Min(array1.Length,array2.Length));	
	}
	public static TResult[] IndexedSingleFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<T1,T2,int,TResult> f, int length)
	{
		try{
			if(Math.Min(array1.Length,array2.Length) < length){
				throw new Exception($"length {length} is larger than arrays' length {Math.Min(array1.Length,array2.Length)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				for(int i = 0; i < length ; i++) rs[i] = f(array1[i],array2[i], i);
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}
	public static TResult[] IndexedSingleFunc<T1,T2,TResult>(List<T1> list1, List<T2> list2, Func<T1,T2,int,TResult> f){
		return IndexedSingleFunc(list1,list2,f,Math.Min(list1.Count,list2.Count));	
	}
	public static TResult[] IndexedSingleFunc<T1,T2,TResult>(List<T1> list1, List<T2> list2, Func<T1,T2,int,TResult> f, int length)
	{
		try{
			if(Math.Min(list1.Count,list2.Count) < length){
				throw new Exception($"length {length} is larger than lists' length {Math.Min(list1.Count,list2.Count)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				for(int i = 0; i < length ; i++) rs[i] = f(list1[i],list2[i], i);
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}

	public static TResult[] IndexedSingleFunc<T1,T2,T3,TResult>(T1[] array1, T2[] array2, T3[] array3, Func<T1,T2,T3,int,TResult> f){
		return IndexedSingleFunc(array1,array2,array3,f,Math.Min(Math.Min(array1.Length,array2.Length),array3.Length));	
	}
	public static TResult[] IndexedSingleFunc<T1,T2,T3,TResult>(T1[] array1, T2[] array2, T3[] array3, Func<T1,T2,T3,int,TResult> f, int length)
	{
		try{
			if(Math.Min(Math.Min(array1.Length,array2.Length),array3.Length) < length){
				throw new Exception($"length {length} is larger than arrays' length {Math.Min(Math.Min(array1.Length,array2.Length),array3.Length)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				for(int i = 0; i < length; i++) rs[i] = f(array1[i],array2[i],array3[i], i);
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}
	public static TResult[] IndexedSingleFunc<T1,T2,T3,TResult>(List<T1> list1, List<T2> list2, List<T3> list3, Func<T1,T2,T3,int,TResult> f){
		return IndexedSingleFunc(list1,list2,list3,f,Math.Min(Math.Min(list1.Count,list2.Count),list3.Count));	
	}
	public static TResult[] IndexedSingleFunc<T1,T2,T3,TResult>(List<T1> list1, List<T2> list2, List<T3> list3, Func<T1,T2,T3,int,TResult> f, int length)
	{
		try{
			if(Math.Min(Math.Min(list1.Count,list2.Count),list3.Count) < length){
				throw new Exception($"length {length} is larger than lists' length {Math.Min(Math.Min(list1.Count,list2.Count),list3.Count)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				for(int i = 0; i < length; i++) rs[i] = f(list1[i],list2[i],list3[i], i);
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}


	public static TResult[] ParallelFunc<TResult>(Func<TResult> f,int length){
		TResult[] r = new TResult[length];
		Parallel.For(0, length, i => r[i] = f());
		return r;
	}
	
	public static TResult[] ParallelFunc<T,TResult>(T[] array, Func<T,TResult> f){
		return ParallelFunc(array,f,array.Length);
	}
	public static TResult[] ParallelFunc<T,TResult>(T[] array, Func<T,TResult> f, int length){
		try{
			if(array.Length < length){throw new Exception($"length {length} is larger than array's length {array.Length}.");}
			else{
				TResult[] r = new TResult[length];
				Parallel.For(0, length, i => r[i] = f(array[i]));
				return r;
			}
		}
		catch(Exception e){throw e;}
	}
	public static TResult[] ParallelFunc<T,TResult>(List<T> list, Func<T,TResult> f){
		return ParallelFunc(list,f,list.Count);
	}
	public static TResult[] ParallelFunc<T,TResult>(List<T> list, Func<T,TResult> f, int length){
		try{
			if(list.Count < length){throw new Exception($"length {length} is larger than list's length {list.Count}.");}
			else{
				TResult[] r = new TResult[length];
				Parallel.For(0, length, i => r[i] = f(list[i]));
				return r;
			}
		}
		catch(Exception e){throw e;}
	}

	public static TResult[] ParallelFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<T1,T2,TResult> f){
		return ParallelFunc(array1,array2,f,Math.Min(array1.Length,array2.Length));	
	}
	public static TResult[] ParallelFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<T1,T2,TResult> f, int length)
	{
		try{
			if(Math.Min(array1.Length,array2.Length) < length){
				throw new Exception($"length {length} is larger than arrays' length {Math.Min(array1.Length,array2.Length)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				Parallel.For(0, length, i => rs[i] = f(array1[i],array2[i]));
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}
	public static TResult[] ParallelFunc<T1,T2,TResult>(List<T1> list1, List<T2> list2, Func<T1,T2,TResult> f){
		return ParallelFunc(list1,list2,f,Math.Min(list1.Count,list2.Count));	
	}
	public static TResult[] ParallelFunc<T1,T2,TResult>(List<T1> list1, List<T2> list2, Func<T1,T2,TResult> f, int length)
	{
		try{
			if(Math.Min(list1.Count,list2.Count) < length){
				throw new Exception($"length {length} is larger than lists' length {Math.Min(list1.Count,list2.Count)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				Parallel.For(0, length, i => rs[i] = f(list1[i],list2[i]));
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}

	public static TResult[] ParallelFunc<T1,T2,T3,TResult>(T1[] array1, T2[] array2, T3[] array3, Func<T1,T2,T3,TResult> f){
		return ParallelFunc(array1,array2,array3,f,Math.Min(Math.Min(array1.Length,array2.Length),array3.Length));	
	}
	public static TResult[] ParallelFunc<T1,T2,T3,TResult>(T1[] array1, T2[] array2, T3[] array3, Func<T1,T2,T3,TResult> f, int length)
	{
		try{
			if(Math.Min(Math.Min(array1.Length,array2.Length),array3.Length) < length){
				throw new Exception($"length {length} is larger than arrays' length {Math.Min(Math.Min(array1.Length,array2.Length),array3.Length)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				Parallel.For(0, length, i => rs[i] = f(array1[i],array2[i],array3[i]));
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}
	public static TResult[] ParallelFunc<T1,T2,T3,TResult>(List<T1> list1, List<T2> list2, List<T3> list3, Func<T1,T2,T3,TResult> f){
		return ParallelFunc(list1,list2,list3,f,Math.Min(Math.Min(list1.Count,list2.Count),list3.Count));	
	}
	public static TResult[] ParallelFunc<T1,T2,T3,TResult>(List<T1> list1, List<T2> list2, List<T3> list3, Func<T1,T2,T3,TResult> f, int length)
	{
		try{
			if(Math.Min(Math.Min(list1.Count,list2.Count),list3.Count) < length){
				throw new Exception($"length {length} is larger than lists' length {Math.Min(Math.Min(list1.Count,list2.Count),list3.Count)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				Parallel.For(0, length, i => rs[i] = f(list1[i],list2[i],list3[i]));
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}


	public static TResult[] IndexedParallelFunc<TResult>(Func<int,TResult> f,int length){
		TResult[] r = new TResult[length];
		Parallel.For(0, length, i => r[i] = f(i));
		return r;
	}
	
	public static TResult[] IndexedParallelFunc<T,TResult>(T[] array, Func<T,int,TResult> f){
		return IndexedParallelFunc(array,f,array.Length);
	}
	public static TResult[] IndexedParallelFunc<T,TResult>(T[] array, Func<T,int,TResult> f, int length){
		try{
			if(array.Length < length){throw new Exception($"length {length} is larger than array's length {array.Length}.");}
			else{
				TResult[] r = new TResult[length];
				Parallel.For(0, length, i => r[i] = f(array[i],i));
				return r;
			}
		}
		catch(Exception e){throw e;}
	}
	public static TResult[] IndexedParallelFunc<T,TResult>(List<T> list, Func<T,int,TResult> f){
		return IndexedParallelFunc(list,f,list.Count);
	}
	public static TResult[] IndexedParallelFunc<T,TResult>(List<T> list, Func<T,int,TResult> f, int length){
		try{
			if(list.Count < length){throw new Exception($"length {length} is larger than list's length {list.Count}.");}
			else{
				TResult[] r = new TResult[length];
				Parallel.For(0, length, i => r[i] = f(list[i],i));
				return r;
			}
		}
		catch(Exception e){throw e;}
	}

	public static TResult[] IndexedParallelFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<T1,T2,int,TResult> f){
		return IndexedParallelFunc(array1,array2,f,Math.Min(array1.Length,array2.Length));	
	}
	public static TResult[] IndexedParallelFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<T1,T2,int,TResult> f, int length)
	{
		try{
			if(Math.Min(array1.Length,array2.Length) < length){
				throw new Exception($"length {length} is larger than arrays' length {Math.Min(array1.Length,array2.Length)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				Parallel.For(0, length, i => rs[i] = f(array1[i],array2[i],i));
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}
	public static TResult[] IndexedParallelFunc<T1,T2,TResult>(List<T1> list1, List<T2> list2, Func<T1,T2,int,TResult> f){
		return IndexedParallelFunc(list1,list2,f,Math.Min(list1.Count,list2.Count));	
	}
	public static TResult[] IndexedParallelFunc<T1,T2,TResult>(List<T1> list1, List<T2> list2, Func<T1,T2,int,TResult> f, int length)
	{
		try{
			if(Math.Min(list1.Count,list2.Count) < length){
				throw new Exception($"length {length} is larger than lists' length {Math.Min(list1.Count,list2.Count)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				Parallel.For(0, length, i => rs[i] = f(list1[i],list2[i],i));
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}

	public static TResult[] IndexedParallelFunc<T1,T2,T3,TResult>(T1[] array1, T2[] array2, T3[] array3, Func<T1,T2,T3,int,TResult> f){
		return IndexedParallelFunc(array1,array2,array3,f,Math.Min(Math.Min(array1.Length,array2.Length),array3.Length));	
	}
	public static TResult[] IndexedParallelFunc<T1,T2,T3,TResult>(T1[] array1, T2[] array2, T3[] array3, Func<T1,T2,T3,int,TResult> f, int length)
	{
		try{
			if(Math.Min(Math.Min(array1.Length,array2.Length),array3.Length) < length){
				throw new Exception($"length {length} is larger than arrays' length {Math.Min(Math.Min(array1.Length,array2.Length),array3.Length)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				Parallel.For(0, length, i => rs[i] = f(array1[i],array2[i],array3[i],i));
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}
	public static TResult[] IndexedParallelFunc<T1,T2,T3,TResult>(List<T1> list1, List<T2> list2, List<T3> list3, Func<T1,T2,T3,int,TResult> f){
		return IndexedParallelFunc(list1,list2,list3,f,Math.Min(Math.Min(list1.Count,list2.Count),list3.Count));	
	}
	public static TResult[] IndexedParallelFunc<T1,T2,T3,TResult>(List<T1> list1, List<T2> list2, List<T3> list3, Func<T1,T2,T3,int,TResult> f, int length)
	{
		try{
			if(Math.Min(Math.Min(list1.Count,list2.Count),list3.Count) < length){
				throw new Exception($"length {length} is larger than lists' length {Math.Min(Math.Min(list1.Count,list2.Count),list3.Count)}.");
			}
			else{
				TResult[] rs = new TResult[length];
				Parallel.For(0, length, i => rs[i] = f(list1[i],list2[i],list3[i],i));
				return rs;
			}
		}
		catch(Exception e){throw e;}
	}

}