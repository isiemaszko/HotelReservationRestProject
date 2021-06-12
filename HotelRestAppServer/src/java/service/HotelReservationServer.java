/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package service;

import com.itextpdf.text.DocumentException;
import java.text.ParseException;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.List;
import javax.ejb.Singleton;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.UriInfo;
import javax.ws.rs.Produces;
import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PUT;
import javax.ws.rs.PathParam;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.MediaType;
import model.Reservation;
import model.Room;
import model.TempMakeReservation;
import resources.BadRequestException;
import resources.InvalidCredentialsException;
import resources.RoomReservationService;

/**
 * REST Web Service
 *
 * @author izabe
 */
@Singleton
@Path("hotel")
public class HotelReservationServer {

    RoomReservationService roomReservationService = new RoomReservationService();

    @Context
    private UriInfo context;

    /**
     * Creates a new instance of HotelReservationServer
     * @throws resources.InvalidCredentialsException
     */
    
    @GET
    @Path("/login")
    public int login(@QueryParam("username") String username, @QueryParam("password") String password) throws InvalidCredentialsException {
        return roomReservationService.login(username,password.toCharArray());
    }
    
    @GET
    @Path("/rooms/{dateFrom}/{dateTo}")
    @Produces(MediaType.APPLICATION_JSON)
    public List<Room> getAvaliableRooms(@PathParam("dateFrom") String from, @PathParam("dateTo") String to) throws BadRequestException, ParseException {
        //format daty YYYY-MM-DD 

        String[] datF = from.split("-", 3);
        String[] datT = to.split("-", 3);
        Date dateFrom = new GregorianCalendar(Integer.parseInt(datF[0]), Integer.parseInt(datF[1]) - 1, Integer.parseInt(datF[2])).getTime();
        Date dateTo = new GregorianCalendar(Integer.parseInt(datT[0]), Integer.parseInt(datT[1]) - 1, Integer.parseInt(datT[2])).getTime();

        return roomReservationService.getAvailableRooms(dateFrom, dateTo);

    }

    @GET
    @Path("/getReservations/{userID}")
    @Produces(MediaType.APPLICATION_JSON)
    public List<Reservation> getReservations(@PathParam("userID") int userId) {
        return roomReservationService.getReservations(userId);
    }

    @POST
    @Consumes(MediaType.APPLICATION_JSON)
    @Path("/makeReservation")
    public int makeReservation(TempMakeReservation reservation) throws BadRequestException {

        return roomReservationService.makeReservation(reservation);
    }

    @DELETE
    @Produces(MediaType.APPLICATION_JSON)
    @Path("/cancelReservation")
    public void cancelReservation(@QueryParam("reservationNumber") int reservationNumber, @QueryParam("userId") int userId) throws BadRequestException {

        roomReservationService.cancelReservation(reservationNumber, userId);

    }

    //public void modifyReservation(Reservation reservation, int userId)
    
    @PUT
    @Produces(MediaType.APPLICATION_JSON)
    @Consumes(MediaType.APPLICATION_JSON)
    @Path("/modifyReservation/{userId}")
    public void modifyReservation(Reservation reservation,@PathParam("userId")int userId )throws BadRequestException{
        roomReservationService.modifyReservation(reservation,userId);
    }
    
    //public byte[] requestReservationConfirmation(int reservationNumber, int userId) throws BadRequestException,DocumentException {
     @GET
     @Produces(MediaType.APPLICATION_JSON)
     @Path("/confirmation")
     public byte[] requestReservationConfirmation(@QueryParam("reservationNumber") int reservationNumber, @QueryParam("userId") int userId) throws BadRequestException, DocumentException{
         return  roomReservationService.requestReservationConfirmation(reservationNumber, userId);
     }
}
