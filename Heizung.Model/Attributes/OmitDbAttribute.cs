using System;

namespace Heizung.Model.Attributes
{
	/// <summary>
	/// Markiert ein Property, dass diese nicht aus der DB geladen wird, weil sie sich bspw. selbst berechnet, oder eine Liste ist.
	/// </summary>
	public class OmitDbAttribute : Attribute
	{
	}
}
