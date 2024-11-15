using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Proyecto.Model;


namespace Proyecto.Helpers
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }



        // Método GET
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var url = $"{_baseUrl}/{endpoint}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                throw new Exception($"Error al obtener datos: {response.ReasonPhrase}");
            }
        }

        // Método POST
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var url = $"{_baseUrl}/{endpoint}";
            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Respuesta del servidor: " + responseData);

                if (typeof(TResponse) == typeof(string))
                {
                    // Si TResponse es string, devuelve el responseData sin deserializar
                    return (TResponse)(object)responseData;
                }

                if (string.IsNullOrWhiteSpace(responseData))
                {
                    return default(TResponse); // Maneja respuestas vacías
                }

                try
                {
                    return JsonConvert.DeserializeObject<TResponse>(responseData);
                }
                catch (JsonReaderException ex)
                {
                    Console.WriteLine("Error de deserialización: " + ex.Message);
                    throw new Exception("Error al deserializar la respuesta JSON", ex);
                }
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error en la respuesta de la API: " + errorContent);
                throw new Exception($"Error al enviar datos: {response.ReasonPhrase}. Detalles: {errorContent}");
            }
        }




        // Método PUT
        public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var url = $"{_baseUrl}/{endpoint}";
            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(responseData);
            }
            else
            {
                throw new Exception($"Error al actualizar datos: {response.ReasonPhrase}");
            }
        }

        // Método DELETE
        public async Task<int> DeleteAsync(string endpoint)
        {
            var url = $"{_baseUrl}/{endpoint}";
            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

}