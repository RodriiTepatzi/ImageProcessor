using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ProcesadorImagenes.Commands;
using ProcesadorImagenes.Data;
using ProcesadorImagenes.Model;

namespace ProcesadorImagenes.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ImageFile _ImageInput;
        private ImageFile _ImageOutput;
        private ICommand load_ButtonCommand;
        private ICommand convertImage_ButtonCommand;
        private string grayPath = "", negativePath = "";


        [Obsolete]
        public MainWindowViewModel()
        {
            LoadButtonCommand = new RelayCommand(new Action<object>(SetImagePath));
            convertImage_ButtonCommand = new RelayCommand(new Action<object>(ConvertImage));
            _ImageInput = new ImageFile("");
            _ImageOutput = new ImageFile("");
        }

        public string ImageInputPath{
            get
            {
                return @_ImageInput.ImagePath;
            }
            set
            {
                _ImageInput.ImagePath = value;
                OnPropertyChanged("ImageInputPath");
            }
        }

        public string ImageOutputPath
        {
            get
            {
                return @_ImageOutput.ImagePath;
            }
            set
            {
                _ImageOutput.ImagePath = value;
                OnPropertyChanged("ImageOutputPath");
            }
        }

        public ICommand LoadButtonCommand
        {
            get
            {
                return load_ButtonCommand;
            }
            set
            {
                load_ButtonCommand = value;
            }
        }

        public ICommand ConvertButtonCommand
        {
            get
            {
                return convertImage_ButtonCommand;
            }
            set
            {
                convertImage_ButtonCommand = value;
            }
        }

        private void SetImagePath(object obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Archivos de imagen |*.bmp;*.jpg;*.png";
            if (fileDialog.ShowDialog() == true)
            {
                Image image;
                BitmapImage bitmapImage = new BitmapImage();
                using (FileStream stream = File.OpenRead(fileDialog.FileName))
                {
                    image = Image.FromStream(stream);
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    ImageInputPath = fileDialog.FileName;
                }
            }
        }

        [Obsolete]
        private void ConvertImage(object obj)
        {
            if (obj is string)
            {
                if (!string.IsNullOrEmpty(ImageInputPath))
                {
                    if (obj as string == "Gris")
                    {
                        string outputPath = string.IsNullOrEmpty(grayPath) ? ImageInputPath : grayPath;
                        outputPath = string.IsNullOrEmpty(negativePath) ? outputPath.Insert(ImageInputPath.Length - 4, "-Gris") : grayPath;

                        ImageProcessor.GetMatrixLegacy(ImageInputPath);

                        string tempPath = ImageProcessor.ConvertToGrayLegacy(ImageInputPath, outputPath);
                        ImageOutputPath = tempPath;
                        grayPath = tempPath;
                    }

                    if (obj as string == "Negativo")
                    {
                        string outputPath = string.IsNullOrEmpty(negativePath) ? ImageInputPath : negativePath;
                        outputPath = string.IsNullOrEmpty(negativePath) ? outputPath.Insert(ImageInputPath.Length - 4, "-Negativo") : negativePath;

                        ImageProcessor.GetMatrixLegacy(ImageInputPath);
                        string tempPath = ImageProcessor.ConvertToNegativeLegacy(ImageInputPath, outputPath);

                        negativePath = tempPath;
                        ImageOutputPath = tempPath;
                    }

                    if (obj as string == "Binario")
                    {
                        string outputPath = ImageInputPath;
                        outputPath = outputPath.Insert(ImageInputPath.Length - 4, "-Binario");

                        int[,] matrix = ImageProcessor.GetMatrixLegacy(ImageInputPath);
                        ImageProcessor.ConvertToBinaryLegacy(matrix, ImageInputPath, outputPath);

                        ImageOutputPath = outputPath;
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un archivo", "Error");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
