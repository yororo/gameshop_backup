using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Name
    {
        #region Properties

        public Salutation Salutation { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Suffix { get; set; }

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
                string firstName = !string.IsNullOrEmpty(FirstName) ? $" { FirstName } " : string.Empty;
                string lastName = !string.IsNullOrEmpty(LastName) ? $" { LastName } " : string.Empty;
                string middleName = !string.IsNullOrEmpty(MiddleName) ? $" { MiddleName } " : string.Empty;
                string suffix = !string.IsNullOrEmpty(Suffix) ? Suffix : string.Empty;

                return $"{ salutation }{ FirstName }{ MiddleName }{ LastName }{ Suffix }".Trim();
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor that creates a name object with specified salutation, first name, middle name, last name, and suffix.
        /// </summary>
        /// <param name="salutation">Salutation.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="middleName">Middle name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="suffix">Suffix.</param>
        public Name(Salutation salutation, string firstName, string middleName, string lastName, string suffix)
        {
            Salutation = salutation;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Suffix = suffix;
        }

        /// <summary>
        /// Constructor that creates a name object with specified first name, middle name, and last name with no salutation and suffix.
        /// </summary>
        /// <param name="firstName">First name.</param>
        /// <param name="middleName">Middle name.</param>
        /// <param name="lastName">Last name.</param>
        public Name(string firstName, string middleName, string lastName)
            : this(Salutation.Unspecified, 
                    firstName, 
                    middleName, 
                    lastName, 
                    string.Empty)
        {
        }

        /// <summary>
        /// Constructor that creates a name object with specified first name, and last name with no salutation and suffix.
        /// </summary>
        /// <param name="firstName">First name.</param>
        /// <param name="middleName">Middle name.</param>
        /// <param name="lastName">Last name.</param>
        public Name(string firstName, string lastName)
            : this(Salutation.Unspecified, 
                    firstName, 
                    string.Empty, 
                    lastName, 
                    string.Empty)
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
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
