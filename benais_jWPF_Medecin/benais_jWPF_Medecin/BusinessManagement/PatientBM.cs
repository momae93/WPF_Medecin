using benais_jWPF_Medecin.DataAccess;
using benais_jWPF_Medecin.ServicePatientReference;
using System.Threading;

namespace benais_jWPF_Medecin.BusinessManagement
{
    public class PatientBM
    {
        public Patient[] GetListPatient()
        {
            return new PatientDA().GetListPatient();
        }

        public Patient GetPatient(int id)
        {
            Thread.Sleep(4000);
            return new PatientDA().GetPatient(id);
        }

        public bool AddPatient(Patient patient)
        {
            return new PatientDA().AddPatient(patient);
        }

        public bool DeletePatient(int id)
        {
            return new PatientDA().DeletePatient(id);
        }
    }
}
