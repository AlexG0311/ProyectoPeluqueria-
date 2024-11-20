using System.Collections.ObjectModel;
using System.Diagnostics;
using Proyecto.Helpers;
using Proyecto.Model;

namespace Proyecto;

public partial class Detalle : ContentPage
{
    private readonly ApiService _apiService;

    public ObservableCollection<Producto> Productos { get; set; } = new ObservableCollection<Producto>();
    public ObservableCollection<Servicio> Servicios { get; set; } = new ObservableCollection<Servicio>(); // Colección para servicios

    public Detalle()
    {
        InitializeComponent();

        _apiService = new ApiService("https://374b-181-78-20-113.ngrok-free.app"); // Cambia la URL de la API a la correcta
        BindingContext = this;


        CargarDatos(); // Carga productos y servicios
    }

    private async void CargarDatos()
    {
        // Llama a los métodos para cargar productos y servicios
        await CargarProductos();
        await CargarServicios();
    }

    private async Task CargarProductos()
    {
        try
        {
            // Llamada a la API para obtener los productos
            List<Producto> productosDesdeApi = await _apiService.GetAsync<List<Producto>>("api/Productoes");
            Productos.Clear();

            // Agrega cada producto a la colección observable
            foreach (var producto in productosDesdeApi)
            {
                Productos.Add(producto);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar los productos: {ex.Message}", "OK");
        }
    }

    private async Task CargarServicios()
    {
        try
        {
            // Llamada a la API para obtener los servicios
            List<Servicio> serviciosDesdeApi = await _apiService.GetAsync<List<Servicio>>("api/Servicios");
            Servicios.Clear();

            // Agrega cada servicio a la colección observable
            foreach (var servicio in serviciosDesdeApi)
            {
                Servicios.Add(servicio);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar los servicios: {ex.Message}", "OK");
        }
    }

    private async void OnCartTapped(object sender, EventArgs e)
    {
        // Simula un efecto de "presionado" reduciendo el tamaño y volviendo a su estado original
        await CarritoFrame.ScaleTo(0.9, 100);  // Reducción de tamaño al presionar
        await CarritoFrame.ScaleTo(1, 100);    // Regresa al tamaño original

        // Navega a la página del carrito
        await Navigation.PushAsync(new CarritoPage());
    }

    private async void VerMaServicios(object sender, EventArgs e)
    {
        // Navegar a la página de destino (reemplaza DestinationPage con la página correspondiente)
        await Navigation.PushAsync(new Servicios());
    }

    private async void VerMasProductos(object sender, EventArgs e)
    {
        // Navegar a la página de destino (reemplaza DestinationPage con la página correspondiente)
        await Navigation.PushAsync(new Productos());
    }

    private async void IrProductos(object sender, EventArgs e)
    {
        // Obtener el producto seleccionado desde el contexto del elemento
        var frame = sender as Frame;
        var productoSeleccionado = frame?.BindingContext as Producto;

        if (productoSeleccionado != null)
        {
            // Navegar a la página de detalles pasando el ID del producto seleccionado
            await Navigation.PushAsync(new DetalleProductos(productoSeleccionado.idProducto));
        }
        else
        {
            await DisplayAlert("Error", "No se pudo obtener la información del producto seleccionado.", "OK");
        }
    }

    private async void IrServicio(object sender, TappedEventArgs e)
    {
        var frame = sender as Frame;
        var servicioSeleccionado = frame?.BindingContext as Servicio;
        if (servicioSeleccionado != null)
        {
            App.CurrentServicio = servicioSeleccionado;
            Debug.WriteLine($"Servicio seleccionado para reserva: {servicioSeleccionado.Nombre}");
            await DisplayAlert("Servicio Seleccionado", $"Seleccionaste el servicio: {App.CurrentServicio.Nombre}", "OK");
        }

        // Navegar a la página de reserva
        await Navigation.PushAsync(new Reserva());
    }

    private async void AgregarAlCarrito(object sender, EventArgs e)
    {
        var button = sender as Button;
        var productoSeleccionado = button?.BindingContext as Producto;

        if (productoSeleccionado != null)
        {
            // Verifica si el producto ya está en el carrito
            var productoExistente = App.CarritoProductos.FirstOrDefault(p => p.idProducto == productoSeleccionado.idProducto);

            if (productoExistente == null)
            {
                // Si no está, agrégalo al carrito con cantidad inicial 1
                productoSeleccionado.Cantidad = 1;
                App.CarritoProductos.Add(productoSeleccionado);
            }
            else
            {
                // Si ya está, aumenta la cantidad
                productoExistente.Cantidad++;
            }

            await DisplayAlert("Carrito", $"Añadido al carrito: {productoSeleccionado.Nombre}", "OK");
        }
    }

    private void OnServicioSelected(object sender, SelectionChangedEventArgs e)
    {
        var servicioSeleccionado = e.CurrentSelection.FirstOrDefault() as Servicio;
        if (servicioSeleccionado != null)
        {
            App.CurrentServicio = servicioSeleccionado; // Guardar el servicio seleccionado
            Debug.WriteLine($"Servicio seleccionado: {servicioSeleccionado.Nombre}");
            DisplayAlert("Servicio", $"Seleccionaste el servicio: {servicioSeleccionado.Nombre}", "OK");
        }
    }

    private void NavigationToPage(ContentPage page)
    {
        App.FlyoutPage.Detail.Navigation.PushAsync(page);
        App.FlyoutPage.IsPresented = false;
    }
}
