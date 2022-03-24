using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProcesadorImagenes.Model
{
    public class ImageFile
    {
        public string ImagePath { get; set; }

        public ImageFile(string ImagePath)
        {
            this.ImagePath = ImagePath;
        }
    }
}
