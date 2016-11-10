using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Feedback
    {
        #region Fields

        private Guid _feedbackId;
        private User _user;
        private string _comments;
        private Rating _rating;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        #endregion

        #region Properties
        public Guid FeedbackId
        {
            get
            {
                return _feedbackId;
            }

            set
            {
                _feedbackId = value;
            }
        }

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public string Comments
        {
            get
            {
                return _comments;
            }

            set
            {
                _comments = value;
            }
        }

        public Rating Rating
        {
            get
            {
                return _rating;
            }

            set
            {
                _rating = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }
            set
            {
                _modifiedDate = value;
            }
        }

        #endregion

        #region Constructors

        public Feedback()
        {
            FeedbackId = Guid.Empty;
            Comments = string.Empty;
            Rating = Rating.Worst;
            CreatedDate = DateTime.MaxValue;
            ModifiedDate = DateTime.MaxValue;
        }

        #endregion
    }
}
