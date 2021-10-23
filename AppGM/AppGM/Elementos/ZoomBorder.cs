using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AppGM
{
    /// <summary>
    /// Elemento borde que facilita las funciones de zoom y desplazamiento en los elementos que este contenga.
    /// </summary>
    public class ZoomBorder : Border
    {
        #region Miembros

        // Campos ---


        private UIElement _elemento = null;
        
        private Point _origen;
        private Point _puntoInicial;


        // Propiedades ---


        public override UIElement Child
        {
            get => base.Child;
            set
            {
                if (value != null && value != this.Child)
                    this.Initialize(value);
                
                base.Child = value;
            }
        }

        #endregion

        #region Funciones

        private void Initialize(UIElement element)
        {
            this._elemento = element;

            if (element != null)
            {
                TransformGroup grupo = new TransformGroup();
                ScaleTransform scaleTransform = new ScaleTransform();

                grupo.Children.Add(scaleTransform);
                
                TranslateTransform tt = new TranslateTransform();
                
                grupo.Children.Add(tt);
                
                element.RenderTransform       = grupo;
                element.RenderTransformOrigin = new Point(0.0, 0.0);
                
                this.PreviewMouseWheel                  += OnPreviewMouseWheel;
                this.PreviewMouseLeftButtonDown         += OnPreviewMouseLeftButtonDown;
                this.PreviewMouseLeftButtonUp           += OnPreviewMouseLeftButtonUp;
                this.PreviewMouseMove                   += OnPreviewMouseMove;
                this.PreviewMouseRightButtonDown += new MouseButtonEventHandler(OnPreviewMouseRightButtonDown);
            }
        }

        private void Reset()
        {
            if (_elemento != null)
            {
                // Reseteamos el zoom.
                var scaleTransform = ObtenerScaleTransform(_elemento);
                scaleTransform.ScaleX = 1.0;
                scaleTransform.ScaleY = 1.0;

                // Reseteamos el scroll horizontal.
                var translateTransform = ObtenerTranslateTransform(_elemento);
                translateTransform.X = 0.0;
                translateTransform.Y = 0.0;
            }
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e != null)
            {
                var scaleTransform = ObtenerScaleTransform(_elemento);
                var translateTransform = ObtenerTranslateTransform(_elemento);

                double zoom = e.Delta > 0 ? .2 : -.2;
                
                if (!(e.Delta > 0) && (scaleTransform.ScaleX < .4 || scaleTransform.ScaleY < .4))
                    return;

                Point relative = e.GetPosition(_elemento);
                
                double abosuluteX;
                double abosuluteY;

                abosuluteX = relative.X * scaleTransform.ScaleX + translateTransform.X;
                abosuluteY = relative.Y * scaleTransform.ScaleY + translateTransform.Y;

                scaleTransform.ScaleX += zoom;
                scaleTransform.ScaleY += zoom;

                translateTransform.X = abosuluteX - relative.X * scaleTransform.ScaleX;
                translateTransform.Y = abosuluteY - relative.Y * scaleTransform.ScaleY;
            }
        }

        private void OnPreviewMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if (_elemento != null && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                var translateTransform = ObtenerTranslateTransform(_elemento);
                
                _puntoInicial = ((MouseButtonEventArgs)e).GetPosition(this);
                _origen = new Point(translateTransform.X, translateTransform.Y);
                
                this.Cursor = Cursors.Hand;
                
                _elemento.CaptureMouse();
            }
        }

        private void OnPreviewMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            if (_elemento != null)
            {
                _elemento.ReleaseMouseCapture();
                
                this.Cursor = Cursors.Arrow;
            }
        }

        private void OnPreviewMouseRightButtonDown(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
                this.Reset();
        }

        private void OnPreviewMouseMove(object sender, RoutedEventArgs e)
        {
            if (_elemento != null && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                if (_elemento.IsMouseCaptured)
                {
                    var translateTransform = ObtenerTranslateTransform(_elemento);
                    
                    Vector vector = _puntoInicial - ((MouseEventArgs)e).GetPosition(this);
                    
                    translateTransform.X = _origen.X - vector.X;
                    translateTransform.Y = _origen.Y - vector.Y;
                }
            }
        }

        private TranslateTransform ObtenerTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform).Children.First(t => t is TranslateTransform);
        }

        private ScaleTransform ObtenerScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform).Children.First(t => t is ScaleTransform);
        }

        #endregion
    }
}
