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
using System.Windows.Threading;

namespace UltimatePathEditor.Behavior
{
    /// <summary>
    /// Commit the text box when the split key or enter is pressed
    /// </summary>
    public class TextBoxEventCommitBehavior : Behavior<FrameworkElement>
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

        /// <summary>
        /// Simulate TAB keydown to send the focus on the next keyboard navigation element.
        /// </summary>
        public static void KeyboardNavigationFocusNext()
        {
            // MoveFocus takes a TraversalRequest as its argument.
            TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);

            // Gets the element with keyboard focus.
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            // Change keyboard focus.
            if (elementWithFocus != null)
            {
                elementWithFocus.MoveFocus(request);
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
                //Send the focus on the new textBox after the refresh rendered.
                this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => { 
                                                                                            KeyboardNavigationFocusNext();
                                                                                            KeyboardNavigationFocusNext();
                                                                                         }));
            }
        }
        #endregion Event Handler
    }
}
