package model;

import java.util.Arrays;
import java.util.Collection;
import java.util.Date;
import java.util.List;
import java.util.NoSuchElementException;
import java.util.stream.Collectors;

public class Rooms {
    
    private static final List<Room> roomList = Arrays.asList(
            new Room("11", 1, true, 1, 18.9, false, false, "east", "Example description"),
            new Room("12", 1, true, 1, 21.7, false, false, "east", "Example description"),
            new Room("13", 1, false, 2, 19.5, false, false, "east", "Example description"),
            new Room("14", 1, false, 2, 20.6, false, false, "east", "Example description"),
            new Room("15", 1, false, 2, 21.3, false, false, "west", "Example description"),
            new Room("16", 1, true, 1, 19.8, false, false, "west", "Example description"),
            new Room("17", 1, true, 1, 20.2, false, false, "west", "Example description"),
            new Room("18", 1, false, 3, 22.4, false, false, "west", "Example description"),
            new Room("21", 2, true, 0, 28.0, true, false, "east", "Example description"),
            new Room("22", 2, true, 0, 31.6, true, false, "east", "Example description"),
            new Room("23", 2, true, 0, 25.9, true, false, "east", "Example description"),
            new Room("24", 2, true, 0, 32, true, false, "west", "Example description"),
            new Room("25", 2, true, 0, 35.8, true, false, "west", "Example description"),
            new Room("26", 2, true, 0, 28.3, true, false, "west", "Example description"),
            new Room("31", 3, false, 4, 33.8, true, false, "east", "Example description"),
            new Room("32", 3, false, 4, 30.2, true, false, "east", "Example description"),
            new Room("33", 3, false, 3, 28.4, true, false, "east", "Example description"),
            new Room("34", 3, false, 2, 27.5, true, false, "west", "Example description"),
            new Room("35", 3, true, 0, 29.7, true, false, "west", "Example description"),
            new Room("36", 3, true, 0, 30.1, true, false, "west", "Example description"),
            new Room("41", 4, true, 0, 49.5, true, false, "east & south", "Suite"),
            new Room("42", 4, true, 0, 58.4, true, false, "west & south", "Suite"),
            new Room("43", 4, true, 0, 77.9, true, true, "east & north & west", "Presidential suite")
            );
    
    public static List<Room> getAvailableRooms(Date from, Date to) {
        Reservations reservations = Reservations.getInstance();
        
        List<Room> bookedRooms = reservations.getReservationsForPeriod(from, to)
                .stream()
                .map(reservation -> reservation.getRooms())
                .flatMap(Collection::stream)
                .collect(Collectors.toList());
        
        return roomList
                .stream()
                .filter(room -> !bookedRooms.contains(room))
                .collect(Collectors.toList());
    }
    
    private Rooms() {
        throw new UnsupportedOperationException();
    }
    
    public static List<Room> getRooms() {
        return roomList;
    }
    
    public static Room findByNumber(String number) {
        return roomList.stream()
                .filter(room -> number.equals(room.getRoomNumber()))
                .findFirst()
                .orElseThrow(() -> new NoSuchElementException());
    }
}
