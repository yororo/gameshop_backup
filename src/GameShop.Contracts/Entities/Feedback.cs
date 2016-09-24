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
        private User _owner;
        private DateTime _created;
        private DateTime _modified;

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

        public User Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        public DateTime Modified
        {
            get { return _modified; }
            set { _modified = value; }
        }

        #endregion

        #region Constructors

        public Feedback()
        {
            FeedbackId = Guid.Empty;
            Comments = string.Empty;
            Rating = Rating.Worst;
            Created = DateTime.MaxValue;
            Modified = DateTime.MaxValue;
        }

        #endregion
    }
}
