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
        public readonly float SignalLeftSIOM;       
        public readonly float SignalRigthSIOM;
        public readonly float SPILeftSIOM;
        public readonly float SPIRigthSIOM;
        public readonly float LengthLeftSIOM;
        public readonly float LengthRigthSIOM;
        public readonly float SignalVK;
        public readonly float SPIVK;
        public readonly float LengthLeftVK;
        public readonly float LengthRigthVK;
        public readonly float ConstantSignal;

        //  Побочные характеристики
        public readonly string NumberTemperatureSensor;
        public readonly bool IsExperement;
                
        public SensitiveElement () { }     

        public SensitiveElement (
            int id = 0,
            string numberVK = null,
            string numberSIOM = null,
            float signalLeftSIOM = 0.0f,
            float signalRigthSIOM = 0.0f,
            float sPILeftSIOM = 0.0f,
            float sPIRigthSIOM = 0.0f,
            float lengthLeftSIOM = 0.0f,
            float lengthRigthSIOM = 0.0f,
            float signalVK = 0.0f,
            float sPIVK = 0.0f,
            float lengthLeftVK = 0.0f,
            float lengthRigthVK = 0.0f,
            float constantSignal = 0.0f,
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



    }
}
