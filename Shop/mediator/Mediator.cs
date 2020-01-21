using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void MediatorCallback<T>(T command) where T : ICommand;
public static class Mediator 
{   
    //make sure you're using the System.Collections.Generic namespace
    private static Dictionary<System.Type, System.Delegate> _subscribers = new Dictionary<System.Type, System.Delegate>();
 
    public static void Subscribe<T>(MediatorCallback<T> callback) where T : ICommand
    {
        if(callback == null) throw new System.ArgumentNullException("callback");
        var tp = typeof(T);
        if(_subscribers.ContainsKey(tp))
            _subscribers[tp] = System.Delegate.Combine(_subscribers[tp], callback);
        else
            _subscribers.Add(tp, callback);
    }
 
    public static void DeleteSubscriber<T>(MediatorCallback<T> callback) where T : ICommand
    {
        if(callback == null) throw new System.ArgumentNullException("callback");
        var tp = typeof(T);
        if(_subscribers.ContainsKey(tp))
        {
            var d = _subscribers[tp];
            d = System.Delegate.Remove(d, callback);
            if(d == null) _subscribers.Remove(tp);
            else _subscribers[tp] = d;
        }
    }
 
    public static void Publish<T>(T command) where T : ICommand
    {
        var tp = typeof(T);
        if(_subscribers.ContainsKey(tp))
        {
            _subscribers[tp].DynamicInvoke(command);
        }
    }
}
