using System.ComponentModel;
using System.Linq;

namespace AppGM.Core
{
    public class ViewModelIngresoPosicion : BaseViewModel
    {
        private string mTextoPosicionX = string.Empty;
        private string mTextoPosicionY = string.Empty;

        public string TextoPosicionX
        {
            get => mTextoPosicionX;
            set
            {
                if (value.Length < mTextoPosicionX.Length)
                {
                    mTextoPosicionX = value;
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PosicionX)));
                }
                else if (value.Last().EsUnNumero())
                {
                    mTextoPosicionX = value;
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PosicionX)));
                }
                else
                    value.Remove(value.Last());
            }
        }
        public string TextoPosicionY
        {
            get => mTextoPosicionY;
            set
            {
                if (value.Length < mTextoPosicionY.Length)
                {
                    mTextoPosicionY = value;
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PosicionY)));
                }
                else if (value.Last().EsUnNumero())
                {
                    mTextoPosicionY = value;
                    DispararPropertyChanged(new PropertyChangedEventArgs(nameof(PosicionY)));
                }
                else
                    value.Remove(value.Last());
            }
        }

        public int PosicionX => int.Parse(mTextoPosicionX);
        public int PosicionY => int.Parse(mTextoPosicionY);

    }
}
