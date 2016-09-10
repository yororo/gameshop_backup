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
        private string _comments;
        private Rating _rating;
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
        #endregion

        #region Constructors
        public Feedback()
        {
            FeedbackId = Guid.Empty;
            Comments = string.Empty;
            Rating = Rating.Worst;
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
