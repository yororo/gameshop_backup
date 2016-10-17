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
        private DateTime _createdDTTM;
        private DateTime _modifiedDTTM;
        private User _createdBy;
        private User _modifiedBy;

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

        public DateTime CreatedDTTM
        {
            get { return _createdDTTM; }
            set { _createdDTTM = value; }
        }

        public DateTime ModifiedDTTM
        {
            get { return _modifiedDTTM; }
            set { _modifiedDTTM = value; }
        }

        public User CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public User ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        #endregion

        #region Constructors

        public Feedback()
        {
            FeedbackId = Guid.Empty;
            Comments = string.Empty;
            Rating = Rating.Worst;
            CreatedDTTM = DateTime.MaxValue;
            ModifiedDTTM = DateTime.MaxValue;
            CreatedBy = new User();
            ModifiedBy = new User();
        }

        #endregion
    }
}
