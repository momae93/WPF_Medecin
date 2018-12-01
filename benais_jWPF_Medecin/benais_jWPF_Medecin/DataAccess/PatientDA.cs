using benais_jWPF_Medecin.ServicePatientReference;
using System.Threading;

namespace benais_jWPF_Medecin.DataAccess
{
    public class PatientDA
    {
        private ServicePatientClient _service;

        public PatientDA()
        {
            _service = new ServicePatientClient();
        }

        /// <summary>
        /// Call WCF GetListPatient function
        /// </summary>
        /// <returns></returns>
        public Patient[] GetListPatient()
        {
            Thread.Sleep(4000);
            return _service.GetListPatient();
        }

        /// <summary>
        /// Call WCF GetPatient function
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Patient GetPatient(int id)
        {
            return _service.GetPatient(id);
        }

        /// <summary>
        /// Call WCF AddPatient function
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public bool AddPatient(Patient patient)
        {
            return _service.AddPatient(patient);
        }

        /// <summary>
        /// Call WCF DeletePatient function
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeletePatient(int id)
        {
            return _service.DeletePatient(id);
        }
    }
}
