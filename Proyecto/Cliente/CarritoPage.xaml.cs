using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Proyecto.Cliente;
using Proyecto.Helpers;
using Proyecto.Model;

namespace Proyecto
{
    public partial class CarritoPage : ContentPage
    {
        public ObservableCollection<Producto> CarritoProductos { get; set; }
        public decimal Total => CarritoProductos.Sum(p => p.Precio * p.Cantidad);

        public ICommand AumentarCantidadCommand { get; }
        public ICommand DisminuirCantidadCommand { get; }
        public ICommand EliminarProductoCommand { get; }
        public ICommand PagarCommand { get; }

        public CarritoPage()
        {
            InitializeComponent();

            CarritoProductos = App.CarritoProductos; // Enlace con la colección global
            AumentarCantidadCommand = new Command<Producto>(AumentarCantidad);
            DisminuirCantidadCommand = new Command<Producto>(DisminuirCantidad);
            EliminarProductoCommand = new Command<Producto>(EliminarProducto);
            PagarCommand = new Command(Pagar);

            BindingContext = this;
        }

        private void AumentarCantidad(Producto producto)
        {
            if (producto != null)
            {
                producto.Cantidad++;
                OnPropertyChanged(nameof(Total));
            }
        }

        private void DisminuirCantidad(Producto producto)
        {
            if (producto != null && producto.Cantidad > 1)
            {
                producto.Cantidad--;
                OnPropertyChanged(nameof(Total));
            }
        }

        private void EliminarProducto(Producto producto)
        {
            if (producto != null)
            {
                CarritoProductos.Remove(producto);
                OnPropertyChanged(nameof(Total));
            }
        }


        private async void Pagar()
        {
            if (CarritoProductos.Any())
            {
                await DisplayAlert("Pago", $"El total a pagar es: ${Total:F2}", "OK");

                // Navegar a la página de factura
                await Navigation.PushAsync(new FacturaPage());

                // Limpiar el carrito (puedes hacerlo aquí o después de finalizar la compra en FacturaPage)
                CarritoProductos.Clear();
                OnPropertyChanged(nameof(Total));
            }
            else
            {
                await DisplayAlert("Carrito vacío", "No hay productos en el carrito para pagar.", "OK");
            }
        }




    }
}
