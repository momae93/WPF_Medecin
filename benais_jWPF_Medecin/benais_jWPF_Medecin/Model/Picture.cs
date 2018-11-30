using System;

namespace benais_jWPF_Medecin.Model
{
    public class Picture
    {
        #region Variables

        private string id;
        private byte[] _content;

        #endregion

        #region Getters/Setters

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public byte[] Content
        {
            get { return _content; }
            set { _content = value; }
        }

        #endregion

        #region Constructor

        public Picture(byte[] content)
        {
            Id = Guid.NewGuid().ToString();
            Content = content;
        }

        #endregion
    }
}
