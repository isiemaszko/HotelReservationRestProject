package model;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement
public class Room {
    private String roomNumber;
    private int floorNumber;
    private boolean hasDoubleBed;
    private int numberOfSingleBeds;
    private double roomSize;
    private boolean hasBathroom;
    private boolean isPresidentialSuite;
    private String windowDirection;
    private String description;

   
     public Room(){}
    public Room(String roomNumber, int floorNumber, boolean hasDoubleBed,
            int numberOfSingleBeds, double roomSize, boolean hasBathroom,
            boolean isPresidentialSuite, String windowDirection,
            String description) {
        this.roomNumber = roomNumber;
        this.floorNumber = floorNumber;
        this.hasDoubleBed = hasDoubleBed;
        this.numberOfSingleBeds = numberOfSingleBeds;
        this.roomSize = roomSize;
        this.hasBathroom = hasBathroom;
        this.isPresidentialSuite = isPresidentialSuite;
        this.windowDirection = windowDirection;
        this.description = description;
    }
    
    public String getRoomNumber() {
        return roomNumber;
    }

    void setRoomNumber(String roomNumber) {
        this.roomNumber = roomNumber;
    }

    public int getFloorNumber() {
        return this.floorNumber;
    }
    
    void setFloorNumber(int floorNumber) {
        this.floorNumber = floorNumber;
    }

    public boolean isHasDoubleBed() {
        return hasDoubleBed;
    }
        
    void setHasDoubleBed(boolean hasDoubleBed) {
        this.hasDoubleBed = hasDoubleBed;
    }
    
    public int getNumberOfSingleBeds() {
        return numberOfSingleBeds;
    }

    void setNumberOfSingleBeds(int numberOfSingleBeds) {
        this.numberOfSingleBeds = numberOfSingleBeds;
    }

    public double getRoomSize() {
        return roomSize;
    }

    void setRoomSize(double roomSize) {
        this.roomSize = roomSize;
    }

    public boolean isHasBathroom() {
        return hasBathroom;
    }

    void setHasBathroom(boolean hasBathroom) {
        this.hasBathroom = hasBathroom;
    }

    public boolean isPresidentialSuite() {
        return isPresidentialSuite;
    }

    void setIsPresidentialSuite(boolean isPresidentialSuite) {
        this.isPresidentialSuite = isPresidentialSuite;
    }

    public String getWindowDirection() {
        return windowDirection;
    }

    void setWindowDirection(String windowDirection) {
        this.windowDirection = windowDirection;
    }

    public String getDescription() {
        return description;
    }

    void setDescription(String description) {
        this.description = description;
    }  
}
