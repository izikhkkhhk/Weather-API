using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Pogoda
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiUrl = "https://api.open-meteo.com/v1/forecast?latitude=54.36&longitude=18.65&current_weather=true&hourly=temperature_2m,relative_humidity_2m,precipitation";

        public Form1()
        {
            InitializeComponent();
            FetchWeatherData();
        }

        private async Task FetchWeatherData()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(ApiUrl);
                var parsedData = JObject.Parse(response);
                var currentWeather = parsedData["current_weather"];
                Temperatura.Text = $"Temperatura: {currentWeather["temperature"]}°C";
                var hourlyData = parsedData["hourly"];
                var humidity = hourlyData["relative_humidity_2m"].First;
                label2.Text = $"Wilgotność: {humidity}%";
                var precipitation = hourlyData["precipitation"].First;
                label3.Text = $"Opady atmosferyczne: {precipitation} mm";
                label4.Text = $"Ostatnia aktualizacja: {DateTime.Now}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas pobierania danych o pogodzie: {ex.Message}");
            }
        }
    }
}