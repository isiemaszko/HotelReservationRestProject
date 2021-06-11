using HotelReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HotelReservation
{
    public partial class MainWindow : Window
    {
        private bool contentValid = true;
        private bool modifyContentValid;
        public List<Room> Rooms { get; set; }
        public List<Reservation> Reservations { get; set; }
        public Reservation SelectedReservation { get; set; } = null;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UsernameLabel.Text = ServiceConnection.GetConnection().Username;
            FromDatePicker.SelectedDate = DateTime.Today;
            ToDatePicker.SelectedDate = DateTime.Today.AddDays(1);
        }

        private async void LoadRooms()
        {
            AsyncTaskMessage.Text = "Loading rooms";
            Rooms = await ServiceConnection.GetConnection().GetRooms(
                FromDatePicker.SelectedDate.GetValueOrDefault(DateTime.Today),
                ToDatePicker.SelectedDate.GetValueOrDefault(DateTime.Today)
                );
            RoomList.ItemsSource = Rooms;
            AsyncTaskMessage.Text = "";
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceConnection.GetConnection().Logout();
            new LoginBox().Show();
            Close();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FromDatePicker.SelectedDate.HasValue && ToDatePicker.SelectedDate.HasValue)
            {
                if (FromDatePicker.SelectedDate.Value >= DateTime.Today)
                {
                    FromDatePicker.Background = Brushes.Transparent;
                }
                else
                {
                    FromDatePicker.Background = Brushes.Red;
                    contentValid = false;
                }
                if (FromDatePicker.SelectedDate.Value < ToDatePicker.SelectedDate.Value)
                {
                    ToDatePicker.Background = Brushes.Transparent;
                }
                else
                {
                    ToDatePicker.Background = Brushes.Red;
                    contentValid = false;
                }
                if (FromDatePicker.SelectedDate.Value >= DateTime.Today && FromDatePicker.SelectedDate.Value < ToDatePicker.SelectedDate.Value)
                {
                    LoadRooms();
                    contentValid = true;
                }
                else
                {
                    MakeReservation.IsEnabled = false;
                }
            }
        }

        private void RoomList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Reservation reservation = ReservationSelection.SelectedItem as Reservation;
            if (reservation is null)
            {
                if (RoomList.SelectedItems.Count == 1)
                {
                    Room room = (Room)RoomList.SelectedItems[0];
                    WindowTextBox.Text = room.windowDirection;
                    DescriptionTextBox.Text = room.description;
                }
                else
                {
                    WindowTextBox.Text = "";
                    DescriptionTextBox.Text = "";
                }
                if (RoomList.SelectedItems.Count == 0)
                {
                    MakeReservation.IsEnabled = false;
                }
                if (RoomList.SelectedItems.Count > 0)
                {
                    if (contentValid)
                        MakeReservation.IsEnabled = true;
                }
            }
            else
            {
                CheckForChanges();
            }
        }

        private async void MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            AsyncTaskMessage.Text = "Making reservation";

            List<string> roomNumbers = new List<string>();
            System.Collections.IList selectedRooms = RoomList.SelectedItems;
            for (int i = 0; i < selectedRooms.Count; i++)
            {
                roomNumbers.Add((selectedRooms[i] as Room).roomNumber);
            }

            try
            {
                int reservationNumber = await ServiceConnection.GetConnection().MakeReservation(
                        roomNumbers,
                        FromDatePicker.SelectedDate.Value,
                        ToDatePicker.SelectedDate.Value,
                        ReservationNotes.Text);

                MessageBox.Show("Reservation made with number " + reservationNumber.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadRooms();
            }
            AsyncTaskMessage.Text = "";
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (MakeTab.IsSelected)
                {
                    if (contentValid)
                    {
                        LoadRooms();
                    }
                }
                else if (ListTab.IsSelected)
                {
                    LoadReservations();
                    RoomList.ItemsSource = new List<Room>();
                }
            }
        }

        private async void LoadReservations()
        {
            AsyncTaskMessage.Text = "Loading reservations";
            Reservations = await ServiceConnection.GetConnection().GetReservations();
            ReservationSelection.IsEnabled = Reservations.Count > 0;
            ReservationSelection.ItemsSource = Reservations;
            AsyncTaskMessage.Text = "";
        }

        private void ReservationSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReservationSelection.SelectedItem is null)
            {
                ModifyFromDatePicker.SelectedDate = null;
                ModifyFromDatePicker.IsEnabled = false;
                ModifyToDatePicker.SelectedDate = null;
                ModifyToDatePicker.IsEnabled = false;
                ModifyReservationNotes.Text = "";
                ModifyReservationNotes.IsEnabled = false;
                ConfirmationButton.IsEnabled = false;
                CancelButton.IsEnabled = false;
                ModifyButton.IsEnabled = false;
                RoomList.ItemsSource = new List<Room>();
            }
            else
            {
                Reservation selectedReservation = ReservationSelection.SelectedItem as Reservation;
                ModifyFromDatePicker.SelectedDate = selectedReservation.from;
                ModifyFromDatePicker.IsEnabled = true;
                ModifyToDatePicker.SelectedDate = selectedReservation.to;
                ModifyToDatePicker.IsEnabled = true;
                ModifyReservationNotes.Text = selectedReservation.notes;
                ModifyReservationNotes.IsEnabled = true;
                ConfirmationButton.IsEnabled = true;
                CancelButton.IsEnabled = true;
                CheckForChanges();
            }
        }

        private void ModifyDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Reservation reservation = ReservationSelection.SelectedItem as Reservation;
            if (reservation is null)
                return;

            if (ModifyFromDatePicker.SelectedDate.HasValue)
            {
                DateTime from = ModifyFromDatePicker.SelectedDate.Value;
                if (from != (ReservationSelection.SelectedItem as Reservation).from)
                {
                    if (from >= DateTime.Today)
                    {
                        ModifyFromDatePicker.Background = Brushes.LawnGreen;
                    }
                    else
                    {
                        ModifyFromDatePicker.Background = Brushes.Red;
                    }
                }
                else
                {
                    ModifyFromDatePicker.Background = Brushes.Transparent;
                }
            }
            else
            {
                ModifyFromDatePicker.Background = Brushes.Red;
            }

            if (ModifyToDatePicker.SelectedDate.HasValue)
            {
                DateTime to = ModifyToDatePicker.SelectedDate.Value;
                if (to != (ReservationSelection.SelectedItem as Reservation).to)
                {
                    ModifyToDatePicker.Background = Brushes.LawnGreen;
                }
                else
                {
                    ModifyToDatePicker.Background = Brushes.Transparent;
                }
                if (ModifyFromDatePicker.SelectedDate.HasValue)
                {
                    if (to <= ModifyFromDatePicker.SelectedDate.Value)
                    {
                        ModifyToDatePicker.Background = Brushes.Red;
                    }
                }
            }
            else
            {
                ModifyToDatePicker.Background = Brushes.Red;
            }

            if (ModifyToDatePicker.Background != Brushes.Red && ModifyFromDatePicker.Background != Brushes.Red)
            {
                modifyContentValid = true;
                LoadModifiedRooms();
            }
            else
            {
                modifyContentValid = false;
            }
            
            CheckForChanges();
        }

        private async void LoadModifiedRooms()
        {
            Reservation reservation = ReservationSelection.SelectedItem as Reservation;
            if (reservation is null)
                return;

            AsyncTaskMessage.Text = "Loading rooms";
            List<Room> rooms = await ServiceConnection.GetConnection().GetRooms(
                ModifyFromDatePicker.SelectedDate.GetValueOrDefault(DateTime.Today),
                ModifyToDatePicker.SelectedDate.GetValueOrDefault(DateTime.Today)
                );
            reservation.rooms.ToList().ForEach(room => rooms.Insert(0, room));
            RoomList.ItemsSource = rooms;
            reservation.rooms.ToList().ForEach(room => RoomList.SelectedItems.Add(room));
            AsyncTaskMessage.Text = "";
        }

        private void Rooms_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Reservation reservation = ReservationSelection.SelectedItem as Reservation;
            if (reservation is null)
                return;
            var row = e.Row;
            var room = row.DataContext as Room;
            if (sender == RoomList)
            {
                if (reservation.rooms.Contains(room))
                {
                    row.Background = Brushes.LawnGreen;
                    return;
                }
            }
            row.Background = Brushes.Transparent;
        }

        private void CheckForChanges()
        {
            Reservation reservation = ReservationSelection.SelectedItem as Reservation;
            if (reservation != null)
            {
                if (ModifyFromDatePicker.SelectedDate.HasValue && ModifyFromDatePicker.SelectedDate.Value == reservation.from
                    && ModifyToDatePicker.SelectedDate.HasValue && ModifyToDatePicker.SelectedDate.Value == reservation.to
                    && ModifyReservationNotes.Text == reservation.notes && RoomsChanged())
                {
                    RevertButton.IsEnabled = false;
                    ModifyButton.IsEnabled = false;
                }
                else
                {
                    RevertButton.IsEnabled = true;
                    if (modifyContentValid)
                    {
                        ModifyButton.IsEnabled = true;
                    }
                    else
                    {
                        ModifyButton.IsEnabled = false;
                    }
                }
            }
            else
            {
                RevertButton.IsEnabled = false;
            }
        }

        private bool RoomsChanged()
        {
            Reservation reservation = ReservationSelection.SelectedItem as Reservation;
            if (RoomList.SelectedItems.Count == 0)
                modifyContentValid = false;
            else
                modifyContentValid = true;
            if (RoomList.SelectedItems.Count != reservation.rooms.Length)
                return false;
            foreach (var room in reservation.rooms)
            {
                if (!RoomList.SelectedItems.Contains(room))
                {
                    return false;
                }
            }
            return true;
        }

        private void ModifyReservationNotes_TextChanged(object sender, TextChangedEventArgs e)
        {
            Reservation reservation = ReservationSelection.SelectedItem as Reservation;
            if (reservation != null && ModifyReservationNotes.Text != reservation.notes)
            {
                ModifyReservationNotes.Background = Brushes.LawnGreen;
            }
            else
            {
                ModifyReservationNotes.Background = Brushes.Transparent;
            }
            CheckForChanges();
        }

        private void RevertButton_Click(object sender, RoutedEventArgs e)
        {
            Reservation selectedReservation = ReservationSelection.SelectedItem as Reservation;
            ModifyFromDatePicker.SelectedDate = selectedReservation.from;
            ModifyToDatePicker.SelectedDate = selectedReservation.to;
            ModifyReservationNotes.Text = selectedReservation.notes;
            RoomList.SelectedItems.Clear();
            selectedReservation.rooms.ToList().ForEach(room => RoomList.SelectedItems.Add(room));
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AsyncTaskMessage.Text = "Cancelling reservation";
            Reservation reservation = ReservationSelection.SelectedItem as Reservation;
            if (await ServiceConnection.GetConnection().CancelReservation(reservation.number))
            {
                MessageBox.Show("Reservation with number " + reservation.number.ToString() + " cancelled");
            }
            else
            {
                MessageBox.Show("Could not cancel reservation");
            }
            LoadReservations();
            AsyncTaskMessage.Text = "";
        }

        private async void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            AsyncTaskMessage.Text = "Modifying reservation";

            Reservation reservation = ReservationSelection.SelectedItem as Reservation;

            reservation.from = ModifyFromDatePicker.SelectedDate.Value;
            reservation.to = ModifyToDatePicker.SelectedDate.Value;
            reservation.notes = ModifyReservationNotes.Text;

            List<Room> rooms = new List<Room>();
            System.Collections.IList selectedRooms = RoomList.SelectedItems;
            for (int i = 0; i < selectedRooms.Count; i++)
            {
                rooms.Add(selectedRooms[i] as Room);
            }
            reservation.rooms = rooms.ToArray();

            if (await ServiceConnection.GetConnection().ModifyReservation(reservation))
            {
                MessageBox.Show("Reservation with number " + reservation.number.ToString() + " modified");
            }
            else
            {
                MessageBox.Show("Could not modify reservation");
            }
            LoadReservations();
            ReservationSelection.SelectedIndex = -1;
            AsyncTaskMessage.Text = "";
        }
    }
}
