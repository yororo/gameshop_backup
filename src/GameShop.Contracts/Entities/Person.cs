using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public abstract class Person
    {
        #region Fields

        private Name _name;
        private List<Address> _addresses;
        private List<ContactInformation> _contactInformation;
        private ContractInformation _contractInformation;

        #endregion

        #region Properties

        public Name Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public List<Address> Addresses
        {
            get
            {
                return _addresses;
            }

            set
            {
                _addresses = value;
            }
        }

        public List<ContactInformation> ContactInformation
        {
            get
            {
                return _contactInformation;
            }

            set
            {
                _contactInformation = value;
            }
        }

        public ContractInformation ContractInformation
        {
            get
            {
                return _contractInformation;
            }

            set
            {
                _contractInformation = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public Person()
        {
            Name = new Name();
            Addresses = new List<Address>();
            ContactInformation = new List<ContactInformation>();
            ContractInformation = new ContractInformation();
        }

        #endregion
    }
}
