using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDOnWorkLib
{
    public class SensitiveElement
    {
        //  Основные характеристики Элемента Чуствительного
        public readonly int Id;
        public readonly string NumberVK;
        public readonly string NumberSIOM;
        public readonly double SignalLeftSIOM;
        public readonly double SignalRigthSIOM;
        public readonly double SPILeftSIOM;
        public readonly double SPIRigthSIOM;
        public readonly double LengthLeftSIOM;
        public readonly double LengthRigthSIOM;
        public readonly double SignalVK;
        public readonly double SPIVK;
        public readonly double LengthLeftVK;
        public readonly double LengthRigthVK;
        public readonly double ConstantSignal;

        //  Побочные характеристики
        public readonly string NumberTemperatureSensor;
        public readonly bool IsExperement;

        public SensitiveElement() { }

        public SensitiveElement(
            int id = 0,
            string numberVK = null,
            string numberSIOM = null,
            double signalLeftSIOM = 0.0,
            double signalRigthSIOM = 0.0,
            double sPILeftSIOM = 0.0,
            double sPIRigthSIOM = 0.0,
            double lengthLeftSIOM = 0.0,
            double lengthRigthSIOM = 0.0,
            double signalVK = 0.0,
            double sPIVK = 0.0,
            double lengthLeftVK = 0.0,
            double lengthRigthVK = 0.0,
            double constantSignal = 0.0,
            string numberTemperatureSensor = null,
            bool isExperement = false
                                )
        {
            Id = id;
            NumberVK = numberVK;
            NumberSIOM = numberSIOM;
            SignalLeftSIOM = signalLeftSIOM;
            SignalRigthSIOM = signalRigthSIOM;
            SPILeftSIOM = sPILeftSIOM;
            SPIRigthSIOM = sPIRigthSIOM;
            LengthLeftSIOM = lengthLeftSIOM;
            LengthRigthSIOM = lengthRigthSIOM;
            SignalVK = signalVK;
            SPIVK = sPIVK;
            LengthLeftVK = lengthLeftVK;
            LengthRigthVK = lengthRigthVK;
            ConstantSignal = constantSignal;
            NumberTemperatureSensor = numberTemperatureSensor;
            IsExperement = isExperement;
        }
        
        public static bool operator ==(SensitiveElement s1, SensitiveElement s2)
        {
            if (
                s1.Id == s2.Id &&
                s1.NumberVK == s2.NumberVK &&
                s1.NumberSIOM == s2.NumberSIOM &&
                s1.SignalLeftSIOM == s2.SignalLeftSIOM &&
                s1.SignalRigthSIOM == s2.SignalRigthSIOM &&
                s1.SPILeftSIOM == s2.SPILeftSIOM &&
                s1.SPIRigthSIOM == s2.SPIRigthSIOM &&
                s1.LengthLeftSIOM == s2.LengthLeftSIOM &&
                s1.LengthRigthSIOM == s2.LengthRigthSIOM &&
                s1.SignalVK == s2.SignalVK &&
                s1.SPIVK == s2.SPIVK &&
                s1.LengthLeftVK == s2.LengthLeftVK &&
                s1.LengthRigthVK == s2.LengthRigthVK &&
                s1.ConstantSignal == s2.ConstantSignal &&
                s1.NumberTemperatureSensor == s2.NumberTemperatureSensor &&
                s1.IsExperement == s2.IsExperement
              ) return true;
                      else return false; 
        }
        public static bool operator !=(SensitiveElement s1, SensitiveElement s2)
        {
            if (
                s1.Id != s2.Id ||
                s1.NumberVK != s2.NumberVK ||
                s1.NumberSIOM != s2.NumberSIOM ||
                s1.SignalLeftSIOM != s2.SignalLeftSIOM ||
                s1.SignalRigthSIOM != s2.SignalRigthSIOM ||
                s1.SPILeftSIOM != s2.SPILeftSIOM ||
                s1.SPIRigthSIOM != s2.SPIRigthSIOM ||
                s1.LengthLeftSIOM != s2.LengthLeftSIOM ||
                s1.LengthRigthSIOM != s2.LengthRigthSIOM ||
                s1.SignalVK != s2.SignalVK ||
                s1.SPIVK != s2.SPIVK ||
                s1.LengthLeftVK != s2.LengthLeftVK ||
                s1.LengthRigthVK != s2.LengthRigthVK ||
                s1.ConstantSignal != s2.ConstantSignal ||
                s1.NumberTemperatureSensor != s2.NumberTemperatureSensor ||
                s1.IsExperement != s2.IsExperement
              ) return false;
                        else return true;
        }
        



    }
}
