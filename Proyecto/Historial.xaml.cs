using System.Collections.ObjectModel;

namespace Proyecto;

public partial class Historial : ContentPage
{
    public ObservableCollection<Appointment> Appointments { get; set; }

    public Historial()
	{
		InitializeComponent();
        Appointments = new ObservableCollection<Appointment>
            {
                new Appointment { Name = "Cita 1", Date = "12 ago 2024", Description = "Descripción breve de la cita.", Image = "appointment_placeholder.png", Price = "$99.99" },
                new Appointment { Name = "Cita 2", Date = "13 ago 2024", Description = "Descripción de otra cita.", Image = "appointment_placeholder.png", Price = "$99.99" },
                new Appointment { Name = "Cita 3", Date = "14 ago 2024", Description = "Más detalles de otra cita.", Image = "appointment_placeholder.png", Price = "$99.99" }
            };
        BindingContext = this;
    }
    public class Appointment
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
    }
}
