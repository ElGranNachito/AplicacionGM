﻿using AppGM.Core;
using System.Collections.Generic;
using System.IO;

namespace AppGM
{
    /// <summary>
    /// Implementacion de la interfaz <see cref="IDirectorio"/>
    /// </summary>
    class Directorio_Windows : IDirectorio
    {
        #region Campos & Propiedades

        //------------------------------CAMPOS--------------------------------


        private DirectoryInfo mDirectorio;


        //----------------------------PROPIEDADES------------------------------


        public string Ruta { get; set; }
        public string Nombre { get; set; }

        public IDirectorio DirectorioPadre => new Directorio_Windows(new DirectoryInfo(Ruta));

        #endregion

        #region Constrcutores

        public Directorio_Windows(DirectoryInfo infoDirectorio)
        {
            Nombre = infoDirectorio.Name;
            Ruta = infoDirectorio.FullName;

            mDirectorio = infoDirectorio;
        }

        #endregion

        #region Funciones
        public void Borrar(bool recursivo) => mDirectorio.Delete(recursivo);

        public List<IArchivo> ObtenerArchivos(string patronDeBusqueda)
        {
            FileInfo[] archivos = mDirectorio.GetFiles(patronDeBusqueda);

            List<IArchivo> archivosResultado = new List<IArchivo>(archivos.Length);

            for (int i = 0; i < archivos.Length; ++i)
                archivosResultado.Add(new Archivo_Windows(archivos[i]));

            return archivosResultado;
        }

        public List<IDirectorio> ObtenerDirectorios(string patronDeBusqueda)
        {
            DirectoryInfo[] archivos = mDirectorio.GetDirectories(patronDeBusqueda);

            List<IDirectorio> archivosResultado = new List<IDirectorio>(archivos.Length);

            for (int i = 0; i < archivos.Length; ++i)
                archivosResultado.Add(new Directorio_Windows(archivos[i]));

            return archivosResultado;
        } 
        #endregion
    }
}
