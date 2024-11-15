using Proyecto.Model;
using System.Collections.ObjectModel;

namespace Proyecto;

public partial class ServicioEmpleado : ContentPage
{
    public ObservableCollection<Servicio> Servicio { get; set; }

    public ServicioEmpleado(ObservableCollection<Servicio> servicios)
    {
        InitializeComponent();
        Servicio = servicios;
        BindingContext = this;
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
    private void NavigationToPage(ContentPage page)
    {
        if (App.FlyoutPage != null)
        {
            App.FlyoutPage.Detail.Navigation.PushAsync(page);
            App.FlyoutPage.IsPresented = false;
        }
    }
}