using System;

using MMABooksTools;
using MMABooksProps;
using MMABooksDB;

using System.Collections.Generic;

namespace MMABooksBusiness
{
    public class Customer : BaseBusiness
    {

        public int CustomerID
        {
            get
            {
                return ((CustomerProps)mProps).CustomerID;
            }
        }

        public String Name
        {
            get
            {
                return ((CustomerProps)mProps).Name;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).Name))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 100)
                    {
                        mRules.RuleBroken("Name", false);
                        ((CustomerProps)mProps).Name = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Name must be no more than 100 characters long.");
                    }
                }
            }
        }

        public String Address
        {
            get
            {
                return ((CustomerProps)mProps).Address;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).Address))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 50)
                    {
                        mRules.RuleBroken("Address", false);
                        ((CustomerProps)mProps).Address = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Address must be no more than 50 characters long.");
                    }
                }
            }
        }

        public String City
        {
            get
            {
                return ((CustomerProps)mProps).City;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).City))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 20)
                    {
                        mRules.RuleBroken("City", false);
                        ((CustomerProps)mProps).City = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("City must be no more than 100 characters long.");
                    }
                }
            }
        }

        public String State
        {
            get
            {
                return ((CustomerProps)mProps).State;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).State))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 2)
                    {
                        mRules.RuleBroken("State", false);
                        ((CustomerProps)mProps).State = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("State must be no more than 2 characters long.");
                    }
                }
            }
        }

        public String ZipCode
        {
            get
            {
                return ((CustomerProps)mProps).ZipCode;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).ZipCode))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 15)
                    {
                        mRules.RuleBroken("ZipCode", false);
                        ((CustomerProps)mProps).ZipCode = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("ZipCode must be no more than 15 characters long.");
                    }
                }
            }
        }

        public override object GetList()
        {
            throw new NotImplementedException();
        }

        protected override void SetDefaultProperties()
        {
            throw new NotImplementedException();
        }

        protected override void SetRequiredRules()
        {
            throw new NotImplementedException();
        }

        protected override void SetUp()
        {
            throw new NotImplementedException();
        }

        #region constructors
        /// <summary>
        /// Default constructor - gets the connection string - assumes a new record that is not in the database.
        /// </summary>
        public Customer() : base()
        {
        }

        /// <summary>
        /// Calls methods SetUp() and Load().
        /// Use this constructor when the object is in the database AND the connection string is in a config file
        /// </summary>
        /// <param name="key">ID number of a record in the database.
        /// Sent as an arg to Load() to set values of record to properties of an 
        /// object.</param>
        public Customer(int key)
            : base(key)
        {
        }

        private Customer(CustomerProps props)
            : base(props)
        {
        }

        #endregion
    }
}
