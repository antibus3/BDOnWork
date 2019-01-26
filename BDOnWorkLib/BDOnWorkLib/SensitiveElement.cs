using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDOnWorkLib
{
    public class SensitiveElement
    {
        //  Основные характеристики Элемента Чуствительного
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
                
             
        public SensitiveElement (
            string numberVK,
            string numberSIOM,
            float signalLeftSIOM,
            float signalRigthSIOM,
            float sPILeftSIOM,
            float sPIRigthSIOM,
            float lengthLeftSIOM,
            float lengthRigthSIOM,
            float signalVK,
            float sPIVK,
            float lengthLeftVK,
            float lengthRigthVK,
            float constantSignal,
            string numberTemperatureSensor,
            bool isExperement
                                )
        {
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
