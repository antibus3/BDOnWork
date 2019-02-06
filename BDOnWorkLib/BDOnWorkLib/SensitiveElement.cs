using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDOnWorkLib
{
    public class SensitiveElement
    {

        public readonly Dictionary<String, object> Filds;

        public SensitiveElement() { }

        public SensitiveElement(Dictionary<String, object> filds)
        {
            Filds = filds;
        }

        public override bool Equals(object obj)
        {
            var ElementOfObj = obj as SensitiveElement;
            if (ElementOfObj == null) return false;  //  Если обьект не элеемент, тогда они не эквивалентны
            if (this.Filds.Keys.Count != ElementOfObj.Filds.Keys.Count) return false; //  Если количество ключей разное, то они не эквивалентны
            foreach (string s in this.Filds.Keys)
            {
                if (this.Filds[s].ToString() != ElementOfObj.Filds[s].ToString()) return false;
            }
            return true;
        }




    }
}
