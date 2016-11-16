using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Feedback
    {
        #region Declarations

        private Guid _feedbackId;
        private User _reviewer;
        private User _reviewee;
        private string _review;
        private Rating _rating;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        #endregion Declarations

        #region Properties
        public Guid FeedbackId
        {
            get { return _feedbackId; }
            set { _feedbackId = value; }
        }

        public User Reviewee
        {
            get { return _reviewee; }
            set { _reviewee = value; }
        }

        public User Reviewer
        {
            get { return _reviewer; }
            set { _reviewer = value; }
        }

        public string Review
        {
            get { return _review; }
            set { _review = value; }
        }

        public Rating Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set{ _modifiedDate = value; }
        }

        #endregion Properties

        #region Constructors

        public Feedback()
        {
            _feedbackId = Guid.Empty;
            _reviewee = new User();
            _reviewer = new User();
            _review = string.Empty;
            _rating = Rating.Worst;
            _createdDate = DateTime.MaxValue;
            _modifiedDate = DateTime.MaxValue;
        }

        #endregion Constructors
    }
}
