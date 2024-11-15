using Proyecto.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ProductosViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Producto> Productos { get; set; } = new ObservableCollection<Producto>();

    public ProductosViewModel()
    {
        CargarProductos();
    }

    private async void CargarProductos()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://3017-190-0-245-162.ngrok-free.app"); // Cambia esta URL a la de tu API

            var response = await client.GetAsync("/Productoes"); // Endpoint de productos
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var productos = JsonSerializer.Deserialize<List<Producto>>(json);

                if (productos != null)
                {
                    foreach (var producto in productos)
                    {
                        Productos.Add(producto);
                    }
                }
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
