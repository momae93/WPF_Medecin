using benais_jWPF_Medecin.Common.Exceptions;
using benais_jWPF_Medecin.DataAccess;
using benais_jWPF_Medecin.ServiceObservationReference;
using System;
using System.Diagnostics;

namespace benais_jWPF_Medecin.BusinessManagement
{
    public class ObservationBM
    {
        #region Constructor

        public ObservationBM()
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Call data access layer AddObservation function
        /// </summary>
        /// <param name="idPatient"></param>
        /// <param name="observation"></param>
        /// <returns></returns>
        public bool AddObservation(int idPatient, Observation observation)
        {
            try
            {
                return new ObservationDA().AddObservation(idPatient, observation);
            }
            catch (Exception e)
            {
                string header = "[DEBUG][ObservationBM.AddObservation] ";
                Debug.WriteLine(header + e.Message);

                throw new CustomServerException();
            }
        }

        #endregion
    }
}
