using benais_jWPF_Medecin.Common.Exceptions;
using benais_jWPF_Medecin.DataAccess;
using benais_jWPF_Medecin.ServiceObservationReference;
using System;

namespace benais_jWPF_Medecin.BusinessManagement
{
    public class ObservationBM
    {
        public ObservationBM()
        {

        }

        public bool AddObservation(int idPatient, Observation observation)
        {
            try
            {
                return new ObservationDA().AddObservation(idPatient, observation);
            }
            catch (Exception e)
            {
                if (e is System.ServiceModel.EndpointNotFoundException)
                    throw new CustomServerException();
                else
                    return false;
            }
        }
    }
}
