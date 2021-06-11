/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package model;

import java.util.Date;
import java.util.List;

/**
 *
 * @author izabe
 */
public class TempMakeReservation {
    private Date from;
    private Date to;
    private List<Integer> rooms;
    private int ownersId;
    private String notes;

    public TempMakeReservation(){}
    public TempMakeReservation( Date from, Date to, List<Integer> rooms, int ownersId, String notes) {
        
        this.from = from;
        this.to = to;
        this.rooms = rooms;
        this.ownersId = ownersId;
        this.notes = notes;
    }
    

    public Date getFrom() {
        return from;
    }

    public void setFrom(Date from) {
        this.from = from;
    }

    public Date getTo() {
        return to;
    }

    public void setTo(Date to) {
        this.to = to;
    }

    public List<Integer> getRooms() {
        return rooms;
    }

    public void setRooms(List<Integer> rooms) {
        this.rooms = rooms;
    }

    public int getOwnersId() {
        return ownersId;
    }

    public void setOwnersId(int ownersId) {
        this.ownersId = ownersId;
    }

    public String getNotes() {
        return notes;
    }

    public void setNotes(String notes) {
        this.notes = notes;
    }
    
    
}
