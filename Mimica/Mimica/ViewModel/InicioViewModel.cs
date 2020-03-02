using System;
using System.Collections.Generic;
using System.Text;
using Mimica.Model;
using System.ComponentModel;
using Xamarin.Forms;
using Mimica.Armazenamento;

namespace Mimica.ViewModel
{
    public class InicioViewModel : INotifyPropertyChanged
    {
        private string _MsgErro;
        public Jogo Jogo { get; set; }
        public string MsgErro { get { return _MsgErro; } set { _MsgErro = value; OnPropertyChanged("MsgErro"); } }
        public Command IniciarCommand { get; set; }
        public InicioViewModel()
        {
            IniciarCommand = new Command(IniciarJogo);
            Jogo = new Jogo();
            Jogo.Grupo1 = new Grupo();
            Jogo.Grupo2 = new Grupo();

            Jogo.TempoPalavra = 180;
            Jogo.Rodadas = 1;
        }
        private void IniciarJogo()
        {
            string error = "";
            if(Jogo.TempoPalavra < 20)
            {
                error += "O tempo minimo para o tempo da palavra e 10 segundos";
            }
            if(Jogo.Rodadas <= 0)
            {
                error += "\nO valor minimo para a rodada e 1";
            }
            if(error.Length > 0)
            {
                MsgErro = error;
            }
            else
            {
                Armazenamento.Armazenamento.Jogo = this.Jogo;
                Armazenamento.Armazenamento.RodadaAtual = 1;
                App.Current.MainPage = new View.Jogo(Jogo.Grupo1);
            }
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string NameProperty)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
            }
        }
    }
}
