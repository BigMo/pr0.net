using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net.Utils
{
    public class Builder<T> where T : new()
    {
        #region VARIABLES
        private T make;
        #endregion

        #region PROPERTIES
        protected T Make { get { return make; } }
        #endregion

        #region CONSTRUCTORS
        public Builder()
        {
            make = new T();
        }

        public T Build()
        {
            return Make;
        }
        #endregion
    }
}
