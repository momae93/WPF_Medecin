using benais_jWPF_Medecin.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benais_jWPF_Medecin.ViewModel.Pattern
{
    static public class PageMediator
    {
        static IDictionary<string, List<Action<EUserControl, object>>> callbacks = new Dictionary<string, List<Action<EUserControl, object>>>();

        static public void Register(string token, Action<EUserControl, object> callback)
        {
            if (!callbacks.ContainsKey(token))
            {
                var list = new List<Action<EUserControl, object>>();
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

        static public void Unregister(string token, Action<EUserControl, object> callback)
        {
            if (callbacks.ContainsKey(token))
                callbacks[token].Remove(callback);
        }

        static public void Notify(string token, EUserControl userControl, object param)
        {
            if (callbacks.ContainsKey(token))
                foreach (var callback in callbacks[token])
                    callback(userControl, param);
        }
    }
}
