using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    Action action;
    public void AddListener(Action method)
    {
        action -= method;
        action += method;
    }
    public void RemoveListener(Action method) { action -= method; }
    public void RemoveAllListeners() { action = null; }
    public void Invoke() { action.Invoke(); }
}
public class Event<T>
{
    Action<T> action;
    public void AddListener(Action<T> method)
    {
        action -= method;
        action += method;
    }
    public void RemoveListener(Action<T> method) { action -= method; }
    public void RemoveAllListeners() { action = null; }
    public void Invoke(T arg) { action.Invoke(arg); }
}
public class Event<T, T1>
{
    Action<T, T1> action;
    public void AddListener(Action<T, T1> method)
    {
        action -= method;
        action += method;
    }
    public void RemoveListener(Action<T, T1> method) { action -= method; }
    public void RemoveAllListeners() { action = null; }
    public void Invoke(T arg1, T1 arg2) { action.Invoke(arg1, arg2); }
}
public class Event<T, T1, T2>
{
    Action<T, T1, T2> action;
    public void AddListener(Action<T, T1, T2> method)
    {
        action -= method;
        action += method;
    }
    public void RemoveListener(Action<T, T1, T2> method) { action -= method; }
    public void RemoveAllListeners() { action = null; }
    public void Invoke(T arg1, T1 arg2, T2 arg3) { action.Invoke(arg1, arg2, arg3); }
}