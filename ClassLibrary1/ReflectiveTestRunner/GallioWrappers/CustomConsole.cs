﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallio.Runtime.ConsoleSupport;
using Action = Gallio.Common.Action;

namespace ClassLibrary1.ReflectiveTestRunner.GallioWrappers
{
    class CustomConsole : IRichConsole 
    {
        public void ResetColor()
        {
            Console.ResetColor();
        }


        public void SetFooter(Action showFooter, Action hideFooter)
        {

        }

        public void Write(char c)
        {
            throw new NotImplementedException();
        }

        public void Write(string str)
        {
            throw new NotImplementedException();
        }

        public void WriteLine()
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string str)
        {
            throw new NotImplementedException();
        }

        public object SyncRoot { get; private set; }
        public bool IsCancelationEnabled { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsRedirected { get; private set; }
        public TextWriter Error { get; private set; }
        public TextWriter Out { get; private set; }
        public ConsoleColor ForegroundColor { get; set; }
        public int CursorLeft { get; set; }
        public int CursorTop { get; set; }
        public string Title { get; set; }
        public int Width { get; private set; }
        public bool FooterVisible { get; set; }
        public event EventHandler Cancel;
    }
}
