using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using Mimica.Model;

namespace Mimica.ViewModel
{
    public class JogoViewModel : INotifyPropertyChanged
    {
        public Grupo Grupo { get; set; }
        public string NumeroGrupo { get; set; }
        public string NomeGrupo { get; set; }
        public byte PalavraPontuacao { get { return _PalavraPontuacao; } set { _PalavraPontuacao = value; OnPropertyChanged("PalavraPontuacao"); } }
        private byte _PalavraPontuacao;

        public string TextoContagem { get { return _TextoContagem; } set { _TextoContagem = value; OnPropertyChanged("TextoContagem"); } }
        private string _TextoContagem;

        public string Palavra { get { return _Palavra; } set { _Palavra = value; OnPropertyChanged("Palavra"); } }
        private string _Palavra;

        public bool IsVisibleContainerContagem { get { return _IsVisibleContainerContagem; } set { _IsVisibleContainerContagem = value; OnPropertyChanged("IsVisibleContainerContagem"); } }
        private bool _IsVisibleContainerContagem;


        public bool IsVisibleBtnIniciar { get { return _IsVisibleBtnIniciar; } set { _IsVisibleBtnIniciar = value; OnPropertyChanged("IsVisibleBtnIniciar"); } }
        private bool _IsVisibleBtnIniciar;

        public bool IsVisibleBtnMostrar { get { return _IsVisibleBtnMostrar; } set { _IsVisibleBtnMostrar = value; OnPropertyChanged("IsVisibleBtnMostrar"); } }
        private bool _IsVisibleBtnMostrar;

        public Command MostrarPalavra { get; set; }
        public bool ContainerContagem { get; set; }
        public Command Acertou { get; set; }
        public Command Error { get; set; }
        public Command Iniciar { get; set; }




        public JogoViewModel(Grupo grupo)
        {
            Grupo = grupo;
            NomeGrupo = grupo.Nome;
            if(grupo == Armazenamento.Armazenamento.Jogo.Grupo1)
            {
                NumeroGrupo = "Grupo 1: ";
            }
            else
            {
                NumeroGrupo = "Grupo 2: ";
            }

            IsVisibleContainerContagem = false;
            IsVisibleBtnIniciar = false;
            IsVisibleBtnMostrar = true;

            Palavra = "****************";
            ContainerContagem = false;
            MostrarPalavra = new Command(MostrarPalavraAction);
            Acertou = new Command(AcertouActions);
            Error = new Command(ErrouAction);
            Iniciar = new Command(IniciarAction);
        }

        private void ErrouAction()
        {
            
            GoProximoGrupo();
            //TODO - Ir tela do jogo trocando os grupos
        }
        private void AcertouActions()
        {
            Grupo.Pontuacao += PalavraPontuacao;
            GoProximoGrupo();


            //TODO - Ir tela do jogo trocando os grupos
        }
        private void GoProximoGrupo()
        {
            //TODO - Verificar rodada terminou
            Grupo grupo;
            if (Armazenamento.Armazenamento.Jogo.Grupo1 == Grupo)
            {
                grupo = Armazenamento.Armazenamento.Jogo.Grupo2;
            }
            else
            {
                grupo = Armazenamento.Armazenamento.Jogo.Grupo1;
                Armazenamento.Armazenamento.RodadaAtual++;

            }
            if (Armazenamento.Armazenamento.RodadaAtual > Armazenamento.Armazenamento.Jogo.Rodadas)
            {
                App.Current.MainPage = new View.Resultado();
            }
            else
            {
                App.Current.MainPage = new View.Jogo(grupo);
            }
        }

        private void MostrarPalavraAction()
        {
            var NumNivel = Armazenamento.Armazenamento.Jogo.NivelNumerico;
            
            if(NumNivel == 0)
            {
                Random rd = new Random();
                int niv = rd.Next(0, 3);
                int ind = rd.Next(0, Armazenamento.Armazenamento.Palavras[niv].Length);
                Palavra = Armazenamento.Armazenamento.Palavras[niv][ind];
                PalavraPontuacao = (Byte) ((niv == 0) ? 1 : (niv == 1) ? 3 : 5);

            }
            if (NumNivel == 1)
            {
                Random rd = new Random();
                int ind = rd.Next(0, Armazenamento.Armazenamento.Palavras[NumNivel - 1].Length);
                Palavra = Armazenamento.Armazenamento.Palavras[NumNivel - 1][ind];
                PalavraPontuacao = 1;
            }
            if (NumNivel == 2)
            {
                Random rd = new Random();
                int ind = rd.Next(0, Armazenamento.Armazenamento.Palavras[NumNivel - 1].Length);
                Palavra = Armazenamento.Armazenamento.Palavras[NumNivel - 1][ind];
                PalavraPontuacao = 3;
            }
            if (NumNivel == 3)
            {
                Random rd = new Random();
                int ind = rd.Next(0, Armazenamento.Armazenamento.Palavras[NumNivel - 1].Length);
                Palavra = Armazenamento.Armazenamento.Palavras[NumNivel - 1][ind];
                PalavraPontuacao = 5;
            }


            IsVisibleBtnMostrar = false;
            IsVisibleBtnIniciar = true;


        }
        private void IniciarAction()
        {
            IsVisibleBtnIniciar = false;
            IsVisibleContainerContagem = true;
            int i = Armazenamento.Armazenamento.Jogo.TempoPalavra;
            TextoContagem = i.ToString();
            i--;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                TextoContagem = i.ToString(); i--;
                if (i < 0)
                {
                    TextoContagem = "Esgotou o tempo";
                }
                return true;
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string NameProperty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
            }
        }
    }

}
