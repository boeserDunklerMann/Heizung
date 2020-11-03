using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heizung.ReSTWrapper
{
	public class Wohnung : BaseWrapper
	{
		private static readonly Lazy<Wohnung> lazy = new Lazy<Wohnung>(() => new Wohnung());
		public static Wohnung Instance => lazy.Value;

		private Wohnung() : base()
		{
			reSTUrl = "api/heizung/wohnung";
		}

		public async Task<List<Model.Wohnung>> GetAllWohnungen()
		{
			return await SendDataGETReturnsModel<List<Model.Wohnung>>();
		}

		public async void WriteWohnung(Model.Wohnung wohnung)
		{
			await SendDataPUT(wohnung);
		}
	}
}