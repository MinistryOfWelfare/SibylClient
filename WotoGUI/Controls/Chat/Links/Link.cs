using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WotoGUI.Controls.Chat.Links
{
	public class Link : IComparable<Link>
    {
        public virtual string Url { get; set; }
        public virtual int Index { get; set; }
        public virtual int Length { get; set; }
        public virtual LinkAction Action { get; set; }
        public virtual string Argument { get; set; }

        public Link(string url, int startIndex, int length, LinkAction action, string argument)
        {
            Url = url;
            Index = startIndex;
            Length = length;
            Action = action;
            Argument = argument;
        }

        public virtual bool Overlaps(Link otherLink) => 
			Index < otherLink.Index + otherLink.Length &&
			otherLink.Index < Index + Length;

        public virtual int CompareTo(Link otherLink) => 
			Index > otherLink.Index ? 1 : -1;
    }
}




