using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class PathEnvironmentVariableDebug : IPathEnvironmentVariable
    {
        string _value;

        public PathEnvironmentVariableDebug() : this(Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine))
        {}

        public PathEnvironmentVariableDebug(string value)
        {
            this._value = value;
        }

        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
    }

}
