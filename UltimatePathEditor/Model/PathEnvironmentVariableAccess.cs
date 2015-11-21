using System;

namespace UltimatePathEditor.Model
{
    /// <summary>
    /// Interface to manipulate Environment Variable Path
    /// </summary>
    interface IPathEnvironmentVariable
    {
        /// <summary>
        /// Value of the Environment Variable Path
        /// </summary>
        string Value { get; set; }
    }

    /// <summary>
    /// Access to read and to write the Environment Variable Path
    /// </summary>
    class PathEnvironmentVariableAccess : IPathEnvironmentVariable
    {
        public string Value
        {
            get
            {
                return Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);
            }
            set
            {
                Environment.SetEnvironmentVariable("Path", value, EnvironmentVariableTarget.Machine);
            }
        }
    }

    /// <summary>
    /// Fake access to simulate the Environment Variable Path
    /// </summary>
    internal class PathEnvironmentVariableDebug : IPathEnvironmentVariable
    {
        public PathEnvironmentVariableDebug() : this(Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine))
        {}

        public PathEnvironmentVariableDebug(string value)
        {
            this.Value = value;
        }

        public string Value { get; set; }
    }

}
