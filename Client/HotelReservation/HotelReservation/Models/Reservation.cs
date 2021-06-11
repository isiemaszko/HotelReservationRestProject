using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Models
{
    public class Reservation
    { 
        private int numberField;

        private DateTime fromField;

        private bool fromFieldSpecified;

        private DateTime toField;

        private bool toFieldSpecified;

        private Room[] roomsField;

        private int ownersIdField;

        private string notesField;

        public int number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        public System.DateTime from
        {
            get
            {
                return this.fromField;
            }
            set
            {
                this.fromField = value;
            }
        }

        public bool fromSpecified
        {
            get
            {
                return this.fromFieldSpecified;
            }
            set
            {
                this.fromFieldSpecified = value;
            }
        }

        public System.DateTime to
        {
            get
            {
                return this.toField;
            }
            set
            {
                this.toField = value;
            }
        }

        public bool toSpecified
        {
            get
            {
                return this.toFieldSpecified;
            }
            set
            {
                this.toFieldSpecified = value;
            }
        }

        public Room[] rooms
        {
            get
            {
                return this.roomsField;
            }
            set
            {
                this.roomsField = value;
            }
        }

        public int ownersId
        {
            get
            {
                return this.ownersIdField;
            }
            set
            {
                this.ownersIdField = value;
            }
        }

        public string notes
        {
            get
            {
                return this.notesField;
            }
            set
            {
                this.notesField = value;
            }
        }
    }
}
