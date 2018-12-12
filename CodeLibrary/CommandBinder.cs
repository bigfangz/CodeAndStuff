

namespace ZacksSampleCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Diagnostics;
    using System.Windows.Input;
    public class CommandBinder : ICommand
    {
        Action<object> _clickDelegate;

        public CommandBinder(Action<object> clickDelegate)
        {
            if (clickDelegate != null)
            {
                this._clickDelegate += clickDelegate;
                AddDebugTrace();
            }
        }




        [Conditional("DEBUG")]
        private void AddDebugTrace()
        {
            //This is safe because AddDebugTrace is only called after _clickDelegate is assigned.
            string methodName = _clickDelegate.GetInvocationList()[0].Method.Name;
            this._clickDelegate += (a) => Trace.WriteLine(string.Format("CLICK: {0}, Arg:{1}", methodName, a));

        }
        /// <summary>
        /// Lets caller know if it is safe to execute the command.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {

            return this._clickDelegate != null;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_clickDelegate != null)
                _clickDelegate(parameter);
        }
    }
}
