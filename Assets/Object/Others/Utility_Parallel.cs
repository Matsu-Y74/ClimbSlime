using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

static class Utility_Parallel{
    public enum ParallelFuncMode {Min, Safety};
    public static TResult[] ParallelFunc<TResult>(int length,Func<TResult> f){
        TResult[] r = new TResult[length];
        Parallel.For(0, length, i => r[i] = f());
        return r;
    }
    public static TResult[] ParallelFunc<T,TResult>(T[] array, Func<T,TResult> f){
        TResult[] r = new TResult[array.Length];
        Parallel.For(0, array.Length, i => r[i] = f(array[i]));
        return r;
    }
    public static TResult[] ParallelFunc<T,TResult>(List<T> list, Func<T,TResult> f){
        TResult[] r = new TResult[list.Count];
        Parallel.For(0, list.Count, i => r[i] = f(list[i]));
        return r;
    }
    public static TResult[] ParallelFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<T1,T2,TResult> f){
        return ParallelFunc(array1,array2,f,ParallelFuncMode.Safety);
    }
    public static TResult[] ParallelFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<T1,T2,TResult> f, ParallelFuncMode mode){
        switch(mode){
            case ParallelFuncMode.Min:
                TResult[] r = new TResult[Math.Min(array1.Length,array2.Length)];
                Parallel.For(0, array1.Length, i => r[i] = f(array1[i],array2[i]));
                return r;
            case ParallelFuncMode.Safety:
                try{
                    if(array1.Length != array2.Length){
                        throw new Exception($"{array1} and {array2} have difference lengths");
                    }
                    else{
                        TResult[] rs = new TResult[array1.Length];
                        Parallel.For(0, array1.Length, i => rs[i] = f(array1[i],array2[i]));
                        return rs;
                    }
                }
                catch(Exception e){ throw e; }
            default:
                throw new Exception($"Undefined mode {mode}");
        }
    }
    public static TResult[] ParallelFunc<T1,T2,TResult>(List<T1> list1, List<T2> list2, Func<T1,T2,TResult> f){
        return ParallelFunc(list1,list2,f,ParallelFuncMode.Safety);
    }
    public static TResult[] ParallelFunc<T1,T2,TResult>(List<T1> list1, List<T2> list2, Func<T1,T2,TResult> f, ParallelFuncMode mode){
        switch(mode){
            case ParallelFuncMode.Min:
                TResult[] r = new TResult[Math.Min(list1.Count,list2.Count)];
                Parallel.For(0, list1.Count, i => r[i] = f(list1[i],list2[i]));
                return r;
            case ParallelFuncMode.Safety:
                try{
                    if(list1.Count != list2.Count){
                        throw new Exception($"{list1} and {list2} have difference lengths");
                    }
                    else{
                        TResult[] rs = new TResult[list1.Count];
                        Parallel.For(0, list1.Count, i => rs[i] = f(list1[i],list2[i]));
                        return rs;
                    }
                }
                catch(Exception e){ throw e; }
            default:
                throw new Exception($"Undefined mode {mode}");
        }
    }

    public static TResult[] IndexedParallelFunc<TResult>(int length,Func<int,TResult> f){
        TResult[] r = new TResult[length];
        Parallel.For(0, length, i => r[i] = f(i));
        return r;
    }
    public static TResult[] IndexedParallelFunc<T,TResult>(T[] array, Func<int,T,TResult> f){
        TResult[] r = new TResult[array.Length];
        Parallel.For(0, array.Length, i => r[i] = f(i,array[i]));
        return r;
    }
    public static TResult[] IndexedParallelFunc<T,TResult>(List<T> list, Func<int,T,TResult> f){
        TResult[] r = new TResult[list.Count];
        Parallel.For(0, list.Count, i => r[i] = f(i,list[i]));
        return r;
    }
    public static TResult[] IndexedParallelFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<int,T1,T2,TResult> f){
        return IndexedParallelFunc(array1,array2,f,ParallelFuncMode.Safety);
    }
    public static TResult[] IndexedParallelFunc<T1,T2,TResult>(T1[] array1, T2[] array2, Func<int,T1,T2,TResult> f, ParallelFuncMode mode){
        switch(mode){
            case ParallelFuncMode.Min:
                TResult[] r = new TResult[Math.Min(array1.Length,array2.Length)];
                Parallel.For(0, array1.Length, i => r[i] = f(i,array1[i],array2[i]));
                return r;
            case ParallelFuncMode.Safety:
                try{
                    if(array1.Length != array2.Length){
                        throw new Exception($"{array1} and {array2} have difference lengths");
                    }
                    else{
                        TResult[] rs = new TResult[array1.Length];
                        Parallel.For(0, array1.Length, i => rs[i] = f(i,array1[i],array2[i]));
                        return rs;
                    }
                }
                catch(Exception e){ throw e; }
            default:
                throw new Exception($"Undefined mode {mode}");
        }
    }
    public static TResult[] IndexedParallelFunc<T1,T2,TResult>(List<T1> list1, List<T2> list2, Func<int,T1,T2,TResult> f){
        return IndexedParallelFunc(list1,list2,f,ParallelFuncMode.Safety);
    }
    public static TResult[] IndexedParallelFunc<T1,T2,TResult>(List<T1> list1, List<T2> list2, Func<int,T1,T2,TResult> f, ParallelFuncMode mode){
        switch(mode){
            case ParallelFuncMode.Min:
                TResult[] r = new TResult[Math.Min(list1.Count,list2.Count)];
                Parallel.For(0, list1.Count, i => r[i] = f(i,list1[i],list2[i]));
                return r;
            case ParallelFuncMode.Safety:
                try{
                    if(list1.Count != list2.Count){
                        throw new Exception($"{list1} and {list2} have difference lengths");
                    }
                    else{
                        TResult[] rs = new TResult[list1.Count];
                        Parallel.For(0, list1.Count, i => rs[i] = f(i,list1[i],list2[i]));
                        return rs;
                    }
                }
                catch(Exception e){ throw e; }
            default:
                throw new Exception($"Undefined mode {mode}");
        }
    }
}