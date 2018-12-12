
namespace ZacksSampleCode
{
    using System;
    using System.Windows.Threading;
    public class DispatchService
    {
        /// <summary>
        /// Set this to the windows Dispatcher object in the Window constructor
        /// </summary>
        public static Dispatcher DispatchObject { get; set; }

        public static void Dispatch(Action action)
        {
            if (DispatchObject == null || DispatchObject.CheckAccess())
            {
                action();
            }
            else
            {
                DispatchObject.Invoke(action);
            }
        }
    }
}
