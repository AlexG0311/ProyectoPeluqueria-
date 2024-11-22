using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Proyecto.Model;

namespace Proyecto.Cliente
{
    public partial class FacturaPage : ContentPage
    {
        public ObservableCollection<ProductoFactura> ProductosFactura { get; set; }
        public decimal Total => ProductosFactura.Sum(p => p.Subtotal);

        public ICommand FinalizarCommand { get; }

        public FacturaPage()
        {
            InitializeComponent();

            // Mapear productos del carrito a productos para la factura
            ProductosFactura = new ObservableCollection<ProductoFactura>(
                App.CarritoProductos.Select(p => new ProductoFactura
                {
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Cantidad = p.Cantidad,
                    Subtotal = p.Precio * p.Cantidad,
                    Img = p.Img
                }));

            FinalizarCommand = new Command(Finalizar);
            BindingContext = this;
        }

        private async void Finalizar()
        {
            // Acción al finalizar, por ejemplo, regresar a la página principal
            await DisplayAlert("Gracias", "Compra finalizada. ¡Gracias por tu compra!", "OK");
            await Navigation.PopToRootAsync();
        }
    }

    public class ProductoFactura
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public string Img { get; set; }
    }
}
