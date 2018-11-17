using System;
using System.Windows;
using System.Windows.Threading;

namespace benais_jWPF_Medecin.ViewModel.Pattern
{
    public static class DispatchService
    {
        /// <summary>
        /// Invoke current application dispatcher on an action callback
        /// </summary>
        /// <param name="action"></param>
        public static void Invoke(Action action)
        {
            Dispatcher dispatchObject = Application.Current.Dispatcher;
            if (dispatchObject == null || dispatchObject.CheckAccess())
            {
                action();
            }
            else
            {
                dispatchObject.Invoke(action);
            }
        }
    }
}
