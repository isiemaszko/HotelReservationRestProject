using HotelReservation.Models;
using HotelReservation.Models.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HotelReservation
{
    class ServiceConnection
    {
        private static ServiceConnection instance;
        private const string URL = "http://localhost:8080/HotelRestAppServer/webresources/hotel/";
        private HttpClient client;
        private int? userId;
        public string Username { get; private set; }
        private char[] password;


        private ServiceConnection()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        }

        public static ServiceConnection GetConnection()
        {
            if (instance == null)
            {
                instance = new ServiceConnection();
            }
            return instance;
        }

        public async Task<List<Room>> GetRooms(DateTime from, DateTime to)
        {
            List<Room> rooms = new List<Room>();
            HttpResponseMessage response = await client.GetAsync("rooms/" + from.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "/" + to.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                rooms = JsonConvert.DeserializeObject<List<Room>>(responseString);
            }
            return rooms;
        }

        //    client.Dispose();

        public async Task<bool> Login(string username, string password)
        {
            Username = username;
            this.password = password.ToCharArray();

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["?username"] = username;
            query["password"] = password;
            HttpResponseMessage response = await client.GetAsync("login/" + query.ToString());
            if (response.IsSuccessStatusCode)
            {
                string svcCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(Username + ":" + password.ToString()));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + svcCredentials);
                userId = int.Parse(await response.Content.ReadAsStringAsync());
                return true;
            }
            return false;

            //X509Store store = new X509Store(StoreLocation.CurrentUser);
            //store.Open(OpenFlags.ReadOnly);

            //X509Certificate2Collection cert = store.Certificates.Find(X509FindType.FindByThumbprint, "34f7113d602342a9ef4ba0539d8fd3b148cdc34b", false);

            //client = new OnlineReceptionImplClient();
            //client.ClientCredentials.ClientCertificate.Certificate = cert[0];

            //store.Close();
        }

        public void Logout()
        {
            client = null;
            Username = null;
            password = null;
            userId = null;
            instance = null;
        }

        public async Task<int> MakeReservation(List<string> roomNumbers, DateTime from, DateTime to, string notes)
        {
            MakeReservation reservation = new MakeReservation();
            reservation.rooms = roomNumbers.Select(int.Parse).ToList();
            reservation.from = from.ToString("yyyy-MM-dd") + "T00:00:00+02:00";
            reservation.to = to.ToString("yyyy-MM-dd") + "T00:00:00+02:00";
            reservation.notes = notes;
            reservation.ownersId = userId.Value;

            HttpResponseMessage response = await client.PostAsync("makeReservation", new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                int reservationId = int.Parse(responseString);
                return reservationId;
            }
            throw new Exception();
        }

        public async Task<List<Reservation>> GetReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            HttpResponseMessage response = await client.GetAsync("getReservations/" + userId.Value.ToString());
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                reservations = JsonConvert.DeserializeObject<List<Reservation>>(responseString);
            }
            return reservations;
        }

        public async Task<bool> CancelReservation(int reservationNumber)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["?reservationNumber"] = reservationNumber.ToString();
            query["userId"] = userId.Value.ToString();
            HttpResponseMessage response = await client.DeleteAsync("cancelReservation/" + query.ToString());
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ModifyReservation(Reservation reservation)
        {
            HttpResponseMessage response = await client.PutAsync("modifyReservation/" + userId.Value.ToString(), new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<byte[]> GetReservationConfirmation(int reservationNumber)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["?reservationNumber"] = reservationNumber.ToString();
            query["userId"] = userId.Value.ToString();
            HttpResponseMessage response = await client.GetAsync("confirmation/" + query.ToString());
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            throw new Exception();
        }
    }
}
