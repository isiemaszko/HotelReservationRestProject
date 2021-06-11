using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Models
{
    public partial class Room
    {
        private string roomNumberField;

        private int floorNumberField;

        private bool hasDoubleBedField;

        private int numberOfSingleBedsField;

        private double roomSizeField;

        private bool hasBathroomField;

        private bool isPresidentialSuiteField;

        private string windowDirectionField;

        private string descriptionField;

        public string roomNumber
        {
            get
            {
                return this.roomNumberField;
            }
            set
            {
                this.roomNumberField = value;
            }
        }

        public int floorNumber
        {
            get
            {
                return this.floorNumberField;
            }
            set
            {
                this.floorNumberField = value;
            }
        }

        public bool hasDoubleBed
        {
            get
            {
                return this.hasDoubleBedField;
            }
            set
            {
                this.hasDoubleBedField = value;
            }
        }

        public int numberOfSingleBeds
        {
            get
            {
                return this.numberOfSingleBedsField;
            }
            set
            {
                this.numberOfSingleBedsField = value;
            }
        }

        public double roomSize
        {
            get
            {
                return this.roomSizeField;
            }
            set
            {
                this.roomSizeField = value;
            }
        }

        public bool hasBathroom
        {
            get
            {
                return this.hasBathroomField;
            }
            set
            {
                this.hasBathroomField = value;
            }
        }

        public bool isPresidentialSuite
        {
            get
            {
                return this.isPresidentialSuiteField;
            }
            set
            {
                this.isPresidentialSuiteField = value;
            }
        }

        public string windowDirection
        {
            get
            {
                return this.windowDirectionField;
            }
            set
            {
                this.windowDirectionField = value;
            }
        }

        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
    }
}
