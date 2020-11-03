using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heizung.ReSTWrapper
{
	public class Raum : BaseWrapper
	{
		private static readonly Lazy<Raum> lazy = new Lazy<Raum>(() => new Raum());
		public static Raum Instance => lazy.Value;
		private Raum()
			:base()
		{
			reSTUrl = "api/heizung/raum";
		}

		/// <summary>
		/// Lädt alle Räume einer Wohnung
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public async Task<List<Model.Raum>> GetRaeume(Model.Wohnung wohnung)
		{
			return await SendDataGETReturnsModel<List<Model.Raum>>(wohnung);
		}

		public async void WriteRaum(Model.Raum raum)
		{
			await SendDataPUT(raum);
		}
	}
}
