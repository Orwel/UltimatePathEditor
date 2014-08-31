using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimatePathEditor.ViewContract;

namespace UltimatePathEditor.Behavior
{
    /// <summary>
    /// The decorator of Path Value composant
    /// </summary>
    class PathValueDecorator
    {
        /// <summary>
        /// The current decorated Path Value
        /// </summary>
        private IPathValueViewContract current;

        /// <summary>
        /// Decorate a Path Value
        /// </summary>
        /// <param name="pathValue">The Path Value</param>
        public void Decorate(IPathValueViewContract pathValue)
        {
            if (current != null)
                current.IsDragOver = false;
            pathValue.IsDragOver = true;
            current = pathValue;
        }

        /// <summary>
        /// Clean the decorated Path Value
        /// </summary>
        public void Clear()
        {
            if (current != null)
                current.IsDragOver = false;
            current = null;
        }
    }
}
