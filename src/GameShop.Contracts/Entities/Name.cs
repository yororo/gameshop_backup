using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Name
    {
        #region Declarations

        private Salutation _salutation;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _suffix;

        #endregion Declarations

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
                return $"{ FirstName } { LastName }";
            }
        }

        public string CompleteName
        {
            get
            {
                string salutation = Salutation != Salutation.Unspecified ? Salutation.ToString() : string.Empty;

                return $"{ salutation } { FirstName } { MiddleName } { LastName } { Suffix }".Trim();
            }
        }

        #endregion Properties

        #region Constructors

        public Name(Salutation salutation, string firstName, string middleName, string lastName, string suffix)
        {
            _salutation = salutation;
            _firstName = firstName;
            _middleName = middleName;
            _lastName = lastName;
            _suffix = suffix;
        }

        public Name(string firstName, string middleName, string lastName)
            : this(Salutation.Unspecified, firstName, middleName, lastName, string.Empty)
        {

        }

        public Name(string firstName, string lastName)
            : this(Salutation.Unspecified, firstName, string.Empty, lastName, string.Empty)
        {

        }

        public Name()
            : this(Salutation.Unspecified, 
                      string.Empty, 
                      string.Empty, 
                      string.Empty, 
                      string.Empty)
        {

        }

        #endregion Constructors
    }
}
