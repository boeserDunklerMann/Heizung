using System;
using Heizung.DBAccess;

namespace Heizung.Test.Cons
{
	class Program
	{
		static void Main(string[] args)
		{
			DBAccess.MySql db = DBAccess.MySql.Instance;
			db.SetConnection("heizung", "heizung", "192.168.1.3", "Heizung");
			var wohnungen = db.LoadAll();
		}
	}
}
