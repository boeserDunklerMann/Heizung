using System;
using System.Collections.Generic;
using System.Text;

namespace Heizung.Model.Attributes
{
	/// <summary>
	/// Mappt eine Property zu einem SqlParameter, damit wir beim Schreiben wissen, welche Property in welchen Parameter gehört.
	/// </summary>
	public sealed class SqlParameterNameAttribute : Attribute
	{
		public string ParameterName { get; private set; }
		public SqlParameterNameAttribute(string parameterName)
		{
			if (parameterName[0] != '?')
				ParameterName = $"?{parameterName}";    // HINT: MySql-specific
			else
				ParameterName = parameterName;
		}
	}
}