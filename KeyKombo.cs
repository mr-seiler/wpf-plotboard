using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PlottingBoard
{
    /// <summary>
    /// Helper class to more easily specify hotkey combinations in code.
    /// (Instances of this class can be collected and iterated.)
    /// NOT extensible to more general, proper WPF command binding (where we could have
    /// the same command bound to a menu item, toolbar button, or/and also a key combo.)
    /// </summary>
    class KeyKombo
    {
        private Key primary;
        private ModifierKeys modifier;
        private ExecutedRoutedEventHandler handler;
        
        /// <summary>
        /// Create a hotkey without a modifer key.
        /// </summary>
        /// <param name="key">Keycode for main key</param>
        /// <param name="handler">Event handling delegate</param>
        public KeyKombo(Key key, ExecutedRoutedEventHandler handler) : this(key, ModifierKeys.None, handler)
        {
        }
        
        /// <summary>
        /// Create a hotkey with a modifer key.
        /// </summary>
        /// <param name="key">Keycode for main key</param>
        /// <param name="modifier">Modifier key</param>
        /// <param name="handler">Event handling delegate</param>
        public KeyKombo(Key key, ModifierKeys modifier, ExecutedRoutedEventHandler handler)
        {
            this.primary = key;
            this.modifier = modifier;
            this.handler = handler;
        }

        /// <summary>
        /// Creates a CommandBinding from a RoutedCommand (to which we add an InputGesture using the
        /// instance's specified keys) and the instance's specified event handler.
        /// </summary>
        /// <returns></returns>
        public CommandBinding getCommandBinding()
        {
            RoutedCommand rc = new RoutedCommand();
            rc.InputGestures.Add(new KeyGesture(this.primary, this.modifier));
            return (new CommandBinding(rc, this.handler));
        }
    }
}
