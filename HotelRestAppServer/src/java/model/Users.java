package model;

import java.util.Arrays;
import java.util.List;
import java.util.NoSuchElementException;

public class Users {
    private static final List<User> userList = Arrays.asList(
            new User(1, "test", "latwehaslo".toCharArray()),
            new User(2, "test2", "trudnehaslo".toCharArray()));
    
    private Users() {
        throw new UnsupportedOperationException();
    }
    
    public static User findUser(String username) {
        return userList
                .stream()
                .filter(user -> username.equals(user.getUsername()))
                .findFirst()
                .orElseThrow(() -> new NoSuchElementException());  
    }
    
    public static User findUser(int idUser) {
        return userList
                .stream()
                .filter(user -> idUser==user.getId())
                .findFirst()
                .orElseThrow(() -> new NoSuchElementException());  
    }
    
    public static List<User> getUsers() {
        return userList;
    }
}
