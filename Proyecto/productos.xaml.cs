using System.Collections.ObjectModel;
using Proyecto.Model;
namespace Proyecto;

public partial class Productos : ContentPage
{
    public ObservableCollection<Servicio> servicios { get; set; }
    public Productos()
	{
		InitializeComponent();


        servicios = new ObservableCollection<Servicio>
            {
                new Servicio { Nombre = "Crema Peinar", Imagen = "producto_1.jpg", Calificacion = "\u2605\u2605\u2605\u2606\u2606" },
                new Servicio { Nombre = "Gel", Imagen = "producto_2.jpg", Calificacion = "\u2605\u2605\u2605\u2606\u2606" },
                new Servicio { Nombre = "Espuma", Imagen = "producto_3.jpg", Calificacion = "\u2605\u2605\u2605\u2606\u2606" },
                new Servicio { Nombre = "Barba", Imagen = "producto_4jpg.jpg", Calificacion = "\u2605\u2605\u2605\u2606\u2606" }
            };

        ColecionServicios.ItemsSource = servicios;
    }
}