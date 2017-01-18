using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Feedback
    {
        #region Properties

        public Guid Id { get; set; }

        public User Reviewee { get; set; }

        public User Reviewer { get; set; }

        public string Review { get; set; }

        public Rating Rating { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Feedback()
        {
            Id = Guid.Empty;
            Reviewee = new User();
            Reviewer = new User();
            Review = string.Empty;
            Rating = Rating.Worst;
            CreatedDate = DateTime.MaxValue;
            ModifiedDate = DateTime.MaxValue;
        }

        #endregion Constructors
    }
}
