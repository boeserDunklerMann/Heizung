using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heizung.ReSTWrapper
{
	public class Messwert:BaseWrapper
	{
		private static readonly Lazy<Messwert> lazy = new Lazy<Messwert>(() => new Messwert());
		public static Messwert Instance => lazy.Value;

		public Messwert():base()
		{
			reSTUrl = "api/heizung/Messwert";
		}

		public async Task<Model.MessWert> WriteMesswert(Model.MessWert wert)
		{
			return await SendDataPUTReturnsModel<Model.MessWert>(wert);
		}
	}
}
