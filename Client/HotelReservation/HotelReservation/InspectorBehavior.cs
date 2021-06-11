using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace HotelReservation
{
    class InspectorBehavior : IEndpointBehavior
    {
        public string LastRequestXML
        {
            get
            {
                return myMessageInspector.LastRequestXML;
            }
        }

        public string LastResponseXML
        {
            get
            {
                return myMessageInspector.LastResponseXML;
            }
        }
        private MyMessageInspector myMessageInspector = new MyMessageInspector();
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(myMessageInspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            
        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }

        public void AddCredentialsToHeader(string username, string password)
        {
            this.myMessageInspector.Username = username;
            this.myMessageInspector.Password = password;
        }
    }

    public class MyMessageInspector : IClientMessageInspector
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string LastRequestXML { get; private set; }
        public string LastResponseXML { get; private set; }
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            LastResponseXML = reply.ToString();
        }

        public object BeforeSendRequest(ref Message request, System.ServiceModel.IClientChannel channel)
        {
            MessageHeader header = MessageHeader.CreateHeader("Username", "http://server.group.hotel.com/", Username);
            MessageHeader header2 = MessageHeader.CreateHeader("Password", "http://server.group.hotel.com/", Password);
            request.Headers.Add(header);
            request.Headers.Add(header2);
            LastRequestXML = request.ToString();
            return request;
        }
    }
}
