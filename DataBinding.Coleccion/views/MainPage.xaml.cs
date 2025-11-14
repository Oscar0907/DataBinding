using DataBinding.Coleccion.Models;
using System.Collections.ObjectModel;

namespace DataBinding.Coleccion.views;

public partial class MainPage : ContentPage
{
    public ObservableCollection<OrigenDePaquete> Origenes { get; }
    private OrigenDePaquete? _origenSeleccionado = null;
    private string? _nombreDelOrigen = string.Empty;
    private string? _rutaDelOrigen = string.Empty;
    public OrigenDePaquete? OrigenSeleccionado 
    {
        get => _origenSeleccionado;
        set 
        {
            if (_origenSeleccionado != value)
            {
                _origenSeleccionado = value;
                OnPropertyChanged(nameof(OrigenSeleccionado));
            }
        }
    }
    public string? NombreDelOrigen
    {
        get => _nombreDelOrigen;
        set
        {
            if ( _nombreDelOrigen != null)
            {
                _nombreDelOrigen = value;
                OnPropertyChanged(nameof(NombreDelOrigen));
            }
        }
    }
    public string? RutaDelOrigen
    {
        get => _rutaDelOrigen;
        set
        {
            if (_rutaDelOrigen != null)
            {
                _rutaDelOrigen = value;
                OnPropertyChanged(nameof(RutaDelOrigen));
            }
        }
    }
	public MainPage()
	{        
		InitializeComponent();
        OrigenDePaquete? OrigenSeleccionado = null;
		Origenes = new ObservableCollection<OrigenDePaquete>();
		CargarDatos();
               BindingContext = this;
        if ( Origenes.Count > 0)
        {
            OrigenSeleccionado = Origenes[1];
        }
    }
	private void CargarDatos() 
	{
		Origenes.Add(new OrigenDePaquete
		{
			Nombre = "Nugert.org",
			Origen ="https://api.nuget.org/v3/index.json",
			EstaHabilitado = true,
		});
        Origenes.Add(new OrigenDePaquete
        {
            Nombre = "Microsoft Visual Studio Offline Packages",
            Origen = $"c:/Program files(x86)/Microsoft 8bits/NuGetPackages",
            EstaHabilitado = false,
        });
    }

    private void OnAgregarButtonClicked(object sender, EventArgs e)
    {
        var origen = new OrigenDePaquete
        {
            Nombre = "Origen del paquete",
            Origen = "Url o ruta del origen del paquete",
            EstaHabilitado = false,
        };
        Origenes.Add(origen);
        OrigenSeleccionado = origen;
    }

    private void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        
        if (OrigenSeleccionado != null)
        {
            var indice = Origenes.IndexOf(OrigenSeleccionado);
            OrigenDePaquete? nuevoSeleccionado;
            if (Origenes.Count > 1)
            {
                // Hay mas de un elemento
                if (indice < Origenes.Count - 1) 
                {
                    // El elemento seleccionado no es el ultimo
                    nuevoSeleccionado = Origenes[indice + 1];
                }
                else 
                {
                    // El elemento seleccionado es el ultimo
                    nuevoSeleccionado = Origenes[indice - 1];
                }
            }
            else 
            {
                // Solo hay un elemento
                nuevoSeleccionado = null;
            }
             Origenes.Remove(OrigenSeleccionado);
            OrigenSeleccionado = nuevoSeleccionado;
        }        
    }

    private void OrigenesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        if (OrigenSeleccionado != null)
        {
            NombreDelOrigen = OrigenSeleccionado.Nombre;
            RutaDelOrigen = OrigenSeleccionado.Origen;
        }
        else
        {
            NombreDelOrigen = string.Empty;
            RutaDelOrigen = string.Empty;
        }
    }

    private void ActualizarButtonClicked(object sender, EventArgs e)
    {
        if (OrigenSeleccionado != null)
        {
            OrigenSeleccionado.Nombre = NombreDelOrigen;
            OrigenSeleccionado.Origen = RutaDelOrigen;
        }
    }
}