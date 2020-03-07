using RegistroDetalle.BLL;
using RegistroDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegistroDetalle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<TelefonoDetalle> Telefonos { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.Telefonos = new List<TelefonoDetalle>();
            PersonaIdTex.Text = "0";
        }
        private void Limpiar()
        {
            PersonaIdTex.Text = "0";
            NombreTex.Text = string.Empty;
            DireccionTex.Text = string.Empty;
            CedulaTex.Text = string.Empty;
            FechaNacimientoDataPike.SelectedDate = DateTime.Now;
            TelefonoTex.Text = string.Empty;
            TipoTex.Text = string.Empty;

            this.Telefonos = new List<TelefonoDetalle>();
            CargarDataGrid();
        }
        private Personas LlenaClase()
        {
            Personas personas = new Personas();
            personas.PersonaId = Convert.ToInt32(PersonaIdTex.Text);
            personas.Nombre = NombreTex.Text;
            personas.Cedula = CedulaTex.Text;
            personas.Direccion = DireccionTex.Text;
            personas.FechaNacimiento = FechaNacimientoDataPike.SelectedDate.Value;

            personas.Telefonos = this.Telefonos;

            return personas;
        }        
        private void LlenaCampos(Personas personas)
        {
            PersonaIdTex.Text = Convert.ToString(personas.PersonaId);
            NombreTex.Text = personas.Nombre;
            DireccionTex.Text = personas.Direccion;
            CedulaTex.Text = Convert.ToString(personas.Cedula);
            FechaNacimientoDataPike.SelectedDate = personas.FechaNacimiento;

            this.Telefonos = personas.Telefonos;
            CargarDataGrid();

        }
        private void CargarDataGrid()
        {
            DetalleDataGrid.ItemsSource = null;
            DetalleDataGrid.ItemsSource = this.Telefonos;
        }
        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(NombreTex.Text))
            {
                MessageBox.Show("El Campo Nombre no puede estar vacio");
                NombreTex.Focus();
                paso = false;
            }
            else 
            {
                foreach (var caracter in NombreTex.Text)
                {
                                  
                    if (!char.IsLetter(caracter) && !char.IsWhiteSpace(caracter))
                    {
                        paso = false;
                        MessageBox.Show("El Campo nombre solo recibe TEXTO..."); 
                    }
                    else
                    if(!char.IsLetter(caracter) && !char.IsWhiteSpace(caracter))

                        paso = true;
                }              
            }

            if (string.IsNullOrWhiteSpace(DireccionTex.Text))
            {
                MessageBox.Show("El Campo Direccion Debe LLenarse..");
                DireccionTex.Focus();
                paso = false;
            }
            if(FechaNacimientoDataPike.Text == string.Empty)
            {
                MessageBox.Show("Seleccione una Fecha Por favor...");
                FechaNacimientoDataPike.Focus();
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(CedulaTex.Text))
            {
                MessageBox.Show("El Campo Cadula no puede estar vacio");
                CedulaTex.Focus();
                paso = false;
            }
            else
            {
                foreach (var caracter in CedulaTex.Text)
                {
                    if (!char.IsDigit(caracter))
                    {
                        paso = false;
                       MessageBox.Show("Escriba solo numeros en el campo CEDULA..");
                    }
                    else
                    if (!char.IsDigit(caracter))
                      paso = true;                    
                }              
            }
           
            if (DetalleDataGrid.Columns.Count == 0)
            {
                MessageBox.Show("Tiene qe agragar el TELEFONO y el TIPO para Guardar...");
                DetalleDataGrid.Focus();
                paso = false;
            }
            return paso;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Personas persona = PersonasBLL.Buscar(Convert.ToInt32(PersonaIdTex.Text));
            return (persona != null);
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Personas personas;
            bool paso = false;
            if (!Validar())
                return;

            personas = LlenaClase();

            if (Convert.ToInt32(PersonaIdTex.Text) == 0)
                paso = PersonasBLL.Guardar(personas);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede Modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = PersonasBLL.Modificar(personas);
            }
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo");
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(PersonaIdTex.Text, out id);

            Limpiar();
            if (PersonasBLL.Eliminar(id))

                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Erro Al eliminar una persona");
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {

            int id = Convert.ToInt32(PersonaIdTex.Text);
            Personas personas = new Personas();
            int.TryParse(PersonaIdTex.Text, out id);

            Limpiar(); 
            if ( personas != null )
            {
                personas = PersonasBLL.Buscar(id);         
                 
                if(personas != null)
                {
                    LlenaCampos(personas);
                    CargarDataGrid();
                 
                }else
                MessageBox.Show("Pernona no Existe....");
            }
            
        }

        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
           if(DetalleDataGrid.Columns.Count > 0 && DetalleDataGrid.SelectedCells!= null)
            {
                Telefonos.RemoveAt(DetalleDataGrid.SelectedIndex);
                CargarDataGrid();
            }
        }

        private void AgregarTipoButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (DetalleDataGrid.ItemsSource != null)
                this.Telefonos = (List < TelefonoDetalle >) DetalleDataGrid.ItemsSource;
            //todo: valida campo de detalle

            //agrega un nuevo detalle con los datos introducidos. 
            this.Telefonos.Add(new TelefonoDetalle(id: 0, personaId: Convert.ToInt32(PersonaIdTex.Text), telefono:TelefonoTex.Text,tipoTelefono:TipoTex.Text));
           
            CargarDataGrid();
            TelefonoTex.Focus();
            TelefonoTex.Clear();
            TipoTex.Clear();
        }

        private void NombreTex_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}
