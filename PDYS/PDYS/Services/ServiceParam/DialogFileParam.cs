using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDYS.Services.ServiceParam
{
    public class DialogFileParam
    {
        public const string ImageFileFilter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
        public const string TextFileFilter = "Text files (*.txt;*.csv)|*.txt;*.csv";

        public string FileFilter { get; set; }
        public Action<byte[]> FileLoadedAction { get; set; }
    }
}
