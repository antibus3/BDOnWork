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

        public override bool Equals(object obj)
        {
            if (!(obj is SensitiveElement)) return false; //  Если обект не является блоком, тогда он не может быть эквивалентным 
            SensitiveElement itemToCompare = obj as SensitiveElement;
            if (
                this.Id == itemToCompare.Id &&
                this.NumberVK == itemToCompare.NumberVK &&
                this.NumberSIOM == itemToCompare.NumberSIOM &&
                this.SignalLeftSIOM == itemToCompare.SignalLeftSIOM &&
                this.SignalRigthSIOM == itemToCompare.SignalRigthSIOM &&
                this.SPILeftSIOM == itemToCompare.SPILeftSIOM &&
                this.SPIRigthSIOM == itemToCompare.SPIRigthSIOM &&
                this.LengthLeftSIOM == itemToCompare.LengthLeftSIOM &&
                this.LengthRigthSIOM == itemToCompare.LengthRigthSIOM &&
                this.SignalVK == itemToCompare.SignalVK &&
                this.SPIVK == itemToCompare.SPIVK &&
                this.LengthLeftVK == itemToCompare.LengthLeftVK &&
                this.LengthRigthVK == itemToCompare.LengthRigthVK &&
                this.ConstantSignal == itemToCompare.ConstantSignal &&
                this.NumberTemperatureSensor == itemToCompare.NumberTemperatureSensor &&
                this.IsExperement == itemToCompare.IsExperement
              ) return true;
                        else return false;
        }




    }
}
