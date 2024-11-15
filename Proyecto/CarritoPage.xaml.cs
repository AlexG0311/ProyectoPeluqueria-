using System.Collections.ObjectModel;
using Proyecto.Model;
using System.Windows.Input;

namespace Proyecto;

public partial class CarritoPage : ContentPage
{
    public ObservableCollection<Carrito> ProductosCarrito { get; set; }
    public ObservableCollection<string> MetodosPago { get; set; }
    public ICommand PagarCommand { get; set; }
    private double _total;

    public double Total
    {
        get => _total;
        set
        {
            _total = value;
            OnPropertyChanged();  // Notificar cambios en la UI
        }
    }
    public string MetodoPagoSeleccionado { get; set; }
    public CarritoPage()
	{
		InitializeComponent();

        // Lista de productos en el carrito
        ProductosCarrito = new ObservableCollection<Carrito>
            {
                new Carrito { Nombre = "No se q es", Precio = 10000, Cantidad = 1, Imagen = "rray.png" },
                new Carrito { Nombre = "No se", Precio = 20000, Cantidad = 1, Imagen = "rray.png" }
            };

        foreach (var producto in ProductosCarrito)
        {
            producto.PropertyChanged += Producto_PropertyChanged;
        }

        // M�todos de pago
        MetodosPago = new ObservableCollection<string>
            {
                "Tarjeta de cr�dito",
                "PayPal",
                "Efectivo"
            };

        // Comando de pagar
        PagarCommand = new Command(() => Pagar());
        // Calcular total

        RecalcularTotal();

       

        BindingContext = this;

    }





    // Recalcula el total cada vez que cambian las cantidades de los productos
    private void Producto_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Carrito.Cantidad))
        {
            RecalcularTotal();
        }
    }

    private void RecalcularTotal()
    {
        Total = ProductosCarrito.Sum(p => p.PrecioTotal);
    }


    private void Pagar()
    {
        // L�gica para procesar el pago
        DisplayAlert("Pagar", "Procesando el pago", "OK");
    }
}