﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Scryber.Styles.Parsing
{
    public class CSSStyleParser : IEnumerable<StyleBase>
    {
        private List<CSSParsingError> _err;
        private static System.Text.RegularExpressions.Regex CommentMatcher = new System.Text.RegularExpressions.Regex("/\\*.*\\*/");
        public IEnumerable<CSSParsingError> Errors
        {
            get { return _err; }
        }

        public bool HasErrors
        {
            get { return this._err.Count > 0; }
        }

        public string Content { get; set; }

        public CSSStyleParser(string content)
        {
            this.Content = content;
            this._err = new List<CSSParsingError>();
        }

        public IEnumerator<StyleBase> GetEnumerator()
        {
            var content = this.Content;
            if (CommentMatcher.Match(content).Success)
                content = CommentMatcher.Replace(content, "");

            var strEnum = new StringEnumerator(content);
            return new CSSStyleEnumerator(strEnum, this);
        }

        internal void RegisterParsingError(int offset, string selector, Exception ex)
        {
            this._err.Add(new CSSParsingError(offset, selector, ex));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class CSSParsingError
    {
        public int Offest { get; set; }

        public string Selector { get; set; }

        public Exception Exception { get; set; }

        public CSSParsingError(int offset, string selector, Exception ex)
        {
            this.Offest = offset;
            this.Selector = selector;
            this.Exception = ex;
        }
    }
}