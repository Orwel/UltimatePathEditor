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
            get { return _instance ?? (_instance = new PathVariableManager()); }
        }

        /// <summary>
        /// Private default constructor
        /// </summary>
        private PathVariableManager()
        {}
        #endregion Singleton

        #region Static Members
        public static IPathEnvironmentVariable PathEnvironmentVariable { get; set; }

        static PathVariableManager()
        {
            //PathEnvironmentVariable = new PathEnvironmentVariableDebug(); 
            PathEnvironmentVariable = new PathEnvironmentVariableAccess(); 
        }
        #endregion

        public const char SplitCharacter = ';';

        #region Fields
        /// <summary>
        /// Memento of Path Environment Variable
        /// </summary>
        private readonly Memento _memento = new Memento(PathEnvironmentVariable.Value);
        #endregion Fields

        /// <summary>
        /// Memorize the current value and set the Path Environment Variable
        /// </summary>
        /// <param name="PathEnvironmentVariableValue">New value to the Path Environment Variable</param>
        public void SetEnvironmentVariableMemento(string PathEnvironmentVariableValue)
        {
            this._memento.Do(PathEnvironmentVariableValue);
            this.SetEvnironmentVariable(PathEnvironmentVariableValue);
        }

        /// <summary>
        /// Return the Path Environment Variable
        /// </summary>
        /// <returns>the current Path Envrinment Variable</returns>
        public string GetEnvironmentVariable()
        {
            return this._memento.Current;
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

        #region Private Methods
        /// <summary>
        /// Set the Path Environment Variable
        /// </summary>
        /// <param name="PathEnvironmentVariableValue">New value to the Path Environment Variable</param>
        private void SetEvnironmentVariable(string PathEnvironmentVariableValue)
        {
            PathEnvironmentVariable.Value = PathEnvironmentVariableValue;
        }
        #endregion
    }
}
