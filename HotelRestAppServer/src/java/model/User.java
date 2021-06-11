package model;

import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import java.util.logging.Level;
import java.util.logging.Logger;

public class User {
    private int id;
    private String username;
    private String hashedPassword;
    private byte[] salt;

    public User(int id, String username, char[] password) {
        try {
            this.id = id;
            this.username = username;
           
            this.salt = AlgorithmPBKDF2.getSalt();
            this.hashedPassword = AlgorithmPBKDF2.generateHash(password, salt);
        } catch (NoSuchAlgorithmException | InvalidKeySpecException ex) {
            Logger.getLogger(User.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    public boolean isPasswordValid(char[] password) {
        try {
            return hashedPassword.equals(AlgorithmPBKDF2.generateHash(password, salt));
        } catch (NoSuchAlgorithmException | InvalidKeySpecException ex) {
            Logger.getLogger(User.class.getName()).log(Level.SEVERE, null, ex);
        }
        return false;
    }

    public int getId() {
        return id;
    }
    
    public String getUsername() {
        return username;
    }
}
