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
        private int ProductoId; // Este ser� el ID del producto que vas a consultar

        public Producto Producto { get; set; } // Propiedad para el BindingContext

        public DetalleProductos(int productoId)
        {
            InitializeComponent();
            _apiService = new ApiService("https://374b-181-78-20-113.ngrok-free.app"); // Cambia la URL base seg�n corresponda
            ProductoId = productoId;
            CargarProductoAsync();
        }

        // M�todo para cargar el producto desde la API
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
                await DisplayAlert("Error", "Hubo un problema al obtener el producto. Intenta m�s tarde.", "Cerrar");
            }
        }


        private async void Insertar(object sender, EventArgs e)
        {
            // Crear un objeto de tipo Rese�aProductoDTO para enviar los datos a la API
            Rese�aProductoDTO rese�aProducto = LlenarRese�aProducto();

            if (rese�aProducto != null) // Aseg�rate de que el objeto no sea null antes de enviarlo
            {
                try
                {
                    // Llamada al servicio API para registrar la rese�a
                    Rese�aProductoDTO result = await _apiService.PostAsync<Rese�aProductoDTO, Rese�aProductoDTO>("api/Rese�aProducto", rese�aProducto);

                    if (result != null)
                    {
                        // Mostrar mensaje de �xito si la rese�a se env�a correctamente
                        await DisplayAlert("Rese�a", "�Rese�a enviada exitosamente!", "Ok");
                    }
                    else
                    {
                        // Mostrar mensaje de error si el env�o falla
                        await DisplayAlert("Error", "Hubo un problema al enviar la rese�a.", "Cerrar");
                    }
                }
                catch (Exception ex)
                {
                    // Manejar errores de red o de la API
                    await DisplayAlert("Error", $"Hubo un problema al enviar la rese�a: {ex.Message}", "Cerrar");
                }
            }
        }



        private Rese�aProductoDTO LlenarRese�aProducto()
        {
            // Obt�n el idCliente del usuario actual (que es el que acaba de iniciar sesi�n)
            int clienteId = (App.CurrentUser as ClienteDTOO)?.idCliente ?? 0;

            if (clienteId == 0)
            {
                DisplayAlert("Error", "El usuario no est� autenticado", "Cerrar");
                return null; // Si no est� autenticado, no puedes enviar la rese�a
            }

            var rese�aProducto = new Rese�aProductoDTO
            {
                Estrellas = (int)EstrellasSlider.Value, // Valor de las estrellas del slider
                Comentario = ComentarioEditor.Text,    // Texto del comentario del editor
                Producto_idProducto = Producto.idProducto, // ID del producto que se est� rese�ando
                Cliente_idCliente = clienteId, // Usar el idCliente del usuario autenticado
            };

            // Verificar si los campos esenciales no est�n vac�os
            if (string.IsNullOrEmpty(rese�aProducto.Comentario) || rese�aProducto.Estrellas == 0)
            {
                DisplayAlert("Error", "Debe completar todos los campos de la rese�a.", "Cerrar");
                return null; // No enviar la rese�a si los campos est�n vac�os
            }

            return rese�aProducto;
        }


    }
}

