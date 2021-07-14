using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

namespace SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Empleado" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Empleado.svc or Empleado.svc.cs at the Solution Explorer and start debugging.
    public class Empleado : IEmpleado
    {
        public SL_WCF1.Result AddEF(ML.Departamento departamento)
        {
            ML.Result result = BL.Departamento.AddEF(departamento);
            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, Ex = result.Ex };
        }
        public SL_WCF1.Result DeleteEF(ML.Departamento departamento)
        {
            ML.Result result = BL.Departamento.DeleteEF(departamento);
            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, Ex = result.Ex };
        }
        public SL_WCF1.Result UpdateEF(ML.Departamento departamento)
        {
            ML.Result result = BL.Departamento.UpdateEF(departamento);
            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, Ex = result.Ex };
        }
    }
}
