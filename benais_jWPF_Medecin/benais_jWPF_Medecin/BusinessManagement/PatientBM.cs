using benais_jWPF_Medecin.Common.Exceptions;
using benais_jWPF_Medecin.DataAccess;
using benais_jWPF_Medecin.ServicePatientReference;
using System.Threading;

namespace benais_jWPF_Medecin.BusinessManagement
{
    public class PatientBM
    {
        public Patient[] GetListPatient()
        {
            try
            {
                return new PatientDA().GetListPatient();
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public Patient GetPatient(int id)
        {
            try
            {
                return new PatientDA().GetPatient(id);
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public bool AddPatient(Patient patient)
        {
            try
            {
                return new PatientDA().AddPatient(patient);
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

        public bool DeletePatient(int id)
        {
            try
            {
                return new PatientDA().DeletePatient(id);
            }
            catch (System.Exception e)
            {
                return false;
            }
        }
    }
}
