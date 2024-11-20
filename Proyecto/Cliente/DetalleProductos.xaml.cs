using System;
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
            _apiService = new ApiService("https://374b-181-78-20-113.ngrok-free.app"); // Cambia la URL base según corresponda
            ProductoId = productoId;
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


        private async void Insertar(object sender, EventArgs e)
        {
            // Crear un objeto de tipo ReseñaProductoDTO para enviar los datos a la API
            ReseñaProductoDTO reseñaProducto = LlenarReseñaProducto();

            if (reseñaProducto != null) // Asegúrate de que el objeto no sea null antes de enviarlo
            {
                try
                {
                    // Llamada al servicio API para registrar la reseña
                    ReseñaProductoDTO result = await _apiService.PostAsync<ReseñaProductoDTO, ReseñaProductoDTO>("api/ReseñaProducto", reseñaProducto);

                    if (result != null)
                    {
                        // Mostrar mensaje de éxito si la reseña se envía correctamente
                        await DisplayAlert("Reseña", "¡Reseña enviada exitosamente!", "Ok");
                    }
                    else
                    {
                        // Mostrar mensaje de error si el envío falla
                        await DisplayAlert("Error", "Hubo un problema al enviar la reseña.", "Cerrar");
                    }
                }
                catch (Exception ex)
                {
                    // Manejar errores de red o de la API
                    await DisplayAlert("Error", $"Hubo un problema al enviar la reseña: {ex.Message}", "Cerrar");
                }
            }
        }



        private ReseñaProductoDTO LlenarReseñaProducto()
        {
            // Obtén el idCliente del usuario actual (que es el que acaba de iniciar sesión)
            int clienteId = (App.CurrentUser as ClienteDTOO)?.idCliente ?? 0;

            if (clienteId == 0)
            {
                DisplayAlert("Error", "El usuario no está autenticado", "Cerrar");
                return null; // Si no está autenticado, no puedes enviar la reseña
            }

            var reseñaProducto = new ReseñaProductoDTO
            {
                Estrellas = (int)EstrellasSlider.Value, // Valor de las estrellas del slider
                Comentario = ComentarioEditor.Text,    // Texto del comentario del editor
                Producto_idProducto = Producto.idProducto, // ID del producto que se está reseñando
                Cliente_idCliente = clienteId, // Usar el idCliente del usuario autenticado
            };

            // Verificar si los campos esenciales no están vacíos
            if (string.IsNullOrEmpty(reseñaProducto.Comentario) || reseñaProducto.Estrellas == 0)
            {
                DisplayAlert("Error", "Debe completar todos los campos de la reseña.", "Cerrar");
                return null; // No enviar la reseña si los campos están vacíos
            }

            return reseñaProducto;
        }


    }
}

