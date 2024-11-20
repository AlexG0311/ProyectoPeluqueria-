using Microsoft.Maui.ApplicationModel.Communication;
using Proyecto.Model;
using System.Collections.ObjectModel;

namespace Proyecto;

public partial class Servicios : ContentPage
{

    public ObservableCollection<Producto> ProductosCombos { get; set; }
    public ObservableCollection<Producto> ProductosIndividuales { get; set; }
    public Servicios()
	{


		InitializeComponent();

       
    }
  


}
