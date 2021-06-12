package model;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement
@XmlAccessorType(XmlAccessType.PUBLIC_MEMBER)
public class Reservation {
    private int number;
    private Date from;
    private Date to;
    private List<Room> rooms;
    private int ownersId;
    private String notes;
    private List<Link> links = new ArrayList<Link>();
    
    public boolean containsPeriod(Date from, Date to) {
       return this.from.before(to) && this.to.after(from);
    }
    public Reservation(){}
    public Reservation(int number, Date from, Date to, List<Room> rooms, int ownersId, String notes) {
        this.number = number;
        this.from = from;
        this.to = to;
        this.rooms = rooms;
        this.ownersId = ownersId;
        this.notes = notes;
    }

    public int getNumber() {
        return number;
    }

    public void setNumber(int number) {
        this.number = number;
    }
    
    public Date getFrom() {
        return from;
    }

    void setFrom(Date from) {
        this.from = from;
    }

    public Date getTo() {
        return to;
    }

    void setTo(Date to) {
        this.to = to;
    }

    public List<Room> getRooms() {
        return rooms;
    }

    void setRooms(List<Room> rooms) {
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

    void setNotes(String notes) {
        this.notes = notes;
    }
    
     public List<Link> getLinks() {
        return links;
    }

    public void setLinks(List<Link> link) {
        this.links = link;
    }
    
     public void addLink(String uri, String rel) {
        Link link=new Link();
        link.setLink(uri);
        link.setRel(rel);
        boolean isThere=false;
        for(Link l:links){
            if((l.getRel()).equals(link.getRel())){
                isThere=true;
                System.out.print("znalezniono link");
            }
        }
        if(!isThere) links.add(link);
         
    }
}
