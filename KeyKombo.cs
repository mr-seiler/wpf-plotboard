using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PlottingBoard
{
    class KeyKombo
    {
        private Key primary;
        private ModifierKeys modifier;
        private ExecutedRoutedEventHandler handler;
        
        public KeyKombo(Key key, ExecutedRoutedEventHandler handler) : this(key, ModifierKeys.None, handler)
        {
        }
        
        public KeyKombo(Key key, ModifierKeys modifier, ExecutedRoutedEventHandler handler)
        {
            this.primary = key;
            this.modifier = modifier;
            this.handler = handler;
        }

        public CommandBinding getCommandBinding()
        {
            RoutedCommand rc = new RoutedCommand();
            rc.InputGestures.Add(new KeyGesture(this.primary, this.modifier));
            return (new CommandBinding(rc, this.handler));
        }
    }
}
