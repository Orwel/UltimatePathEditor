using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace BehaviorTextBoxCommitPressKey
{
    /// <summary>
    /// Commit the text box when the split key or enter is pressed
    /// </summary>
    class TextBoxEventCommitBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            var textBox = this.AssociatedObject as TextBox;
            if (textBox == null)
                throw new ArgumentException("TextBoxCommitKeyDownBehavior is to TextBox only.");
            textBox.KeyDown += TextBox_KeyDown;
            textBox.TextChanged += TextBox_TextChanged;
        }

        /// <summary>
        /// Commit a TextBox
        /// </summary>
        /// <param name="textBox">TextBox will be committed</param>
        public static void CommitTextBox(TextBox textBox)
        {
            BindingExpression binding = BindingOperations.GetBindingExpression(textBox, TextBox.TextProperty);

            if (binding != null)
            {
                binding.UpdateSource();
            }
        }

        #region Event Handler
        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
                return;
            if (e.Key == Key.Enter)
            {
                CommitTextBox(textBox);
            }
        }

        void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
                return;
            if (textBox.Text.Contains(UltimatePathEditor.Model.PathVariableManager.SplitCharacter))
            {
                CommitTextBox(textBox);
            }
        }
        #endregion Event Handler
    }
}
