using benais_jWPF_Medecin.Common.Exceptions;
using benais_jWPF_Medecin.DataAccess;
using benais_jWPF_Medecin.ServicePatientReference;
using System.Diagnostics;
using System.Threading;

namespace benais_jWPF_Medecin.BusinessManagement
{
    public class PatientBM
    {

        #region Constructor

        public PatientBM()
        {

        }

        #endregion


        #region Methods

        /// <summary>
        /// Call data access layer GetListPatient function
        /// </summary>
        /// <returns></returns>
        public Patient[] GetListPatient()
        {
            try
            {
                return new PatientDA().GetListPatient();
            }
            catch (System.Exception e)
            {
                string header = "[DEBUG][PatientBM.GetListPatient] ";
                Debug.WriteLine(header + e.Message);

                throw new CustomServerException();
            }
        }

        /// <summary>
        /// Call data access layer GetPatient function
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Patient GetPatient(int id)
        {
            try
            {
                return new PatientDA().GetPatient(id);
            }
            catch (System.Exception e)
            {
                string header = "[DEBUG][PatientBM.GetPatient] ";
                Debug.WriteLine(header + e.Message);

                throw new CustomServerException();
            }
        }

        /// <summary>
        /// Call data access layer AddPatient function
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Call data access layer DeletePatient function
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        #endregion
    }
}
