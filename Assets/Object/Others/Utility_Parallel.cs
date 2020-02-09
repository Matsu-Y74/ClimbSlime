using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Utility_Parallel{
    static class Utility_Parallel{
        public static TResult[] ParallelFunc<T,TResult>(T[] array, Func<T,TResult> f){
            TResult[] r = new TResult[array.Length];
            Parallel.For(0, array.Length, i => r[i] = f(array[i]));
            return r;
        }
        public static TResult[] ParallelFunc<T,TResult>(List<T> array, Func<T,TResult> f){
            TResult[] r = new TResult[array.Count];
            Parallel.For(0, array.Count, i => r[i] = f(array[i]));
            return r;
        }
        public static TResult[] ParallelFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<T1,T2,TResult> f){
            try{
                if(array1.Length != array2.Length){
                    throw new Exception($"{array1} and {array2} have difference lengths");
                }
                else{
                    TResult[] r = new TResult[array1.Length];
                    Parallel.For(0, array1.Length, i => r[i] = f(array1[i],array2[i]));
                    return r;
                }
            }
            catch(Exception e){ throw e; }
        }
        public static TResult[] ParallelFunc<T1,T2,TResult>(List<T1> array1, List<T2> array2, Func<T1,T2,TResult> f){
            try{
                if(array1.Count != array2.Count){
                    throw new Exception($"{array1} and {array2} have difference counts");
                }
                else{
                    TResult[] r = new TResult[array1.Count];
                    Parallel.For(0, array1.Count, i => r[i] = f(array1[i],array2[i]));
                    return r;
                }
            }
            catch(Exception e){ throw e; }
        }

        public static TResult[] IndexedParallelFunc<T,TResult>(T[] array, Func<int,T,TResult> f){
            TResult[] r = new TResult[array.Length];
            Parallel.For(0, array.Length, i => r[i] = f(i,array[i]));
            return r;
        }
        public static TResult[] IndexedParallelFunc<T,TResult>(List<T> array, Func<int,T,TResult> f){
            TResult[] r = new TResult[array.Count];
            Parallel.For(0, array.Count, i => r[i] = f(i,array[i]));
            return r;
        }
        public static TResult[] IndexedParallelFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<int,T1,T2,TResult> f){
            try{
                if(array1.Length != array2.Length){
                    throw new Exception($"{array1} and {array2} have difference lengths");
                }
                else{
                    TResult[] r = new TResult[array1.Length];
                    Parallel.For(0, array1.Length, i => r[i] = f(i,array1[i],array2[i]));
                    return r;
                }
            }
            catch(Exception e){ throw e; }
        }
        public static TResult[] IndexedParallelFunc<T1,T2,TResult>(List<T1> array1, List<T2> array2, Func<int,T1,T2,TResult> f){
            try{
                if(array1.Count != array2.Count){
                    throw new Exception($"{array1} and {array2} have difference counts");
                }
                else{
                    TResult[] r = new TResult[array1.Count];
                    Parallel.For(0, array1.Count, i => r[i] = f(i,array1[i],array2[i]));
                    return r;
                }
            }
            catch(Exception e){ throw e; }
        }
    }
}
