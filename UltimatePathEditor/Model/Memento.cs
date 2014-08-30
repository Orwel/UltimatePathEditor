using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimatePathEditor.Model
{
    /// <summary>
    /// Memento memorize the different state of a value.
    /// </summary>
    class Memento
    {
        #region Fields
        private Stack<string> UndoStack = new Stack<string>();
        private Stack<string> RedoStack = new Stack<string>();
        #endregion Fields

        /// <summary>
        /// Memorize the value.
        /// </summary>
        /// <param name="value"></param>
        public void Do(string value)
        {
            this.RedoStack.Clear();
            this.UndoStack.Push(value);
        }

        /// <summary>
        /// Return the precedent state
        /// </summary>
        /// <returns>The precedent state or null if nothing is memorized</returns>
        public string Undo()
        {
            if (RedoStack.Count != 0)
            {
                var m = RedoStack.Pop();
                UndoStack.Push(m);
                return m;
            }
            else
                return null;
        }

        /// <summary>
        /// Return the next state
        /// </summary>
        /// <returns>The next state or null if nothing is memorized</returns>
        public string Redo()
        {
            if (UndoStack.Count >= 2)
            {
                RedoStack.Push(UndoStack.Pop());
                return UndoStack.Peek();
            }
            else
                return null;
        }
    }
}
