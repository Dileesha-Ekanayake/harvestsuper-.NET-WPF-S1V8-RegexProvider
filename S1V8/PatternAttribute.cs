using System;
using System.Collections.Generic;
using System.Reflection;

namespace S1V8
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class PatternAttribute : Attribute
    {
        public string Regex { get; }
        public PatternAttribute(string regex)
        {
            Regex = regex;
        }
    }
}
