using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BDOnWorkLib
{
    public class ParseParfjonov : Panel
    {

        private readonly Bitmap _graphicDrawing;

        public ParseParfjonov () { }

        public ParseParfjonov (string patchOfFile)
        {
            _graphicDrawing = new Bitmap (patchOfFile)
        }

    }
}
