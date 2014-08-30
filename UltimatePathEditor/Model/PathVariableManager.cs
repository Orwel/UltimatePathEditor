using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimatePathEditor.Model
{
    /// <summary>
    /// Manager of Path Environment Variable
    /// </summary>
    sealed class PathVariableManager
    {
        #region Singleton
        private static PathVariableManager _instance;
        public static PathVariableManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PathVariableManager();
                return _instance;
            }
        }
        #endregion Singleton

        public const char SplitCharacter = ';';

        #region Fields
        /// <summary>
        /// Memento of Path Environment Variable
        /// </summary>
        private Memento _memento = new Memento();
        #endregion Fields

        /// <summary>
        /// Set the Path Environment Variable
        /// </summary>
        /// <param name="PathEnvironmentVariable">New value to the Path Environment Variable</param>
        private void SetEvnironmentVariable(string PathEnvironmentVariable)
        {
            //Environment.SetEnvironmentVariable("Path", PathEnvironmentVariable, EnvironmentVariableTarget.Machine);
        }

        /// <summary>
        /// Memorize the current value and set the Path Environment Variable
        /// </summary>
        /// <param name="PathEnvironmentVariable">New value to the Path Environment Variable</param>
        public void SetEvnironmentVariableMemento(string PathEnvironmentVariable)
        {
            this._memento.Do(this.GetEnvironmentVariable());
            this.SetEvnironmentVariable(PathEnvironmentVariable);
        }

        /// <summary>
        /// Return the Path Environment Variable
        /// </summary>
        /// <returns>the current Path Envrinment Variable</returns>
        public string GetEnvironmentVariable()
        {
            return Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);
        }

        /// <summary>
        /// Restore the precedent value to the Path Environment Variable.
        /// </summary>
        /// <returns>Value restored to the Path Environment Variable or null if nothing is send.</returns>
        public string Undo()
        {
            var m = this._memento.Undo();
            if (m != null)
                this.SetEvnironmentVariable(m);
            return m;
        }

        /// <summary>
        /// Send the next value to the Path Environment Variable.
        /// </summary>
        /// <returns>Value restored to the Path Environment Variable or null if nothing is send.</returns>
        public string Redo()
        {
            var m = this._memento.Redo();
            if (m != null)
                this.SetEvnironmentVariable(m);
            return m;
        }
    }
}
