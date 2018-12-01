using benais_jWPF_Medecin.ServiceObservationReference;

namespace benais_jWPF_Medecin.DataAccess
{
    public class ObservationDA
    {
        private IServiceObservation _service;

        public ObservationDA()
        {
            _service = new ServiceObservationClient();
        }

        /// <summary>
        /// Call WCF AddObservation function
        /// </summary>
        /// <param name="idPatient"></param>
        /// <param name="observation"></param>
        /// <returns></returns>
        public bool AddObservation(int idPatient, Observation observation)
        {
            return _service.AddObservation(idPatient, observation);
        }
    }
}
