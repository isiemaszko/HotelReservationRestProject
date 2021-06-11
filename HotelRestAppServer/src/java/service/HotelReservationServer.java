/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package service;

import java.util.Date;
import java.util.List;
import javax.ejb.Singleton;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.UriInfo;
import javax.ws.rs.Produces;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PUT;
import javax.ws.rs.PathParam;
import javax.ws.rs.core.MediaType;
import model.Room;
import resources.BadRequestException;
import resources.RoomReservationService;

/**
 * REST Web Service
 *
 * @author izabe
 */
@Singleton
@Path("hotel")
public class HotelReservationServer {

    RoomReservationService  roomReservationService=new RoomReservationService();
    
    @Context
    private UriInfo context;

    /**
     * Creates a new instance of HotelReservationServer
     */
    
    @GET
    @Path("/rooms")
    @Produces(MediaType.APPLICATION_JSON)
    public List<Room>  sayHello() {
        return roomReservationService.getRooms();
    }
    
     @GET
    @Path("/rooms/{dateFrom}/{dateTo}")
    @Produces(MediaType.APPLICATION_JSON)
    public List<Room> getAvaliableRooms(@PathParam("dateFrom") Date from, @PathParam("dateTo") Date to) throws BadRequestException {
        return roomReservationService.getAvailableRooms(from,to);
    }
    
  
}
