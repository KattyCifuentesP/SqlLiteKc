using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SqlLiteKc.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqlLiteKc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection _con;
        private ObservableCollection<Estudiante> _tablaEstudiante;
        public ConsultaRegistro()
        {
            InitializeComponent();
            _con = DependencyService.Get<DataBase>().GetConnection();
        }

        protected async override void OnAppearing()
        {
            var resultado = await _con.Table<Estudiante>().ToListAsync();
            _tablaEstudiante = new ObservableCollection<Estudiante>(resultado);
            ListaUsuarios.ItemsSource = _tablaEstudiante;
            base.OnAppearing();

        }

        private void ListaUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (Estudiante)e.SelectedItem;
            var item = Obj.Id.ToString();
            int ID = Convert.ToInt32(item);
            string Nombre = Obj.Nombre;
            string Usuario = Obj.Usuario;
            string Contrasena = Obj.Contrasena;
            try
            {
                Navigation.PushAsync(new Elemento(ID,Nombre,Usuario,Contrasena));
            }
            catch(Exception ex)
            {
                DisplayAlert("Alerta", "Error en el proceso " + ex.Message, "ok");
            }
        }
    }
}