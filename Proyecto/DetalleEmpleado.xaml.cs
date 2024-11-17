
using Proyecto.Model;
using System.Collections.ObjectModel;

namespace Proyecto;

public partial class DetalleEmpleado : ContentPage
{
    public ObservableCollection<Citas> Citas { get; set; }
    public ObservableCollection<Servicio> Servicio { get; set; }  // Cambié el nombre a "Servicios" para mayor claridad

    public DetalleEmpleado()
    {
        InitializeComponent();
       
    }



    private async void VerMaServicios(object sender, EventArgs e)  // Cambié a "VerMasServicios" para mayor claridad
    {
        // Navegar a la página de servicios
        await Navigation.PushAsync(new ServicioEmpleado(Servicio));
    }

    private async void VerMasCitas(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CitaEmpleado());
    }

    private void IrServicio(object sender, TappedEventArgs e)
    {
        var frame = (Frame)sender;
        var itemSelected = frame.BindingContext as Servicio;

        if (itemSelected != null)
        {
            // Navegar a la página "DetalleServicios" pasando el objeto seleccionado
            NavigationToPage(new DetalleServicioEmpleado(itemSelected));
        }
    }

    private void IrCitas(object sender, TappedEventArgs e)
    {
        var frame = (Frame)sender;
        var itemSelected = frame.BindingContext as Citas;

        if (itemSelected != null)
        {
            NavigationToPage(new DetallesCitasEmpleado(itemSelected));
        }
    }


    private void NavigationToPage(ContentPage page)
    {
        if (App.InicioEmpleado != null)
        {
            App.InicioEmpleado.Detail.Navigation.PushAsync(page);
            App.InicioEmpleado.IsPresented = false;
        }
    }


}