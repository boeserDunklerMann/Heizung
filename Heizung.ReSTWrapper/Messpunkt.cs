using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heizung.ReSTWrapper
{
	public class Messpunkt : BaseWrapper
	{
		private static readonly Lazy<Messpunkt> lazy = new Lazy<Messpunkt>(() => new Messpunkt());
		public static Messpunkt Instance => lazy.Value;

		public Messpunkt():base()
		{
			reSTUrl = "api/heizung/messpunkt";
		}

		public async Task<Model.Messpunkt> WriteMesspunkt(Model.Messpunkt messpunkt)
		{
			return await SendDataPUTReturnsModel<Model.Messpunkt>(messpunkt);
		}
	}
}
