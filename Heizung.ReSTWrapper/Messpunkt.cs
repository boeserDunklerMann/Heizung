using System;
using System.Collections.Generic;
using System.Text;

namespace Heizung.ReSTWrapper
{
	public class Messpunkt : BaseWrapper
	{
		public Messpunkt():base()
		{
			reSTUrl = "api/heizung/messpunkt";
		}

		public async void WriteMesspunkt(Model.Messpunkt messpunkt)
		{
			await SendDataPUT(messpunkt);
		}
	}
}
