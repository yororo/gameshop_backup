using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Name
    {
        #region Fields

        private Salutation _salutation;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _suffix;

        #endregion

        #region Properties

        public Salutation Salutation
        {
            get
            {
                return _salutation;
            }

            set
            {
                _salutation = value;
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = value;
            }
        }

        public string MiddleName
        {
            get
            {
                return _middleName;
            }

            set
            {
                _middleName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
            }
        }

        public string Suffix
        {
            get
            {
                return _suffix;
            }

            set
            {
                _suffix = value;
            }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public string CompleteName
        {
            get
            {
                return string.Format("{0} {1} {2} {3}, {4}", Salutation, FirstName, MiddleName, LastName, Suffix);
            }
        }

        #endregion

        #region Constructors

        public Name()
        {
            Salutation = Salutation.NotSpecified;
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
            Suffix = string.Empty;
        }

        #endregion
    }
}
