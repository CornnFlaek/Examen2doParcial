﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorDeSeñales
{
    abstract class Señal
    {
        public List<Muestra> Muestras { get; set; }
        public double TiempoInicial { get; set; }
        public double TiempoFinal { get; set; }
        public double FrecuenciaDeMuestreo { get; set; }

        public double AmplitudMaxima { get; set; }

        public abstract double evaluar(double tiempo);

        public void construirSeñal()
        {
            double periodoMuestreo = 1 / FrecuenciaDeMuestreo;

            Muestras.Clear();

            for(double i = TiempoInicial; i <= TiempoFinal; i+= periodoMuestreo)
            {
                double muestra = evaluar(i);

                Muestras.Add(new Muestra(i, muestra));

                if (Math.Abs(muestra) > AmplitudMaxima)
                {
                    AmplitudMaxima = Math.Abs(muestra);
                }
            }
        }

        public static Señal multiplicarSeñales( Señal señal1, Señal señal2)
        {
            SeñalResultante resultado = new SeñalResultante();
            resultado.TiempoInicial = señal1.TiempoInicial;
            resultado.TiempoFinal = señal1.TiempoFinal;
            resultado.FrecuenciaDeMuestreo = señal1.FrecuenciaDeMuestreo;

            int indice = 0;
            foreach (var muestra in señal1.Muestras)
            {
                double nuevoValor = muestra.Y * señal2.Muestras[indice].Y;

                resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor));

                if (Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }

                indice++;
            }

            return resultado;
        }

        public static Señal escalaExponencial( Señal señalOriginal, double exponente)
        {
            SeñalResultante resultado = new SeñalResultante();

            resultado.TiempoInicial = señalOriginal.TiempoInicial;
            resultado.TiempoFinal = señalOriginal.TiempoFinal;
            resultado.FrecuenciaDeMuestreo = señalOriginal.FrecuenciaDeMuestreo;

            foreach (var muestra in señalOriginal.Muestras){
                double nuevoValor = Math.Pow(muestra.Y, exponente);

                resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor));

                if (Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
            }

            return resultado;
        }

        public static Señal escalarAmplitud( Señal señalOriginal, double factorEscala)
        {
            SeñalResultante resultado = new SeñalResultante();

            resultado.TiempoInicial = señalOriginal.TiempoInicial;
            resultado.TiempoFinal = señalOriginal.TiempoFinal;
            resultado.FrecuenciaDeMuestreo = señalOriginal.FrecuenciaDeMuestreo;

            foreach(var muestra in señalOriginal.Muestras)
            {
                double nuevoValor = muestra.Y * factorEscala;
                resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor));

                if (Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
            }

            return resultado;
        }

        public static Señal desplazarAmplitud ( Señal señalOriginal, double cantidadDesplazamiento)
        {
            SeñalResultante resultado = new SeñalResultante();

            foreach (var muestra in señalOriginal.Muestras)
            {
                double nuevoValor = muestra.Y + cantidadDesplazamiento;
                resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor));

                if (Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
            }

            return resultado;
        }

        public static Señal limiteInferior(
            Señal señal1, Señal señal2)
        {
            SeñalResultante resultado =
                new SeñalResultante();
            resultado.TiempoInicial =
                señal1.TiempoInicial;
            resultado.TiempoFinal =
                señal1.TiempoFinal;
            resultado.FrecuenciaDeMuestreo =
                señal1.FrecuenciaDeMuestreo;

            int indice = 0;
            foreach (var muestra in señal1.Muestras)
            {
                double valor1 = muestra.Y;
                double valor2 = señal2.Muestras[indice].Y;
                double nuevoValor =
                    (muestra.Y +
                    señal2.Muestras[indice].Y) / 2.0;
                if (valor1 < valor2)
                {
                    nuevoValor = valor1;
                }
                else
                {
                    nuevoValor = valor2;
                }


                resultado.Muestras.Add(
                    new Muestra(muestra.X, nuevoValor));

                if (Math.Abs(nuevoValor) >
                    resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima =
                        Math.Abs(nuevoValor);
                }

                indice++;
            }
            return resultado;
        }
    }
}
