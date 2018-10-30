using benais_jWPF_Medecin.ServicePatientReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benais_jWPF_Medecin.DataAccess
{
    public class PatientDA
    {
        private ServicePatientClient _service;

        public PatientDA()
        {
            _service = new ServicePatientClient();
        }

        public Patient[] GetListPatient()
        {
            return _service.GetListPatient();
        }

        public Patient GetPatient(int id)
        {
            return _service.GetPatient(id);
        }

        public bool AddPatient(Patient patient)
        {
            return _service.AddPatient(patient);
        }

        public bool DeletePatient(int id)
        {
            return _service.DeletePatient(id);
        }
    }
}
