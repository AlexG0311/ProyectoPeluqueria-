using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Proyecto.Helpers;
using Proyecto.Model;

namespace Proyecto
{
    public partial class DetalleProductos : ContentPage
    {
        private readonly ApiService _apiService;
        private int ProductoId; // Este será el ID del producto que vas a consultar

        public Producto Producto { get; set; } // Propiedad para el BindingContext

        public DetalleProductos(int productoId)
        {
            InitializeComponent();
            _apiService = new ApiService(App.ApiBaseUrl); // Cambia la URL base según corresponda
            ProductoId = productoId;
            BindingContext = Producto;
            CargarProductoAsync();
        }

        // Método para cargar el producto desde la API
        private async Task CargarProductoAsync()
        {
            try
            {
                // Consumir la API para obtener los detalles del producto
                Producto = await _apiService.GetAsync<Producto>($"api/Productoes/{ProductoId}");

                // Establecer el BindingContext con el objeto Producto
                BindingContext = this;

                // Si no se encuentra el producto, mostrar un mensaje
                if (Producto == null)
                {
                    await DisplayAlert("Error", "No se pudo obtener el producto", "OK");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al cargar el producto: {ex.Message}");
                await DisplayAlert("Error", "Hubo un problema al obtener el producto. Intenta más tarde.", "Cerrar");
            }
        }
        private void AgregarAlCarrito(object sender, EventArgs e)
        {
            if (Producto != null)
            {
                var productoExistente = App.CarritoProductos.FirstOrDefault(p => p.idProducto == Producto.idProducto);

                if (productoExistente != null)
                {
                    productoExistente.Cantidad++;
                }
                else
                {
                    var nuevoProducto = new Producto
                    {
                        idProducto = Producto.idProducto,
                        Nombre = Producto.Nombre,
                        Descripcion = Producto.Descripcion,
                        Img = Producto.Img,
                        Precio = Producto.Precio,
                        Cantidad = 1
                    };
                    App.CarritoProductos.Add(nuevoProducto);
                }

                DisplayAlert("Carrito", $"{Producto.Nombre} fue agregado al carrito.", "OK");
            }
        }




    }
}

