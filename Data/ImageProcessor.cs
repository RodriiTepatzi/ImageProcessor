using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ProcesadorImagenes.Data
{
    class ImageProcessor
    {

        [Obsolete("This method is deprecated since it has limits on width and height. Use Bitmap methods instead")]
        public static int[,] GetMatrixLegacy(string filePath)
        {
            Bitmap imagen = new Bitmap(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), filePath));

            int height = imagen.Height, width = imagen.Width, promedio;
            int[,] mat = new int[height, width];
            double funcion;

            for (int i = 0; i < imagen.Width; i++)
            {
                for (int j = 0; j < imagen.Height; j++)
                {
                    funcion = ((.2125 * imagen.GetPixel(i, j).R) + (.7154 * imagen.GetPixel(i, j).G) + (.0721 * imagen.GetPixel(i, j).B));
                    promedio = Convert.ToInt32(funcion);

                    mat[j, i] = promedio;
                }
            }
            SaveMatrixToTxt(mat, filePath);
            return mat;
        }

        public static Bitmap ConvertNegative(Bitmap bitmap)
        {
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height);
            Color firstColor, secondColor;
            int r, g, b;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    firstColor = bitmap.GetPixel(i, j);
                    r = 255 - firstColor.R;
                    g = 255 - firstColor.G;
                    b = 255 - firstColor.B;

                    secondColor = Color.FromArgb(r, g, b);
                    result.SetPixel(i, j, secondColor);
                }

            }
            return result;
        }

        public static Bitmap ConvertGray(Bitmap bitmap)
        {
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height);
            Color firstColor, secondColor;
            int hue;
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    firstColor = bitmap.GetPixel(i, j);
                    hue = Convert.ToInt16(0.299 * firstColor.R) + Convert.ToInt16(0.587 * firstColor.G) + Convert.ToInt16(0.114 * firstColor.B);
                    secondColor = Color.FromArgb(hue, hue, hue);
                    result.SetPixel(i, j, secondColor);
                }
            }
            return result;
        }

        // Legacy Functions
        [Obsolete("This method is deprecated since it has limits on width and height. Use not legacy methods instead")]
        public static void ConvertToBinaryLegacy(int[,] matrix, string inputImage, string outputImage)
        {
            Bitmap bitmap = new Bitmap(Image.FromFile(inputImage));
            int[,] m_bn = new int[matrix.GetLength(0), matrix.GetLength(1)];
            m_bn = (int[,])matrix.Clone();

            for (int i = 0; i < m_bn.GetLength(0); i++)
            {
                for (int j = 0; j < m_bn.GetLength(1); j++)
                {
                    if (m_bn[i, j] < 30)
                    {
                        m_bn[i, j] = 0;
                        bitmap.SetPixel(j, i, Color.White);
                    }
                    else
                    {
                        m_bn[i, j] = 1;
                        bitmap.SetPixel(j, i, Color.Black);
                    }
                }
            }
            SaveMatrixToTxt(m_bn, outputImage);
            bitmap.Save(outputImage);
        }

        [Obsolete("This method is deprecated since it has limits on width and height. Use not legacy methods instead")]
        public static string ConvertToGrayLegacy(string inputPath, string outputPath)
        {
            string outputhPathT = outputPath;
            string filename = Path.GetFileNameWithoutExtension(outputPath);

            if (!string.IsNullOrEmpty(filename))
            {
                if(filename.Contains('(') && filename.Contains(')'))
                {
                    int index = int.Parse(filename[filename.Length - 2].ToString());
                    outputhPathT = outputPath.Replace("(" + index + ")", "(" + (index + 1).ToString() + ")");
                    
                }
                else
                {
                    outputhPathT = outputPath.Insert(outputPath.Length - 4, "(2)");
                }
            }
            

            using (Bitmap bitmap = new Bitmap(inputPath))
            {
                int height = bitmap.Height, width = bitmap.Width, prom;
                int[,] m_g = new int[height, width];
                double function;

                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        function = ((.2125 * bitmap.GetPixel(i, j).R) + (.7154 * bitmap.GetPixel(i, j).G) + (.0721 * bitmap.GetPixel(i, j).B));
                        prom = Convert.ToInt32(function);
                        bitmap.SetPixel(i, j, Color.FromArgb(bitmap.GetPixel(i, j).A, prom, prom, prom));
                        m_g[j, i] = prom;
                    }
                }

                bitmap.Save(outputhPathT);
                bitmap.Dispose();
                SaveMatrixToTxt(m_g, outputhPathT);
            }

            return outputhPathT;
        }

        [Obsolete("This method is deprecated since it has limits on width and height. Use not legacy methods instead")]
        public static string ConvertToNegativeLegacy(string inputPath, string outputPath)
        {
            string outputhPathT = outputPath;
            string filename = Path.GetFileNameWithoutExtension(outputPath);

            if (!string.IsNullOrEmpty(filename))
            {
                if (filename.Contains('(') && filename.Contains(')'))
                {
                    int index = int.Parse(filename[filename.Length - 2].ToString());
                    outputhPathT = outputPath.Replace("(" + index + ")", "(" + (index + 1).ToString() + ")");

                }
                else
                {
                    outputhPathT = outputPath.Insert(outputPath.Length - 4, "(2)");
                }
            }

            using (Bitmap bitmap = new Bitmap(inputPath))
            {
                int height = bitmap.Height, width = bitmap.Width, promedio;
                int[,] matrix = new int[height, width];
                double funcion;

                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        funcion = ((.2125 * bitmap.GetPixel(i, j).R) + (.7154 * bitmap.GetPixel(i, j).G) + (.0721 * bitmap.GetPixel(i, j).B));
                        promedio = Convert.ToInt32(funcion);
                        bitmap.SetPixel(i, j, Color.FromArgb(bitmap.GetPixel(i, j).A, 255 - promedio, 255 - promedio, 255 - promedio));
                        matrix[j, i] = 255 - promedio;
                    }
                }

                bitmap.Save(outputhPathT);
                SaveMatrixToTxt(matrix, outputhPathT);
            }

            return outputhPathT;
        }

        private static void SaveMatrixToTxt(int[,] impr, string outputPath)
        {
            if (outputPath.Contains(".png")) outputPath = outputPath.Replace(".png", ".txt");
            if (outputPath.Contains(".jpg")) outputPath = outputPath.Replace(".jpg", ".txt");

            using (var sw = new StreamWriter(outputPath))
            {
                for (int i = 0; i < impr.GetLongLength(0); i++)
                {
                    for (int j = 0; j < impr.GetLongLength(1); j++)
                    {
                        sw.Write(impr[i, j] + "\t");
                    }
                    sw.WriteLine("\n");
                }

                sw.Close();
            }
        }
    }
}
