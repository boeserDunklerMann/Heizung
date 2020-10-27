using System;
using System.Collections.Generic;
using System.Text;

namespace Heizung.Model.Attributes
{
	public sealed class StringAttribute : Attribute
	{
		public int MaxLength { get; set; }
		public StringAttribute(int maxLength)
		{
			MaxLength = maxLength;
		}
	}
}
