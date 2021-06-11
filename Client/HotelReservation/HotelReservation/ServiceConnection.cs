using HotelReservation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
            HttpResponseMessage response = await client.GetAsync("rooms");
            if (response.IsSuccessStatusCode)
            {
                var a = await response.Content.ReadAsStringAsync();
                rooms = JsonConvert.DeserializeObject<List<Room>>(a);
            }
            return rooms;
        }

        //    client.Dispose();

        public async Task<bool> Login(string username, string password)
        {
            Username = username;
            this.password = password.ToCharArray();
            ushort?[] packed = new ushort?[this.password.Length];
            for (int i = 0; i < packed.Length; i++)
            {
                packed[i] = this.password[i];
            }

            //X509Store store = new X509Store(StoreLocation.CurrentUser);
            //store.Open(OpenFlags.ReadOnly);

            //X509Certificate2Collection cert = store.Certificates.Find(X509FindType.FindByThumbprint, "34f7113d602342a9ef4ba0539d8fd3b148cdc34b", false);

            //client = new OnlineReceptionImplClient();
            //client.ClientCredentials.ClientCertificate.Certificate = cert[0];

            //store.Close();


            //userId = response.@return;
            return true;
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
            //makeReservationResponse response = await client.makeReservationAsync(roomNumbers.ToArray(), from, to, notes, (int)userId);

            return 1;
        }

        public async Task<List<Reservation>> GetReservations()
        {
            //getReservationsResponse response = await client.getReservationsAsync((int)userId);

            return new List<Reservation>();
        }

        public async Task<bool> CancelReservation(int reservationNumber)
        {
            //try
            //{
            //    cancelReservationResponse response = await client.cancelReservationAsync(reservationNumber, (int)userId);
            //    return true;
            //}
            //catch (FaultException<BadRequestException>)
            //{
            //    return false;
            //}
            return true;
        }

        public async Task<bool> ModifyReservation(Reservation reservation)
        {
            //try
            //{
            //    modifyReservationResponse response = await client.modifyReservationAsync(reservation, (int)userId);
            //    return true;
            //}
            //catch (FaultException<BadRequestException>)
            //{
            //    return false;
            //}
            return true;
        }
    }
}
