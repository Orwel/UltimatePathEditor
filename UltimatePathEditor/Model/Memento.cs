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
        private string _current;
        private Stack<string> _undoStack = new Stack<string>();
        private Stack<string> _redoStack = new Stack<string>();
        #endregion Fields

        #region Properties
        public string Current { get { return this._current; } }
        #endregion Properties

        public Memento(string current)
        {
            this._current = current;
        }

        /// <summary>
        /// Memorize the value.
        /// </summary>
        /// <param name="value"></param>
        public void Do(string current)
        {
            this._redoStack.Clear();
            this._undoStack.Push(this._current);
            this._current = current;
        }

        /// <summary>
        /// Return the precedent state
        /// </summary>
        /// <returns>The precedent state or null if nothing is memorized</returns>
        public string Undo()
        {
            if (_undoStack.Count > 0)
            {
                _redoStack.Push(this._current);
                this._current = _undoStack.Pop();                
                return this._current;
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
            if (_redoStack.Count > 0)
            {
                _undoStack.Push(this._current);
                this._current = _redoStack.Pop();                
                return this._current;
            }
            else
                return null;
        }
    }
}
