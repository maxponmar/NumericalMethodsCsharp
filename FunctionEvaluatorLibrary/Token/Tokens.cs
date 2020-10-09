using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FunctionEvaluatorLibrary.Token
{
    class Tokens
    {
        private List<string> tokens;        

        public Tokens()
        {
            tokens = new List<string>();            
        }

        public void addToken(string token)
        {
            tokens.Add(token);
        }

        public int Count()
        {
            return tokens.Count;
        }        

        public int indexOf(string key)
        {
            return tokens.IndexOf(key);
        }

        public int indexOf(string key, int index)
        {
            return tokens.IndexOf(key, index);
        }

        public int lastIndexOf(string key)
        {
            return tokens.LastIndexOf(key);
        }

        public void removeRange(int index, int count)
        {
            tokens.RemoveRange(index, count);
        }

        public void removeAt(int index)
        {
            tokens.RemoveAt(index);
        }

        public bool contains(string key)
        {
            return tokens.Contains(key);
        }

        public string this[int index]
        {
            get => tokens[index];
            set => tokens[index] = value;
        }
    }
}
