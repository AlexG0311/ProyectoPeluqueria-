using Proyecto.Model;

namespace Proyecto;

public partial class DetalleServicioEmpleado : ContentPage
{
	


    public Servicio Detalles { get; set; }

    public DetalleServicioEmpleado(Servicio detalle)
    {
        InitializeComponent();
        Detalles = detalle;
        BindingContext = this;
    }
}