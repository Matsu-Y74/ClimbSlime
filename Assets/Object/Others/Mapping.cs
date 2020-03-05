using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Mapping{
	public static class SingleFunction{
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


		public static void SingleAction(Action f,int length){ for(int i = 0; i < length ; i++) f();
		}
		
		public static void SingleAction<T>(T[] array, Action<T> f){
			SingleAction(array,f,array.Length);
		}
		public static void SingleAction<T>(T[] array, Action<T> f, int length){
			try{
				if(array.Length < length){throw new Exception($"length {length} is larger than array's length {array.Length}.");}
				else{
					for(int i = 0; i < length ; i++) f(array[i]);
				}
			}
			catch(Exception e){throw e;}
		}
		public static void SingleAction<T>(List<T> list, Action<T> f){
			SingleAction(list,f,list.Count);
		}
		public static void SingleAction<T>(List<T> list, Action<T> f, int length){
			try{
				if(list.Count < length){throw new Exception($"length {length} is larger than list's length {list.Count}.");}
				else{
					for(int i = 0; i < length ; i++) f(list[i]);
				}
			}
			catch(Exception e){throw e;}
		}

		public static void SingleAction<T1,T2>(T1[] array1, T2[] array2, Action<T1,T2> f){
			SingleAction(array1,array2,f,Math.Min(array1.Length,array2.Length));	
		}
		public static void SingleAction<T1,T2>(T1[] array1, T2[] array2, Action<T1,T2> f, int length)
		{
			try{
				if(Math.Min(array1.Length,array2.Length) < length){
					throw new Exception($"length {length} is larger than arrays' length {Math.Min(array1.Length,array2.Length)}.");
				}
				else{
					for(int i = 0; i < length ; i++) f(array1[i],array2[i]);
				}
			}
			catch(Exception e){throw e;}
		}
		public static void SingleAction<T1,T2>(List<T1> list1, List<T2> list2, Action<T1,T2> f){
			SingleAction(list1,list2,f,Math.Min(list1.Count,list2.Count));	
		}
		public static void SingleAction<T1,T2>(List<T1> list1, List<T2> list2, Action<T1,T2> f, int length)
		{
			try{
				if(Math.Min(list1.Count,list2.Count) < length){
					throw new Exception($"length {length} is larger than lists' length {Math.Min(list1.Count,list2.Count)}.");
				}
				else{
					for(int i = 0; i < length ; i++) f(list1[i],list2[i]);
				}
			}
			catch(Exception e){throw e;}
		}

		public static void SingleAction<T1,T2,T3>(T1[] array1, T2[] array2, T3[] array3, Action<T1,T2,T3> f){
			SingleAction(array1,array2,array3,f,Math.Min(Math.Min(array1.Length,array2.Length),array3.Length));	
		}
		public static void SingleAction<T1,T2,T3>(T1[] array1, T2[] array2, T3[] array3, Action<T1,T2,T3> f, int length)
		{
			try{
				if(Math.Min(Math.Min(array1.Length,array2.Length),array3.Length) < length){
					throw new Exception($"length {length} is larger than arrays' length {Math.Min(Math.Min(array1.Length,array2.Length),array3.Length)}.");
				}
				else{
					for(int i = 0; i < length; i++) f(array1[i],array2[i],array3[i]);
				}
			}
			catch(Exception e){throw e;}
		}
		public static void SingleAction<T1,T2,T3>(List<T1> list1, List<T2> list2, List<T3> list3, Action<T1,T2,T3> f){
			SingleAction(list1,list2,list3,f,Math.Min(Math.Min(list1.Count,list2.Count),list3.Count));	
		}
		public static void SingleAction<T1,T2,T3>(List<T1> list1, List<T2> list2, List<T3> list3, Action<T1,T2,T3> f, int length)
		{
			try{
				if(Math.Min(Math.Min(list1.Count,list2.Count),list3.Count) < length){
					throw new Exception($"length {length} is larger than lists' length {Math.Min(Math.Min(list1.Count,list2.Count),list3.Count)}.");
				}
				else{
					for(int i = 0; i < length; i++) f(list1[i],list2[i],list3[i]);
				}
			}
			catch(Exception e){throw e;}
		}


		public static void IndexedSingleAction(Action<int> f,int length){
			for(int i = 0; i < length ; i++) f(i);
		}
		
		public static void IndexedSingleAction<T>(T[] array, Action<T,int> f){
			IndexedSingleAction(array,f,array.Length);
		}
		public static void IndexedSingleAction<T>(T[] array, Action<T,int> f, int length){
			try{
				if(array.Length < length){throw new Exception($"length {length} is larger than array's length {array.Length}.");}
				else{
					for(int i = 0; i < length ; i++) f(array[i], i);
				}
			}
			catch(Exception e){throw e;}
		}
		public static void IndexedSingleAction<T>(List<T> list, Action<T,int> f){
			IndexedSingleAction(list,f,list.Count);
		}
		public static void IndexedSingleAction<T>(List<T> list, Action<T,int> f, int length){
			try{
				if(list.Count < length){throw new Exception($"length {length} is larger than list's length {list.Count}.");}
				else{
					for(int i = 0; i < length ; i++) f(list[i], i);
				}
			}
			catch(Exception e){throw e;}
		}

		public static void IndexedSingleAction<T1,T2>(T1[] array1, T2[] array2, Action<T1,T2,int> f){
			IndexedSingleAction(array1,array2,f,Math.Min(array1.Length,array2.Length));	
		}
		public static void IndexedSingleAction<T1,T2>(T1[] array1, T2[] array2, Action<T1,T2,int> f, int length)
		{
			try{
				if(Math.Min(array1.Length,array2.Length) < length){
					throw new Exception($"length {length} is larger than arrays' length {Math.Min(array1.Length,array2.Length)}.");
				}
				else{
					for(int i = 0; i < length ; i++) f(array1[i],array2[i], i);
				}
			}
			catch(Exception e){throw e;}
		}
		public static void IndexedSingleAction<T1,T2>(List<T1> list1, List<T2> list2, Action<T1,T2,int> f){
			IndexedSingleAction(list1,list2,f,Math.Min(list1.Count,list2.Count));	
		}
		public static void IndexedSingleAction<T1,T2>(List<T1> list1, List<T2> list2, Action<T1,T2,int> f, int length)
		{
			try{
				if(Math.Min(list1.Count,list2.Count) < length){
					throw new Exception($"length {length} is larger than lists' length {Math.Min(list1.Count,list2.Count)}.");
				}
				else{
					for(int i = 0; i < length ; i++) f(list1[i],list2[i], i);
				}
			}
			catch(Exception e){throw e;}
		}

		public static void IndexedSingleAction<T1,T2,T3>(T1[] array1, T2[] array2, T3[] array3, Action<T1,T2,T3,int> f){
			IndexedSingleAction(array1,array2,array3,f,Math.Min(Math.Min(array1.Length,array2.Length),array3.Length));	
		}
		public static void IndexedSingleAction<T1,T2,T3>(T1[] array1, T2[] array2, T3[] array3, Action<T1,T2,T3,int> f, int length)
		{
			try{
				if(Math.Min(Math.Min(array1.Length,array2.Length),array3.Length) < length){
					throw new Exception($"length {length} is larger than arrays' length {Math.Min(Math.Min(array1.Length,array2.Length),array3.Length)}.");
				}
				else{
					for(int i = 0; i < length; i++) f(array1[i],array2[i],array3[i], i);
				}
			}
			catch(Exception e){throw e;}
		}
		public static void IndexedSingleAction<T1,T2,T3>(List<T1> list1, List<T2> list2, List<T3> list3, Action<T1,T2,T3,int> f){
			IndexedSingleAction(list1,list2,list3,f,Math.Min(Math.Min(list1.Count,list2.Count),list3.Count));	
		}
		public static void IndexedSingleAction<T1,T2,T3>(List<T1> list1, List<T2> list2, List<T3> list3, Action<T1,T2,T3,int> f, int length)
		{
			try{
				if(Math.Min(Math.Min(list1.Count,list2.Count),list3.Count) < length){
					throw new Exception($"length {length} is larger than lists' length {Math.Min(Math.Min(list1.Count,list2.Count),list3.Count)}.");
				}
				else{
					for(int i = 0; i < length; i++) f(list1[i],list2[i],list3[i], i);
				}
			}
			catch(Exception e){throw e;}
		}
	}

	public static class ParallelFunction{
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


		public static void ParallelAction(Action f,int length){
			Parallel.For(0, length, i => f());
		}
		
		public static void ParallelAction<T>(T[] array, Action<T> f){
			ParallelAction(array,f,array.Length);
		}
		public static void ParallelAction<T>(T[] array, Action<T> f, int length){
			try{
				if(array.Length < length){throw new Exception($"length {length} is larger than array's length {array.Length}.");}
				else{
					Parallel.For(0, length, i => f(array[i]));
				}
			}
			catch(Exception e){throw e;}
		}
		public static void ParallelAction<T>(List<T> list, Action<T> f){
			ParallelAction(list,f,list.Count);
		}
		public static void ParallelAction<T>(List<T> list, Action<T> f, int length){
			try{
				if(list.Count < length){throw new Exception($"length {length} is larger than list's length {list.Count}.");}
				else{
					Parallel.For(0, length, i => f(list[i]));
				}
			}
			catch(Exception e){throw e;}
		}

		public static void ParallelAction<T1,T2>(T1[] array1, T2[] array2, Action<T1,T2> f){
			ParallelAction(array1,array2,f,Math.Min(array1.Length,array2.Length));	
		}
		public static void ParallelAction<T1,T2>(T1[] array1, T2[] array2, Action<T1,T2> f, int length)
		{
			try{
				if(Math.Min(array1.Length,array2.Length) < length){
					throw new Exception($"length {length} is larger than arrays' length {Math.Min(array1.Length,array2.Length)}.");
				}
				else{
					Parallel.For(0, length, i => f(array1[i],array2[i]));
				}
			}
			catch(Exception e){throw e;}
		}
		public static void ParallelAction<T1,T2>(List<T1> list1, List<T2> list2, Action<T1,T2> f){
			ParallelAction(list1,list2,f,Math.Min(list1.Count,list2.Count));	
		}
		public static void ParallelAction<T1,T2>(List<T1> list1, List<T2> list2, Action<T1,T2> f, int length)
		{
			try{
				if(Math.Min(list1.Count,list2.Count) < length){
					throw new Exception($"length {length} is larger than lists' length {Math.Min(list1.Count,list2.Count)}.");
				}
				else{
					Parallel.For(0, length, i => f(list1[i],list2[i]));
				}
			}
			catch(Exception e){throw e;}
		}

		public static void ParallelAction<T1,T2,T3>(T1[] array1, T2[] array2, T3[] array3, Action<T1,T2,T3> f){
			ParallelAction(array1,array2,array3,f,Math.Min(Math.Min(array1.Length,array2.Length),array3.Length));	
		}
		public static void ParallelAction<T1,T2,T3>(T1[] array1, T2[] array2, T3[] array3, Action<T1,T2,T3> f, int length)
		{
			try{
				if(Math.Min(Math.Min(array1.Length,array2.Length),array3.Length) < length){
					throw new Exception($"length {length} is larger than arrays' length {Math.Min(Math.Min(array1.Length,array2.Length),array3.Length)}.");
				}
				else{
					Parallel.For(0, length, i => f(array1[i],array2[i],array3[i]));
				}
			}
			catch(Exception e){throw e;}
		}
		public static void ParallelAction<T1,T2,T3>(List<T1> list1, List<T2> list2, List<T3> list3, Action<T1,T2,T3> f){
			ParallelAction(list1,list2,list3,f,Math.Min(Math.Min(list1.Count,list2.Count),list3.Count));	
		}
		public static void ParallelAction<T1,T2,T3>(List<T1> list1, List<T2> list2, List<T3> list3, Action<T1,T2,T3> f, int length)
		{
			try{
				if(Math.Min(Math.Min(list1.Count,list2.Count),list3.Count) < length){
					throw new Exception($"length {length} is larger than lists' length {Math.Min(Math.Min(list1.Count,list2.Count),list3.Count)}.");
				}
				else{
					Parallel.For(0, length, i => f(list1[i],list2[i],list3[i]));
				}
			}
			catch(Exception e){throw e;}
		}


		public static void IndexedParallelAction(Action<int> f,int length){
			Parallel.For(0, length, i => f(i));
		}
		
		public static void IndexedParallelAction<T>(T[] array, Action<T,int> f){
			IndexedParallelAction(array,f,array.Length);
		}
		public static void IndexedParallelAction<T>(T[] array, Action<T,int> f, int length){
			try{
				if(array.Length < length){throw new Exception($"length {length} is larger than array's length {array.Length}.");}
				else{
					Parallel.For(0, length, i => f(array[i],i));
				}
			}
			catch(Exception e){throw e;}
		}
		public static void IndexedParallelAction<T>(List<T> list, Action<T,int> f){
			IndexedParallelAction(list,f,list.Count);
		}
		public static void IndexedParallelAction<T>(List<T> list, Action<T,int> f, int length){
			try{
				if(list.Count < length){throw new Exception($"length {length} is larger than list's length {list.Count}.");}
				else{
					Parallel.For(0, length, i => f(list[i],i));
				}
			}
			catch(Exception e){throw e;}
		}

		public static void IndexedParallelAction<T1,T2>(T1[] array1, T2[] array2, Action<T1,T2,int> f){
			IndexedParallelAction(array1,array2,f,Math.Min(array1.Length,array2.Length));	
		}
		public static void IndexedParallelAction<T1,T2>(T1[] array1, T2[] array2, Action<T1,T2,int> f, int length)
		{
			try{
				if(Math.Min(array1.Length,array2.Length) < length){
					throw new Exception($"length {length} is larger than arrays' length {Math.Min(array1.Length,array2.Length)}.");
				}
				else{
					Parallel.For(0, length, i => f(array1[i],array2[i],i));
				}
			}
			catch(Exception e){throw e;}
		}
		public static void IndexedParallelAction<T1,T2>(List<T1> list1, List<T2> list2, Action<T1,T2,int> f){
			IndexedParallelAction(list1,list2,f,Math.Min(list1.Count,list2.Count));	
		}
		public static void IndexedParallelAction<T1,T2>(List<T1> list1, List<T2> list2, Action<T1,T2,int> f, int length)
		{
			try{
				if(Math.Min(list1.Count,list2.Count) < length){
					throw new Exception($"length {length} is larger than lists' length {Math.Min(list1.Count,list2.Count)}.");
				}
				else{
					Parallel.For(0, length, i => f(list1[i],list2[i],i));
				}
			}
			catch(Exception e){throw e;}
		}

		public static void IndexedParallelAction<T1,T2,T3>(T1[] array1, T2[] array2, T3[] array3, Action<T1,T2,T3,int> f){
			IndexedParallelAction(array1,array2,array3,f,Math.Min(Math.Min(array1.Length,array2.Length),array3.Length));	
		}
		public static void IndexedParallelAction<T1,T2,T3>(T1[] array1, T2[] array2, T3[] array3, Action<T1,T2,T3,int> f, int length)
		{
			try{
				if(Math.Min(Math.Min(array1.Length,array2.Length),array3.Length) < length){
					throw new Exception($"length {length} is larger than arrays' length {Math.Min(Math.Min(array1.Length,array2.Length),array3.Length)}.");
				}
				else{
					Parallel.For(0, length, i => f(array1[i],array2[i],array3[i],i));
				}
			}
			catch(Exception e){throw e;}
		}
		public static void IndexedParallelAction<T1,T2,T3>(List<T1> list1, List<T2> list2, List<T3> list3, Action<T1,T2,T3,int> f){
			IndexedParallelAction(list1,list2,list3,f,Math.Min(Math.Min(list1.Count,list2.Count),list3.Count));	
		}
		public static void IndexedParallelAction<T1,T2,T3>(List<T1> list1, List<T2> list2, List<T3> list3, Action<T1,T2,T3,int> f, int length)
	{
		try{
			if(Math.Min(Math.Min(list1.Count,list2.Count),list3.Count) < length){
				throw new Exception($"length {length} is larger than lists' length {Math.Min(Math.Min(list1.Count,list2.Count),list3.Count)}.");
			}
			else{
				Parallel.For(0, length, i => f(list1[i],list2[i],list3[i],i));
			}
		}
		catch(Exception e){throw e;}
	}
	}
}
