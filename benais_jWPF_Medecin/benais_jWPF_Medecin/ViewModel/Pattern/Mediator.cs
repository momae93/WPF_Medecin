using System;
using System.Collections.Generic;

namespace benais_jWPF_Medecin.ViewModel.Pattern
{
    public static class Mediator
    {
        static IDictionary<string, List<Action<object>>> callbacks = new Dictionary<string, List<Action<object>>>();

        static public void Register(string token, Action<object> callback)
        {
            if (!callbacks.ContainsKey(token))
            {
                var list = new List<Action<object>>();
                list.Add(callback);
                callbacks.Add(token, list);
            }
            else
            {
                bool found = false;
                foreach (var item in callbacks[token])
                    if (item.Method.ToString() == callback.Method.ToString())
                        found = true;
                if (!found)
                    callbacks[token].Add(callback);
            }
        }

        static public void Unregister(string token, Action<object> callback)
        {
            if (callbacks.ContainsKey(token))
                callbacks[token].Remove(callback);
        }

        static public void Notify(string token, object args)
        {
            if (callbacks.ContainsKey(token))
                foreach (var callback in callbacks[token])
                    callback(args);
        }
    }
}
