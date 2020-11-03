using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Heizung.ReSTWrapper
{
	/// <summary>
	/// Basisklasse für alle Aufrufe zur ReST-API
	/// </summary>
	public abstract class BaseWrapper
	{
		protected string baseUrl = "http://192.168.1.2:5003/";
		//protected string baseUrl = "https://andre-nas.servebeer.com:8080/";
		protected string reSTUrl;
		public string Url => $"{baseUrl}{reSTUrl}";
		protected System.Net.WebClient web;

		protected BaseWrapper()
		{
			web = new System.Net.WebClient();
		}

		// TODO try-catch

		/// <ChangeLog>
		/// <Create Datum="15.10.2020" Entwickler="DA" />
		/// </ChangeLog>
		/// <summary>
		/// Sendet Daten als JSON im Get-Body
		/// </summary>
		/// <param name="data"></param>
		/// <remarks>
		/// https://stackoverflow.com/questions/43421126/how-to-use-httpclient-to-send-content-in-body-of-get-request
		/// https://stackoverflow.com/a/47902348
		/// </remarks>
		protected async void SendDataGET(Model.BaseModel data)
		{
			HttpClient client = new HttpClient();

			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(Url),
				Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
			};
			var response = await client.SendAsync(request).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();
		}

		/// <ChangeLog>
		/// <Create Datum="15.10.2020" Entwickler="DA" />
		/// </ChangeLog>
		/// <summary>
		/// Sendet Daten als JSON im Get-Body, deserialisiert aus dem zurückgekommenen JSON ein Objekt
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <returns></returns>
		/// <remarks>
		/// https://stackoverflow.com/questions/43421126/how-to-use-httpclient-to-send-content-in-body-of-get-request
		/// https://stackoverflow.com/a/47902348
		/// </remarks>
		protected async Task<T> SendDataGETReturnsModel<T>(Model.BaseModel data = null)
		{
			HttpClient client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(Url),
				Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
			};
			var response = await client.SendAsync(request).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();
			var respBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			return JsonConvert.DeserializeObject<T>(respBody);
		}

		/// <ChangeLog>
		/// <Create Datum="15.10.2020" Entwickler="DA" />
		/// </ChangeLog>
		/// <summary>
		/// Sendet Daten im Post-Body
		/// </summary>
		protected async void SendDataPOST(Model.BaseModel data)
		{
			HttpClient client = new HttpClient();
			HttpRequestMessage request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri(Url),
				Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
			};
			HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();
		}

		/// <ChangeLog>
		/// <Create Datum="15.10.2020" Entwickler="DA" />
		/// </ChangeLog>
		/// <summary>
		/// Sendet Daten im Put-Body
		/// </summary>
		/// <ChangeLog>
		/// <Create Datum="15.10.2020" Entwickler="DA" />
		/// </ChangeLog>
		/// <summary>
		/// Sendet Daten im Put-Body
		/// </summary>
		protected async Task SendDataPUT(Model.BaseModel data)
		{
			HttpClient client = new HttpClient();
			HttpRequestMessage request = new HttpRequestMessage
			{
				Method = HttpMethod.Put,
				RequestUri = new Uri(Url),
				Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
			};
			HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();
		}

		/// <ChangeLog>
		/// <Create Datum="15.10.2020" Entwickler="DA" />
		/// </ChangeLog>
		/// <summary>
		/// Sendet Daten im Post-Body, deserialisiert aus dem zurückgekommenen JSON ein Objekt
		/// </summary>
		protected async Task<T> SendDataPOSTReturnsModel<T>(Model.BaseModel data)
		{
			HttpClient client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri(Url),
				Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
			};
			var response = await client.SendAsync(request).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();
			var respBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			return JsonConvert.DeserializeObject<T>(respBody);
		}
	}
}

