using CMP.Vistas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamForms.Controls;

namespace CMP.VistaModelo
{
    internal class VMCalendario : BaseViewModel
    {
        #region VARIABLES
        private DateTime? _date;
        private ObservableCollection<SpecialDate> attendances;
        #endregion

        #region CONSTRUCTOR
        public VMCalendario(INavigation navigation)
        {
            Initialize();
            Navigation = navigation;
        }
        #endregion

        #region OBJETOS

        public DateTime? Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged(nameof(Date));
            }
        }
        public ObservableCollection<SpecialDate> Attendances
        {
            get { return attendances; }
            set { attendances = value; NotifyPropertyChanged(nameof(Attendances)); }
        }

        private void Initialize()
        {
            var dates = new List<SpecialDate>();

            var grayColor = Color.FromHex("#CCE5E5E5");

            Attendances = new ObservableCollection<SpecialDate>(dates) {
                new SpecialDate(DateTime.Now.AddDays(3))
                {
                    Selectable = true,
                    BackgroundPattern = new BackgroundPattern(1)
                    {
                        Pattern = new List<Pattern>
                        {
                            new Pattern{ WidthPercent = 1f, HightPercent = 0.7f, Color = Color.White},
                            new Pattern{ WidthPercent = 1f, HightPercent = 0.3f, Color = Color.Yellow,Text = "Vacation", TextColor=Color.DarkBlue, TextSize=8, TextAlign=TextAlign.Middle},
                        }
                    }
                },
                new SpecialDate(DateTime.Now.AddDays(5))
                {
                    Selectable = true,
                    BackgroundPattern = new BackgroundPattern(1)
                    {
                        Pattern = new List<Pattern>
                        {
                            new Pattern{ WidthPercent = 1f, HightPercent = 0.7f, Color = Color.White},
                            new Pattern{ WidthPercent = 1f, HightPercent = 0.3f, Color = Color.LightGreen, Text = "Absence", TextColor=Color.DarkBlue, TextSize=8, TextAlign=TextAlign.Middle},
                        }
                    }
                },
                new SpecialDate(DateTime.Now.AddDays(4))
                {
                    Selectable = true,
                    BackgroundPattern = new BackgroundPattern(1)
                    {
                        Pattern = new List<Pattern>
                        {
                            new Pattern{ WidthPercent = 1f, HightPercent = 0.7f, Color = grayColor},
                            new Pattern{ WidthPercent = 1f, HightPercent = 0.15f, Color = Color.Yellow, Text = "Vacation", TextColor=Color.DarkBlue, TextSize=8, TextAlign=TextAlign.Middle},
                            new Pattern{ WidthPercent = 1f, HightPercent = 0.15f, Color = Color.LightGreen, Text = "Absence", TextColor=Color.DarkBlue, TextSize=8, TextAlign=TextAlign.Middle},
                        }
                    }
                },
                new SpecialDate(DateTime.Now.AddDays(6))
                {
                    Selectable = true,
                    BackgroundPattern = new BackgroundPattern(1)
                    {
                        Pattern = new List<Pattern>
                        {
                            new Pattern{ WidthPercent = 1f, HightPercent = 0.7f, Color = grayColor},
                            new Pattern{ WidthPercent = 1f, HightPercent = 0.3f, Color = Color.LightGreen, Text = "Absence", TextColor=Color.DarkBlue, TextSize=11, TextAlign=TextAlign.Middle},
                        }
                    }
                },
                new SpecialDate(DateTime.Now)
                {
                    Selectable = true,
                    TextColor = Color.FromHex("#BE5165"),
                    FontAttributes = FontAttributes.Bold
                },
                new SpecialDate(DateTime.Now.AddDays(1))
                {
                    Selectable = true,
                    BackgroundPattern = new BackgroundPattern(1)
                    {
                        Pattern = new List<Pattern>
                        {
                            new Pattern{ WidthPercent = 1f, HightPercent = 1f, Color = grayColor},
                        }
                    }
                },
                new SpecialDate(DateTime.Now.AddDays(2))
                {
                    Selectable = true,
                    BackgroundPattern = new BackgroundPattern(1)
                    {
                        Pattern = new List<Pattern>
                        {
                            new Pattern{ WidthPercent = 1f, HightPercent = 1f, Color = grayColor},
                        }
                    }
                },
                new SpecialDate(DateTime.Now.AddDays(8))
                {
                    Selectable = true,
                    BackgroundPattern = new BackgroundPattern(1)
                    {
                        Pattern = new List<Pattern>
                        {
                            new Pattern{ WidthPercent = 1f, HightPercent = 1f, Color = grayColor},
                        }
                    }
                },
                new SpecialDate(DateTime.Now.AddDays(9))
                {
                    Selectable = true,
                    BackgroundPattern = new BackgroundPattern(1)
                    {
                        Pattern = new List<Pattern>
                        {
                            new Pattern{ WidthPercent = 1f, HightPercent = 1f, Color = grayColor},
                        }
                    }
                },

            };
        }

        #endregion

        #region PROCESOS
        public async Task RegresarAMenu()
        {
            await Navigation.PopAsync();
        }
        //public void ProcesoSimple()
        //{

        //}
        #endregion

        #region COMANDOS
        public Command DateChosenCommand => new Command((obj) =>
        {
            System.Diagnostics.Debug.WriteLine(obj as DateTime?);
        });

        public ICommand VolverMenuCommand => new Command(async () => await RegresarAMenu());


        //public ICommand ProcesoSimpleCommand => new Command(ProcesoSimple);
        #endregion
    }
}
