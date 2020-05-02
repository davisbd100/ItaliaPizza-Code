
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic
{

    /// <summary>Clase con una enumeracion para los posibles resultados de una operacion</summary>
    public class ResultEnum
    {
        public enum ResultOperation
        {
            Success,
            NullObject,
            UnknowFail,
            SQLFail,
            ExistingRecord
        }
    }
}