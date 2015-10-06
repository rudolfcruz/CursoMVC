using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace AutoLote.Helpers
{
    public enum Tamanios
    {
        Miniatura,
        Mediana
    }
    public class clsGuardarImagen
    {
        public string RedimensionarAndGuardar(string nombreArchivo, Stream imageBuffer, Tamanios tamanios, bool cuadro, string opcion)
        {
            try
            {
                int oldAncho, oldAlto, newAncho, newAlto, maxLadoTamanio;
                string saveRuta, serverRuta, imagesRuta, urlImagen = string.Empty;
                Image image = Image.FromStream(imageBuffer);
                Bitmap newImage;
                oldAncho = image.Width;
                oldAlto = image.Height;

                serverRuta = HttpContext.Current.Server.MapPath("~");
                imagesRuta = serverRuta + "Content\\Imagenes\\";
                if (tamanios == Tamanios.Miniatura)
                {
                    urlImagen += "~/content/Imagenes/Miniatura/" + Path.GetFileName(nombreArchivo) + ".jpg";
                    maxLadoTamanio = 80;
                    saveRuta = imagesRuta + "Miniatura\\" + Path.GetFileName(nombreArchivo) + ".jpg";
                }
                else
                {
                    urlImagen += "~/content/Imagenes/Mediana/" + Path.GetFileName(nombreArchivo) + ".jpg";
                    maxLadoTamanio = 600;
                    saveRuta = imagesRuta + "Mediana\\" + Path.GetFileName(nombreArchivo) + ".jpg";
                }
                if (cuadro)
                {
                    int ladoMenor = oldAncho >= oldAlto ? oldAlto : oldAncho;
                    double coeficiente = maxLadoTamanio / (double)ladoMenor;
                    newAncho = Convert.ToInt32(coeficiente * oldAncho);
                    newAlto = Convert.ToInt32(coeficiente * oldAlto);
                    Bitmap tempimage = new Bitmap(image, newAncho, newAlto);
                    int ejeX = (newAncho - maxLadoTamanio) / 2;
                    int ejeY = (newAlto - maxLadoTamanio) / 2;
                    newImage = new Bitmap(maxLadoTamanio, maxLadoTamanio);
                    Graphics tempGrafico = Graphics.FromImage(newImage);
                    tempGrafico.SmoothingMode = SmoothingMode.AntiAlias;
                    tempGrafico.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    tempGrafico.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    tempGrafico.DrawImage(tempimage, new Rectangle(0, 0, maxLadoTamanio, maxLadoTamanio), ejeX, ejeY, maxLadoTamanio, maxLadoTamanio, GraphicsUnit.Pixel);
                }
                else
                {
                    int maxLado = oldAncho >= oldAlto ? oldAncho : oldAlto;
                    if (maxLado > maxLadoTamanio)
                    {
                        double coeficiente = maxLadoTamanio / (double)maxLado;
                        newAncho = Convert.ToInt32(coeficiente * oldAncho);
                        newAlto = Convert.ToInt32(coeficiente * oldAlto);
                    }
                    else
                    {
                        newAncho = oldAncho;
                        newAlto = oldAlto;
                    }
                    newImage = new Bitmap(image, newAncho, newAlto);
                }
                switch (opcion)
                {
                    case "C":
                        if (File.Exists(saveRuta))
                        {
                            return "false";
                        }
                        else
                        {
                            newImage.Save(saveRuta, ImageFormat.Jpeg);
                        }
                        break;
                    case "U":
                        if (File.Exists(saveRuta))
                            File.Delete(saveRuta);
                        File.Move(nombreArchivo,saveRuta);
                        break;
                }                
                image.Dispose();
                newImage.Dispose();
                return urlImagen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}