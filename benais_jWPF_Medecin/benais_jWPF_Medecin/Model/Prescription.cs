using System;

namespace benais_jWPF_Medecin.Model
{
    public class Prescription
    {
        #region Variables

        private string id;
        private string _content;

        #endregion

        #region Getters/Setters

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        #endregion

        #region Constructor

        public Prescription(string content)
        {
            Id = Guid.NewGuid().ToString();
            Content = content;
        }

        #endregion
    }
}
